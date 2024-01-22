using Microsoft.AspNetCore.Mvc;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;
using MVCSimpleCRM.ViewModels;
using System.Security.Claims;

namespace MVCSimpleCRM.Controllers
{
    public class TaskUsersController : Controller
    {
        private readonly ITaskUserRepository _taskUserRepository;

        public TaskUsersController(ITaskUserRepository taskUserRepository)
        {
            //_context = context;
            this._taskUserRepository = taskUserRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(TaskUsers taskUsersVM)
        {
            if (ModelState.IsValid)
            {
                var taskUsers = new TaskUsers
                {
                    IdTask = taskUsersVM.IdTask,
                    IdUser = taskUsersVM.IdUser
                };
                _taskUserRepository.Add(taskUsers);

                //AddUsersToTasks(taskVM);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error");
            }

            return View(taskUsersVM);
        }
    }
}
