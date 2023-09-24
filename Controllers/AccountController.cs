using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.ViewModels;
using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
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
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.PasswordHash);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.PasswordHash, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Users");
                    }
                }
                //Password is incorrect
                TempData["Error"] = "Podane hasło nie jest prawidłowe!";
                return View(loginVM);
            }
            //User not found
            TempData["Error"] = "Podany użytkownik nie istnieje!";
            return View(loginVM);

            /*if (!ModelState.IsValid) return View(loginVM);

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
            return View(loginVM);*/
        }
    }
}
