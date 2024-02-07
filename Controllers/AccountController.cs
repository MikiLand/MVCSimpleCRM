﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;
using MVCSimpleCRM.Repository;
using MVCSimpleCRM.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCSimpleCRM.Controllers
{
    public class AccountController : Controller
    {
        //private readonly ApplicationDbContext _context;


        private readonly IAccountRepository _accountRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskUserRepository _taskUserRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public object ViewBag { get; }

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext context,
            IAccountRepository accountRepository,
            ITaskRepository taskRepository,
            ITaskUserRepository taskUserRepository)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            this._accountRepository = accountRepository;
            this._taskRepository = taskRepository;
            this._taskUserRepository = taskUserRepository;
        }

        /*public AccountController(IAccountRepository accountRepository) 
        {
            //_context = context;
            this._accountRepository = accountRepository;
        }*/

        public async Task<IActionResult> Index()
        {
            //List<Users> users = _context.users.ToList();
            IEnumerable<AspNetUsers> accounts = await _accountRepository.GetAll();
            var userCreatedTasks = await _accountRepository.GetAllUserCreatedTasks();
            return View(accounts);
        }

        public async Task<IActionResult> Detail(string id)
        {
            //AspNetUsers accounts = await _accountRepository.GetByIdAsync(id);
            //return View(accounts);

            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null) return View("Error");

            List<int> tasksIDList = await _taskUserRepository.GetTopUserAttachedTasks(account.Id);

            var accountVM = new DetailAccountViewModel
            {
                Id = account.Id,
                UserName = account.UserName,
                Name = account.Name,
                Surname = account.Surname,
                Email = account.Email,
                PasswordHash = account.PasswordHash,
                Tasks = await _taskRepository.GetAllTasksCreatedByUser(account.Id),
                CreatedTasks = await _taskRepository.GetTopTasksCreatedByUser(account.Id, 3),
                UserTasks = await _taskRepository.GetTopUserTasks(tasksIDList, 3)
            };
            return View(accountVM);
        }

        public async Task<IActionResult> DetailMyAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return RedirectToAction("Detail", "Account", new { id = userId });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null) return View("Error");
            var accountVM = new EditAccountViewModel
            {
                Id = account.Id,
                UserName = account.UserName,
                Name = account.Name,
                Surname = account.Surname,
                Email = account.Email,
                PasswordHash = account.PasswordHash
            };
            return View(accountVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditAccountViewModel accountVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Edycja nieudana!");
                return View("Edit", accountVM);
            }

            var account = new AspNetUsers
            {
                Id = accountVM.Id,
                UserName = accountVM.UserName,
                Name = accountVM.Name,
                Surname = accountVM.Surname,
                Email = accountVM.Email,
                PasswordHash = accountVM.PasswordHash
            };

            _accountRepository.Update(account);

            return RedirectToAction("Detail", "Account", new {id = account.Id });
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.PasswordHash);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.PasswordHash, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Account");
                    }
                }
                TempData["Error"] = "Podane hasło nie jest prawidłowe!";
                return View(loginVM);
            }
            TempData["Error"] = "Podany użytkownik nie istnieje!";
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
        
            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user != null)
            {
                TempData["Error"] = "Użytkownik o takim adresie email już istnieje";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                UserName = registerViewModel.UserName,
                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                Email = registerViewModel.Email,
                //PasswordHash = registerViewModel.PasswordHash
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.PasswordHash);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var accountDetails = await _accountRepository.GetByIdAsync(id);
            if (accountDetails == null)
            {
                TempData["Result"] = "ERROR";
                return View("Error");
            }
            _accountRepository.Delete(accountDetails);
            TempData["Result"] = "OK";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true)

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/account/RefreshSearchedUsers")]
        public async Task<IActionResult> RefreshSearchedUsers(string SearchedUser)
        {
            List<AspNetUsers> UsersList = await _accountRepository.GetSearchedUsers(SearchedUser);

            var tasks = new IndexTaskViewModel
            {
                Users = new List<AspNetUsersIndexViewModel> { }
            };

            foreach (var User in UsersList)
            {
                var UserIndexVM = new AspNetUsersIndexViewModel
                {
                    Id = User.Id,
                    UserName = User.UserName,
                    Name = User.Name,
                    Surname = User.Surname,
                    Email = User.Email,
                    PasswordHash = User.PasswordHash,
                    CreateDate = User.CreateDate,
                    IsChecked = false
                };

                tasks.Users.Add(UserIndexVM);
            }
            HttpContext.Session.SetString("ActualSearchedUsersModel", JsonConvert.SerializeObject(tasks));

            return PartialView("_SearchedUsers", tasks);
        }

        public async Task<IActionResult> SetChecked(string UserID, string SearchedTaskTitle, int SortBy, DateTime DateFrom, DateTime DateTo, string DateType)
        {
            var ActualSearchedUsersModel = HttpContext.Session.GetString("ActualSearchedUsersModel");
            /*var tasks = new IndexTaskViewModel
            {
                Users = new List<AspNetUsersIndexViewModel> { }
            };*/

            IndexTaskViewModel ActualUsersVM = JsonConvert.DeserializeObject<IndexTaskViewModel>(ActualSearchedUsersModel);

            foreach (var User in ActualUsersVM.Users)
            {
                if(User.Id == UserID)
                {
                    if(User.IsChecked == false)
                    {
                        User.IsChecked = true;
                    }
                    else
                    {
                        User.IsChecked = false;
                    }
                }
            }

            HttpContext.Session.SetString("ActualSearchedUsersModel", JsonConvert.SerializeObject(ActualUsersVM));

            return PartialView("_SearchedUsers", ActualUsersVM);
        }
    }
}
