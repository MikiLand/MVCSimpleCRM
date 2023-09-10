using Microsoft.AspNetCore.Mvc;

namespace MVCSimpleCRM.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
