﻿using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;
using MVCSimpleCRM.ViewModels;
using System.Linq;
using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCSimpleCRM.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Tasks task)
        {
            _context.Add(task);
            return Save();
        }

        public bool Delete(Tasks task)
        {
            _context.Remove(task);
            return Save();
        }

        public async Task<IEnumerable<Tasks>> GetAll()
        {
            return await _context.tasks.ToListAsync();
        }

        public async Task<List<Tasks>> GetAllList()
        {
            return await _context.tasks.ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetByTitleLike(string TitleLike)
        {
            return await _context.tasks.Where(x => x.Title.Contains("Title")).ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetAllTasksCreatedByUser(string UserID)
        {
            return await _context.tasks.Where(x => x.IDUserCreate == UserID).Take(3).ToListAsync();
        }

        public async Task<Tasks> GetByIdAsync(int Id)
        {
            return await _context.tasks.Where(x => x.Id == Id).FirstOrDefaultAsync();
            //return await _context.users.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Tasks> GetByIdAsyncNoTracking(int id)
        {
            return _context.tasks.FirstOrDefault();
            //return await _context.users.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Tasks task)
        {
            _context.Update(task);
            return Save();
        }
        public async Task<List<Tasks>> RefreshTasks2(string SearchedTaskTitle, int SortBy, DateTime DateFrom, DateTime DateTo, string DateType, List<AspNetUsersIndexViewModel> UsersList)
        {
            List<String> UsersIDList = new List<String>();

            if (SearchedTaskTitle is null)
                SearchedTaskTitle = string.Empty;

            if(UsersList is not null)
            {
                foreach (var user in UsersList)
                {
                    UsersIDList.Add(user.Id);
                }
            }

            switch (DateType)
            {
                case "Utworzenia":
                    switch (SortBy)
                    {
                        case 1:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle) && UsersIDList.Contains(x.IDUserCreate)).OrderByDescending(x => x.DueDate).ToListAsync();
                        case 2:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle) && UsersIDList.Contains(x.IDUserCreate)).OrderBy(x => x.DueDate).ToListAsync();
                        case 3:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle) && UsersIDList.Contains(x.IDUserCreate)).OrderByDescending(x => x.CreateDate).ToListAsync();
                        case 4:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle) && UsersIDList.Contains(x.IDUserCreate)).OrderBy(x => x.CreateDate).ToListAsync();
                        case 5:
                            return await _context.tasks.ToListAsync();
                        case 6:
                            return await _context.tasks.ToListAsync();
                        default:
                            return await _context.tasks.ToListAsync();
                    }
                case "Przypomnienia":
                    switch (SortBy)
                    {
                        case 1:
                            return await _context.tasks.ToListAsync();
                        case 2:
                            return await _context.tasks.ToListAsync();
                        case 3:
                            return await _context.tasks.ToListAsync();
                        case 4:
                            return await _context.tasks.ToListAsync();
                        case 5:
                            return await _context.tasks.ToListAsync();
                        case 6:
                            return await _context.tasks.ToListAsync();
                        default:
                            return await _context.tasks.ToListAsync();
                    }
                case "Aktualizacji":
                    switch (SortBy)
                    {
                        case 1:
                            return await _context.tasks.ToListAsync();
                        case 2:
                            return await _context.tasks.ToListAsync();
                        case 3:
                            return await _context.tasks.ToListAsync();
                        case 4:
                            return await _context.tasks.ToListAsync();
                        case 5:
                            return await _context.tasks.ToListAsync();
                        case 6:
                            return await _context.tasks.ToListAsync();
                        default:
                            return await _context.tasks.ToListAsync();
                    }
                case "Zakończenia":
                    switch (SortBy)
                    {
                        case 1:
                            return await _context.tasks.Where(x => x.DueDate >= DateFrom && x.DueDate <= DateTo && x.Title.Contains(SearchedTaskTitle) && UsersIDList.Contains(x.IDUserCreate)).OrderBy(x => x.CreateDate).ToListAsync();
                        case 2:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle) && UsersIDList.Contains(x.IDUserCreate)).OrderByDescending(x => x.DueDate).ToListAsync();
                        case 3:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle) && UsersIDList.Contains(x.IDUserCreate)).OrderByDescending(x => x.DueDate).ToListAsync();
                        case 4:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle) && UsersIDList.Contains(x.IDUserCreate)).OrderByDescending(x => x.DueDate).ToListAsync();
                        case 5:
                            return await _context.tasks.ToListAsync();
                        case 6:
                            return await _context.tasks.ToListAsync();
                        default:
                            return await _context.tasks.ToListAsync();
                    }
            }

            switch (SortBy)
            {
                case 1:
                    return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle)).OrderBy(x => x.CreateDate).ToListAsync();
                case 2:
                    return await _context.tasks.ToListAsync();
                case 3:
                    return await _context.tasks.ToListAsync();
                case 4:
                    return await _context.tasks.ToListAsync();
                case 5:
                    return await _context.tasks.ToListAsync();
                case 6:
                    return await _context.tasks.ToListAsync();
                default:
                    return await _context.tasks.ToListAsync();
            }
        }

        public async Task<List<Tasks>> RefreshTasks(string SearchedTaskTitle, int SortBy, DateTime DateFrom, DateTime DateTo, string DateType)
        {
            if(SearchedTaskTitle is null)
                SearchedTaskTitle = string.Empty;

            switch (DateType)
            {
                case "Utworzenia":
                    switch (SortBy)
                    {
                        case 1:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle)).OrderByDescending(x => x.DueDate).ToListAsync();
                        case 2:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle)).OrderBy(x => x.DueDate).ToListAsync();
                        case 3:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle)).OrderByDescending(x => x.CreateDate).ToListAsync();
                        case 4:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle)).OrderBy(x => x.CreateDate).ToListAsync();
                        case 5:
                            return await _context.tasks.ToListAsync();
                        case 6:
                            return await _context.tasks.ToListAsync();
                        default:
                            return await _context.tasks.ToListAsync();
                    }
                case "Przypomnienia":
                    switch (SortBy)
                    {
                        case 1:
                            return await _context.tasks.ToListAsync();
                        case 2:
                            return await _context.tasks.ToListAsync();
                        case 3:
                            return await _context.tasks.ToListAsync();
                        case 4:
                            return await _context.tasks.ToListAsync();
                        case 5:
                            return await _context.tasks.ToListAsync();
                        case 6:
                            return await _context.tasks.ToListAsync();
                        default:
                            return await _context.tasks.ToListAsync();
                    }
                case "Aktualizacji":
                    switch (SortBy)
                    {
                        case 1:
                            return await _context.tasks.ToListAsync();
                        case 2:
                            return await _context.tasks.ToListAsync();
                        case 3:
                            return await _context.tasks.ToListAsync();
                        case 4:
                            return await _context.tasks.ToListAsync();
                        case 5:
                            return await _context.tasks.ToListAsync();
                        case 6:
                            return await _context.tasks.ToListAsync();
                        default:
                            return await _context.tasks.ToListAsync();
                    }
                case "Zakończenia":
                    switch (SortBy)
                    {
                        case 1:
                            return await _context.tasks.Where(x => x.DueDate >= DateFrom && x.DueDate <= DateTo && x.Title.Contains(SearchedTaskTitle)).OrderBy(x => x.CreateDate).ToListAsync();
                        case 2:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle)).OrderByDescending(x => x.DueDate).ToListAsync();
                        case 3:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle)).OrderByDescending(x => x.DueDate).ToListAsync();
                        case 4:
                            return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle)).OrderByDescending(x => x.DueDate).ToListAsync();
                        case 5:
                            return await _context.tasks.ToListAsync();
                        case 6:
                            return await _context.tasks.ToListAsync();
                        default:
                            return await _context.tasks.ToListAsync();
                    }
            }

            switch (SortBy)
            {
                case 1:
                    return await _context.tasks.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo && x.Title.Contains(SearchedTaskTitle)).OrderBy(x => x.CreateDate).ToListAsync();
                case 2:
                    return await _context.tasks.ToListAsync();
                case 3:
                    return await _context.tasks.ToListAsync();
                case 4:
                    return await _context.tasks.ToListAsync();
                case 5:
                    return await _context.tasks.ToListAsync();
                case 6:
                    return await _context.tasks.ToListAsync();
                default:
                    return await _context.tasks.ToListAsync();
            }
        }

        public async Task<List<Tasks>> GetTopTasksCreatedByUser(string userId, int TasksAmount)
        {
            return await _context.tasks.Where(r => r.IDUserCreate == userId).OrderByDescending(x => x.CreateDate).Take(TasksAmount).ToListAsync();
        }

        public async Task<List<Tasks>> GetTopUserTasks(List<int> tasksIDList, int TasksAmount)
        {
            return await _context.tasks.Where(x => tasksIDList.Contains(x.Id)).OrderBy(x => x.CreateDate).Take(TasksAmount).ToListAsync();
        }
    }
}