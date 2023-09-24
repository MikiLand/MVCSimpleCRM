using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.ViewModels;
using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Users loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByLoginAsync(loginVM.Login, "");

            if(User != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if(result.Succeeded)
                    {
                        RedirectToAction("Index", "Users");
                    }
                }

                TempData["Error"] = "Podane hasło nie jest prawidłowe!";
                return View(loginVM);
            }
            TempData["Error"] = "Podany użytkownik nie istnieje!";
            return View(loginVM);
        }
    }
}
