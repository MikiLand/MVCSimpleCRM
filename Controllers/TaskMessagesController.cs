using Microsoft.AspNetCore.Mvc;

namespace MVCSimpleCRM.Controllers
{
    public class TaskMessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
