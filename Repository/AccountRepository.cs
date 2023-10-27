using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;
using System.Security.Claims;

namespace MVCSimpleCRM.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        /*public bool Add(Users user)
        {
            _context.Add(user);
            return Save();
        }*/

        public bool Update(AspNetUsers account)
        {
            _context.Update(account);
            return Save();
        }

        public bool Delete(AspNetUsers account)
        {
            _context.Remove(account);
            return Save();
        }

        public async Task<IEnumerable<AspNetUsers>> GetAll()
        {
            return await _context.aspNetUsers.ToListAsync();
        }

        public async Task<AspNetUsers> GetByIdAsync(string Id)
        {
            return await _context.aspNetUsers.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<AspNetUsers> GetByIdAsyncNoTracking(int id)
        {
            return _context.aspNetUsers.FirstOrDefault();
            //return await _context.accounts.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<AspNetUsers>> GetUserByLogin(string username)
        {
            //return await _context.users.Where(u => u.Login == login).ToListAsync();
            //return await _context.users.Where(u => u.Login.Contains(login) && u.Login.StartsWith("aaa")).ToListAsync();
            return await _context.aspNetUsers.Where(u => u.UserName.Contains(username)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<List<Tasks>> GetAllUserCreatedTasks()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userTasks = _context.tasks.Where(r => r.IDUserCreate == curUser);
            return userTasks.ToList();
        }
    }
}
