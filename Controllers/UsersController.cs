using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;

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
    }
}
