using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;
using MVCSimpleCRM.ViewModels;
using System.Reflection;

namespace MVCSimpleCRM.Controllers
{
    public class UsersController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        //public UsersController(ApplicationDbContext context, IUserRepository userRepository)
        public UsersController(IUserRepository userRepository) 
        {
            //_context = context;
            this._userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            //List<Users> users = _context.users.ToList();
            IEnumerable<Users> users = await _userRepository.GetAll();
            return View(users);
        }

        public async Task<IActionResult> Detail(int id)
        {
            //Users users = _context.users.FirstOrDefault(u => u.Id == id);
            Users users = await _userRepository.GetByIdAsync(id);
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel userVM)
        {
            if(ModelState.IsValid) 
            {
                var user = new Users
                {
                    Name = userVM.Name,
                    Surname = userVM.Surname,
                    Login = userVM.Login,
                    Email = userVM.Email,
                    Password = userVM.Password,
                    IsTACConfirmed = true,
                    IsAdmin = 0,
                    CreateDate = DateTime.Now
                };
                _userRepository.Add(user);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error");
            }

            return View(userVM);
        }

        /*public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return View("Error");
        }*/
    }
}
