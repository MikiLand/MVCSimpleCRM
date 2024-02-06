using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;
using MVCSimpleCRM.Repository;
using MVCSimpleCRM.ViewModels;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        private readonly ITaskUserRepository _taskUserRepository;
        //public IList<AspNetUsers> AddUserToTask;
        //public List<string> UsersAddedToTask; 

        public TasksController(ITaskRepository taskRepository, IAccountRepository accountRepository, ITaskUserRepository taskUserRepository)
        {
            //_context = context;
            this._taskRepository = taskRepository;
            this._accountRepository = accountRepository;
            this._taskUserRepository = taskUserRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<AspNetUsers> UsersList = await _accountRepository.GetAllList();

            var tasksVM = new IndexTaskViewModel
            {
                Tasks = await _taskRepository.GetAllList(),
                Users = new List<AspNetUsersIndexViewModel> { },
            };

            foreach (var User in UsersList)
            {
                var UserIndexVM = new AspNetUsersIndexViewModel
                {
                    Id = User.Id,
                    UserName = User.UserName,
                    Name = User.Name,
                    Surname = User.Surname,
                    Email = User.Email,
                    PasswordHash = User.PasswordHash,
                    CreateDate = User.CreateDate,
                    IsChecked = false
                };

                tasksVM.Users.Add(UserIndexVM);
            }


            IEnumerable<Tasks> tasks = await _taskRepository.GetAll();
            HttpContext.Session.SetString("ActualTasksModel", JsonConvert.SerializeObject(tasks));

            HttpContext.Session.SetString("ActualSearchedUsersModel", JsonConvert.SerializeObject(tasksVM));

            //_accountRepository.GetSearchedUsers("");
            return View(tasksVM);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Tasks task = await _taskRepository.GetByIdAsync(id);

            var taskVM = new EditTaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CreatorStatus = task.CreatorStatus,
                CreateDate = task.CreateDate,
                DueDate = task.DueDate,
                IDUserCreate = task.IDUserCreate,
                //TaskPositionUsers = new List<TaskUsers> { }
                TaskPositionUsers = await _taskUserRepository.GetAllTaskUsersAttachedToTask(task.Id)
            };

            var TaskVM2 = new EditTaskViewModel2
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CreatorStatus = task.CreatorStatus,
                CreateDate = task.CreateDate,
                DueDate = task.DueDate,
                IDUserCreate = task.IDUserCreate,
                TaskPositionUsers = new List<TaskUserViewModel> { },
                AvileableUsers = new List<AspNetUsers> { }
            };

            foreach (var User in taskVM.TaskPositionUsers)
            {
                AspNetUsers UserVM = await _accountRepository.GetByIdAsync(User.IdUser);


                var TaskUserViewModelVM = new TaskUserViewModel
                {
                    IdTask = User.IdTask,
                    IdUser = User.IdUser,
                    UserName = UserVM.UserName,
                    Name = UserVM.Name,
                    Surname = UserVM.Surname
                };

                TaskVM2.TaskPositionUsers.Add(TaskUserViewModelVM);
            }

            return View(TaskVM2);
        }

        public async Task<IActionResult> Create()
        {
            var tasksVM = new EditTaskViewModel2
            {
                TaskPositionUsers = new List<TaskUserViewModel> { },
                AvileableUsers = await _accountRepository.GetAllList()
            };

            HttpContext.Session.SetString("ActualModel", JsonConvert.SerializeObject(tasksVM));
            return View(tasksVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EditTaskViewModel2 taskVM)
        {
            var ActualModel = HttpContext.Session.GetString("ActualModel");

            //Code need to be eliminated as returned model isn't. Another method of model veryfication need to be implemented            
            //if (ModelState.IsValid)
            //{
            var task = new Tasks
                {
                    Title = taskVM.Title,
                    Description = taskVM.Description,
                    Status = taskVM.Status,
                    CreatorStatus = taskVM.CreatorStatus,
                    CreateDate = taskVM.CreateDate,
                    DueDate = taskVM.DueDate,
                    IDUserCreate = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _taskRepository.Add(task);

                if (ActualModel is not null)
                {
                    EditTaskViewModel2 ActualTaskVM = JsonConvert.DeserializeObject<EditTaskViewModel2>(ActualModel);

                    foreach (var NewUserList in ActualTaskVM.TaskPositionUsers)
                    {
                        AspNetUsers NewUser = await _accountRepository.GetByIdAsync(NewUserList.IdUser);
                        if (NewUser != null)
                        {
                            TaskUsers NewTaskUser = new TaskUsers
                            {
                                IdTask = ActualTaskVM.Id,
                                IdUser = NewUser.Id
                            };
                            _taskUserRepository.Add(NewTaskUser);
                        }
                    }
                }

                return RedirectToAction("Index");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Error");
            //}
            //return View(taskVM);
        }

        /*public void AddUsersToTasks(EditTaskViewModel taskVM)
        {
            foreach (var User in taskVM.TaskPositionUsers)
            {
                _taskUserRepository.Add(User);
            }
        }*/

        public async Task<IActionResult> Edit(int id)
        {
            Tasks task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return View("Error");
            var taskVM = new EditTaskViewModel
            {
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CreatorStatus = task.CreatorStatus,
                CreateDate = task.CreateDate,
                DueDate = task.DueDate,
                IDUserCreate = task.IDUserCreate,
                //TaskPositionUsers = new List<TaskUsers> { }
                TaskPositionUsers = await _taskUserRepository.GetAllTaskUsersAttachedToTask(task.Id)
            };

            var TaskVM2 = new EditTaskViewModel2
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CreatorStatus = task.CreatorStatus,
                CreateDate = task.CreateDate,
                DueDate = task.DueDate,
                IDUserCreate = task.IDUserCreate,
                TaskPositionUsers = new List<TaskUserViewModel> { },
                AvileableUsers = await _accountRepository.GetAllList()
            };

            foreach (var User in taskVM.TaskPositionUsers)
            {
                AspNetUsers UserVM = await _accountRepository.GetByIdAsync(User.IdUser);


                var TaskUserViewModelVM = new TaskUserViewModel
                {
                    IdTask = User.IdTask,
                    IdUser = User.IdUser,
                    UserName = UserVM.UserName,
                    Name = UserVM.Name,
                    Surname = UserVM.Surname
                };

                TaskVM2.TaskPositionUsers.Add(TaskUserViewModelVM);
            }

            HttpContext.Session.SetString("ActualModel", JsonConvert.SerializeObject(TaskVM2));
            return View(TaskVM2);
        }

        [HttpGet]
        [Route("/tasks/RefreshAddUser")]
        public async Task<IActionResult> RefreshAddUser(string json, string AttachedUserName)
        {
            AspNetUsers UserVM = await _accountRepository.GetUserByUserName(AttachedUserName);
            EditTaskViewModel2 TaskVM = JsonConvert.DeserializeObject<EditTaskViewModel2>(json);
            var ActualModel = HttpContext.Session.GetString("ActualModel");

            if (ActualModel is not null)
            {
                TaskVM = JsonConvert.DeserializeObject<EditTaskViewModel2>(ActualModel);
            }

            var TaskUserViewModelVM = new TaskUserViewModel
            {
                IdTask = TaskVM.Id,
                IdUser = UserVM.Id,
                UserName = UserVM.UserName,
                Name = UserVM.Name,
                Surname = UserVM.Surname
            };

            foreach (var User in TaskVM.TaskPositionUsers)
            {
                if (AttachedUserName == User.UserName)
                {
                    return View(TaskVM);
                }
            }

            TaskVM.TaskPositionUsers.Add(TaskUserViewModelVM);

            HttpContext.Session.SetString("ActualModel", JsonConvert.SerializeObject(TaskVM));
            return PartialView("_TaskUsers", TaskVM);
        }

        [HttpGet]
        [Route("/tasks/RefreshRemoveUser")]
        public async Task<IActionResult> RefreshRemoveUser(string json, string AttachedUserName)
        {
            AspNetUsers UserVM = await _accountRepository.GetUserByUserName(AttachedUserName);
            EditTaskViewModel2 TaskVM = JsonConvert.DeserializeObject<EditTaskViewModel2>(json);
            var ActualModel = HttpContext.Session.GetString("ActualModel");

            if (ActualModel is not null)
            {
                TaskVM = JsonConvert.DeserializeObject<EditTaskViewModel2>(ActualModel);
            }

            var TaskUserViewModelVM = new TaskUserViewModel
            {
                IdTask = TaskVM.Id,
                IdUser = UserVM.Id,
                UserName = UserVM.UserName,
                Name = UserVM.Name,
                Surname = UserVM.Surname
            };

            TaskUserViewModel RemovedTUVM = null;

            for (int i = TaskVM.TaskPositionUsers.Count - 1; i >= 0; i--)
            {
                TaskUserViewModel User = TaskVM.TaskPositionUsers[i];

                if (AttachedUserName == User.UserName)
                {
                    RemovedTUVM = TaskVM.TaskPositionUsers[i];
                }
            }

            if(RemovedTUVM is not null)
            {
                TaskVM.TaskPositionUsers.Remove(RemovedTUVM);
            }

            HttpContext.Session.SetString("ActualModel", JsonConvert.SerializeObject(TaskVM));
            return PartialView("_TaskUsers", TaskVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTaskViewModel2 taskVM)
        {
            var ActualModel = HttpContext.Session.GetString("ActualModel");

            

            //Code need to be eliminated as returned model isn't. Another method of model veryfication need to be implemented
            /*if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Edycja nieudana!");
                return View("Edit", taskVM);
            }*/

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

            List<TaskUsers> TaskPositionUsersOld = await _taskUserRepository.GetAllTaskUsersAttachedToTask(task.Id);

            if (ActualModel is not null)
            {
                EditTaskViewModel2 ActualTaskVM = JsonConvert.DeserializeObject<EditTaskViewModel2>(ActualModel);

                //Removing user
                foreach (var OldUserList in TaskPositionUsersOld)
                {
                    AspNetUsers OldUser = await _accountRepository.GetByIdAsync(OldUserList.IdUser);
                    if (OldUser != null) 
                    {
                        bool NotDelete = false;
                        foreach (var NewUserList in ActualTaskVM.TaskPositionUsers) 
                        {
                            
                            AspNetUsers NewUser = await _accountRepository.GetByIdAsync(NewUserList.IdUser);
                            if (NewUser != null)
                            {
                                if(OldUser == NewUser)
                                {
                                    NotDelete = true;
                                }
                            }
                        }
                        if(NotDelete == false) 
                        {
                            _taskUserRepository.Delete(await _taskUserRepository.GetTaskUserByID(OldUser.Id, ActualTaskVM.Id));
                        }
                    }
                }

                //Adding user
                foreach(var NewUserList in ActualTaskVM.TaskPositionUsers)
                {
                    AspNetUsers NewUser = await _accountRepository.GetByIdAsync(NewUserList.IdUser);
                    if (NewUser != null)
                    {
                        bool NotAdd = false;
                        foreach(var OldUserList in TaskPositionUsersOld)
                        {
                            AspNetUsers OldUser = await _accountRepository.GetByIdAsync(OldUserList.IdUser);
                            if(OldUser != null)
                            {
                                if(NewUser == OldUser)
                                {
                                    NotAdd = true;
                                }
                            }
                        }
                        if(NotAdd == false)
                        {
                            TaskUsers NewTaskUser = new TaskUsers
                            {
                                IdTask = ActualTaskVM.Id,
                                IdUser = NewUser.Id
                            };
                            _taskUserRepository.Add(NewTaskUser);
                        }
                    }
                }
            }
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

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/tasks/RefreshTasks")]
        public async Task<IActionResult> RefreshTasks(string json, string SearchedTaskTitle, int SortBy, DateTime DateFrom, DateTime DateTo, string DateType)
        {
            var ActualModel = HttpContext.Session.GetString("ActualModel");

            var tasks = new IndexTaskViewModel
            {
                Tasks = await _taskRepository.RefreshTasks(SearchedTaskTitle, SortBy, DateFrom, DateTo, DateType)
            };

            return PartialView("_TasksIndex", tasks);
        }

        public async Task<IActionResult> RefreshTasks2(string SearchedTaskTitle, int SortBy, DateTime DateFrom, DateTime DateTo, string DateType, List<AspNetUsersIndexViewModel> UsersList)
        {
            var ActualSearchedUsersModel = HttpContext.Session.GetString("ActualSearchedUsersModel");

            IndexTaskViewModel ActualUsersVM = JsonConvert.DeserializeObject<IndexTaskViewModel>(ActualSearchedUsersModel);

            List<AspNetUsersIndexViewModel> SearchForUsers = new List<AspNetUsersIndexViewModel>();

            foreach (var User in ActualUsersVM.Users)
            {
                if (User.IsChecked == true)
                {
                    SearchForUsers.Add(User);
                }
            }

            if (SearchForUsers.Count == 0)
            {
                foreach (var User in ActualUsersVM.Users)
                {
                    SearchForUsers.Add(User);
                }
            }

            var tasks = new IndexTaskViewModel
            {
                Tasks = await _taskRepository.RefreshTasks2(SearchedTaskTitle, SortBy, DateFrom, DateTo, DateType, SearchForUsers)
            };

            return PartialView("_TasksIndex", tasks);
        }
    }
}
