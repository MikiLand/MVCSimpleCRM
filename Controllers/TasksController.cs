using Microsoft.AspNetCore.Mvc;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Tasks> tasks = _context.tasks.ToList();
            List<Users> users = _context.users.ToList();
            return View(tasks);
        }
    }
}
