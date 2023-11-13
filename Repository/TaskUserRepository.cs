using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Repository
{
    public class TaskUserRepository
    {
        private readonly ApplicationDbContext _context;

        public async Task<IEnumerable<TaskUsers>> GetAllUsersAttachedToTask(int TaskID)
        {
            return await _context.taskUsers.Where(x => x.IdTask == TaskID).Take(3).ToListAsync();
        }
    }
}
