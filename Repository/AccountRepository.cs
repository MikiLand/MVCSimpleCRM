using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;
using MVCSimpleCRM.ViewModels;
using System.Linq;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            return await _context.aspNetUsers.OrderBy(x => x.UserName).ToListAsync();
        }

        public async Task<List<AspNetUsers>> GetAllList()
        {
            return await _context.aspNetUsers.OrderBy(x => x.UserName).ToListAsync();
        }

        public async Task<AspNetUsers> GetByIdAsync(string Id)
        {
            return await _context.aspNetUsers.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        
        /*public async Task<DetailAccountViewModel> GetByIdVMAsync(string Id)
        {
            return await _context.aspNetUsers.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }*/

        public async Task<AspNetUsers> GetByIdAsyncNoTracking(int id)
        {
            return _context.aspNetUsers.FirstOrDefault();
        }

        public async Task<AspNetUsers> GetUserByUserName(string username)
        {
            return await _context.aspNetUsers.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<IEnumerable<AspNetUsers>> GetUserByLogin(string username)
        {
            return await _context.aspNetUsers.Where(u => u.UserName.Contains(username)).ToListAsync();
        }

        public async Task<IList<AspNetUsers>> GetUserByLoginList(string username)
        {
            return await _context.aspNetUsers.Where(u => u.UserName.Contains(username)).ToListAsync();
        }

        public async Task<List<AspNetUsers>> RefreshAccounts(string SearchedAccount, int Page)
        {
            return await _context.aspNetUsers.Where(x => x.UserName.Contains(SearchedAccount) || x.Name.Contains(SearchedAccount) || x.Surname.Contains(SearchedAccount)).Skip((Page - 1) * 8).Take(8).ToListAsync();
        }

        public async Task<int> RefreshAccountsCount(string SearchedAccount, int Page)
        {
            return await _context.aspNetUsers.Where(x => x.UserName.Contains(SearchedAccount) || x.Name.Contains(SearchedAccount) || x.Surname.Contains(SearchedAccount)).CountAsync();
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

        public async Task<List<AspNetUsers>> GetSearchedUsers(string SearchedUser)
        {
            if (SearchedUser is null)
                return await _context.aspNetUsers.ToListAsync();
            return await _context.aspNetUsers.Where(u => u.UserName.Contains(SearchedUser) || u.Name.Contains(SearchedUser) || u.Surname.Contains(SearchedUser)).ToListAsync();
        }
    }
}
