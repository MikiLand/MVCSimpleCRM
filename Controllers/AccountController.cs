﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;
using MVCSimpleCRM.Repository;
using MVCSimpleCRM.ViewModels;

namespace MVCSimpleCRM.Controllers
{
    public class AccountController : Controller
    {
        //private readonly ApplicationDbContext _context;


        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public object ViewBag { get; }

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext context,
            IAccountRepository accountRepository)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            this._accountRepository = accountRepository;
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
            return View(accounts);
        }

        public async Task<IActionResult> Detail(string id)
        {
            //Users users = _context.users.FirstOrDefault(u => u.Id == id);
            AspNetUsers accounts = await _accountRepository.GetByIdAsync(id);
            return View(accounts);
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

            return RedirectToAction("Detail", "Account");
        }

        /*private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;*/

        /*public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }*/

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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
