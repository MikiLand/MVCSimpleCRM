using Microsoft.AspNetCore.Mvc;

namespace MVCSimpleCRM.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
