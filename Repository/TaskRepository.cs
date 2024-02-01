﻿using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;
using MVCSimpleCRM.ViewModels;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Tasks>> RefreshTasks(string SearchedTaskTitle, int SortBy, DateTime DateFrom, DateTime DateTo)
        {
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
    }
}
