using Microsoft.AspNetCore.Mvc;

namespace MVCSimpleCRM.Controllers
{
    public class TaskUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
