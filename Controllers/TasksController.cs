using Microsoft.AspNetCore.Mvc;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;
using MVCSimpleCRM.ViewModels;

namespace MVCSimpleCRM.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITaskRepository _taskRepository;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel taskVM)
        {
            if (ModelState.IsValid)
            {
                var task = new Tasks
                {
                    Title = taskVM.Title,
                    Description = taskVM.Description,
                    Status = taskVM.Status,
                    CreatorStatus = taskVM.CreatorStatus,
                    CreateDate = taskVM.CreateDate,
                    DueDate = taskVM.DueDate,
                    IDUserCreate = ""
                };
                _taskRepository.Add(task);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error");
            }

            return View(taskVM);
        }
    }
}
