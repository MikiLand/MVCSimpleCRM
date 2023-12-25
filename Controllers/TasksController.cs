using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;
using MVCSimpleCRM.Repository;
using MVCSimpleCRM.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCSimpleCRM.Controllers
{
    public class TasksController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly ITaskRepository _taskRepository;
        private readonly IAccountRepository _accountRepository;
        public IList<AspNetUsers> AddUserToTask;
        public List<string> UsersAddedToTask; 

        public TasksController(ITaskRepository taskRepository, IAccountRepository accountRepository)
        {
            //_context = context;
            this._taskRepository = taskRepository;
            this._accountRepository = accountRepository;
        }

        /*public IActionResult Index()
        {
            List<Tasks> tasks = _context.tasks.ToList();
            List<Users> users = _context.users.ToList();
            return View(tasks);
        }*/

        public async Task<IActionResult> Index()
        {
            //List<Users> users = _context.users.ToList();
            IEnumerable<Tasks> tasks = await _taskRepository.GetAll();
            return View(tasks);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Tasks task = await _taskRepository.GetByIdAsync(id);
            return View(task);
        }

        public async Task<JsonResult> MyJson(string SearchedTitle)
        {
            IEnumerable<Tasks> tasks = await _taskRepository.GetAll();

            //Task test = _taskRepository.GetAll();

            var task = new EditTaskViewModel
            {
                Title = "Test",
                Description = "Test",
                Status = "Test",
                CreatorStatus = "Test",
                CreateDate = DateTime.Now,
                DueDate = DateTime.Now,
                IDUserCreate = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Users = Enumerable.Empty<AspNetUsers>(),
                //Users = await _accountRepository.GetAll()
                //AddedUsers = await _taskUsersRepository.GetAllUserAddedToTask()
            };


            return Json(tasks);
            //return Json("TTTEST");
        }

        public async Task<IActionResult> Create()
        {
            //IList<object> list = null;
            //var test = (await _accountRepository.GetUserByLogin("")).ToList();
            //AddUserToTask = await _accountRepository.GetByIdAsync("4673c067-0dc8-4c61-952a-46a5b91adfcd");
            AddUserToTask = (await _accountRepository.GetUserByLogin("qwertyuio")).ToList();
            //AddUserToTask.Add(await _accountRepository.GetByIdAsync("4673c067-0dc8-4c61-952a-46a5b91adfcd"));
            //AddUserToTask.Add(await _accountRepository.GetByIdAsync("a53989d9-f8c0-42ba-b2ff-9acbcc168dec"));
            //AddUserToTask.Add(await _accountRepository.GetByIdAsync("4673c067-0dc8-4c61-952a-46a5b91adfcd"));
            //var test = (await _accountRepository.GetByIdAsync("4673c067-0dc8-4c61-952a-46a5b91adfcd"));
            //list.Add(test);
            //if(test == null)
            //{

            //}
            //else
            //{
            //    AddUserToTask.Add(test);
            //}

            var tasksVM = new EditTaskViewModel
            {
                Users = await _accountRepository.GetAll(),
                AddedUsers = Enumerable.Empty<AspNetUsers>(),
                AddedUsersList = AddUserToTask
            };
            return View(tasksVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EditTaskViewModel taskVM)
        {
            if (ModelState.IsValid)
            {
                var task = new EditTaskViewModel
                {
                    Title = taskVM.Title,
                    Description = taskVM.Description,
                    Status = taskVM.Status,
                    CreatorStatus = taskVM.CreatorStatus,
                    CreateDate = taskVM.CreateDate,
                    DueDate = taskVM.DueDate,
                    IDUserCreate = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Users = await _accountRepository.GetAll()
                    //AddedUsers = await _taskUsersRepository.GetAllUserAddedToTask()
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

        public async Task<IActionResult> AddUserToModel2(EditTaskViewModel taskVM)
        {
            AddUserToTask = (await _accountRepository.GetUserByLogin("qwertyuio")).ToList();
            AddUserToTask.Add(await _accountRepository.GetByIdAsync("4673c067-0dc8-4c61-952a-46a5b91adfcd"));

            var task = new EditTaskViewModel
            {
                Users = await _accountRepository.GetAll(),
                //AddedUsers = (IEnumerable<AspNetUsers>)list
                AddedUsers = Enumerable.Empty<AspNetUsers>(),
                AddedUsersList = AddUserToTask
            };
            return View(task);
        }

        public async Task<IActionResult> AddUserToModel(EditTaskViewModel taskVM)
        {
            AddUserToTask.Add(await _accountRepository.GetByIdAsync("4673c067-0dc8-4c61-952a-46a5b91adfcd"));

            var task = new EditTaskViewModel
            {
                Title = taskVM.Title,
                Description = taskVM.Description,
                Status = taskVM.Status,
                CreatorStatus = taskVM.CreatorStatus,
                CreateDate = taskVM.CreateDate,
                DueDate = taskVM.DueDate,
                IDUserCreate = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Users = await _accountRepository.GetAll(),
                AddedUsers = Enumerable.Empty<AspNetUsers>(),
                AddedUsersList = AddUserToTask
            };
            return View(taskVM);
        }

        public async Task<IActionResult> AddUserToList(string UserLogin, EditTaskViewModel taskVM)
        {
            AddUserToTask.Add((AspNetUsers)await _accountRepository.GetUserByLogin(UserLogin));
            //AddUserToTask.Add(await _accountRepository.GetUserByLogin(UserID)); ;
            return View(taskVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return View("Error");
            var taskVM = new EditTaskViewModel
            {
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CreatorStatus = task.CreatorStatus,
                CreateDate = task.CreateDate,
                DueDate = task.DueDate,
                IDUserCreate = task.IDUserCreate
            };
            return View(taskVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTaskViewModel taskVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Edycja nieudana!");
                return View("Edit", taskVM);
            }

            var task = new Tasks
            {
                Id = id,
                Title = taskVM.Title,
                Description = taskVM.Description,
                Status = taskVM.Status,
                CreatorStatus = taskVM.CreatorStatus,
                CreateDate = taskVM.CreateDate,
                DueDate = taskVM.DueDate,
                IDUserCreate = taskVM.IDUserCreate
            };

            _taskRepository.Update(task);

            return RedirectToAction("Detail", "Tasks", new { id = task.Id });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var taskDetails = await _taskRepository.GetByIdAsync(id);
            if (taskDetails == null)
            {
                TempData["Result"] = "ERROR";
                return View("Error");
            }
            _taskRepository.Delete(taskDetails);
            TempData["Result"] = "OK";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true)

            return RedirectToAction("Index");
        }
    }
}
