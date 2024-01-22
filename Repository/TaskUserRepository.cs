using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Repository
{
    public class TaskUserRepository : ITaskUserRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(TaskUsers taskUser)
        {
            _context.Add(taskUser);
            return Save();
        }

        public bool Delete(TaskUsers taskUser)
        {
            _context.Remove(taskUser);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(TaskUsers taskUser)
        {
            _context.Update(taskUser);
            return Save();
        }

        public async Task<IEnumerable<TaskUsers>> GetAllUsersAttachedToTask(int TaskID)
        {
            return await _context.taskUsers.Where(x => x.IdTask == TaskID).Take(3).ToListAsync();
        }
    }
}
