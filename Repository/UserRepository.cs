using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Interfaces;
using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Users user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Delete(Users user)
        {
            _context.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _context.users.ToListAsync();
        }

        public async Task<Users> GetByIdAsync(int id)
        {
            return await _context.users.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Users> GetByIdAsyncNoTracking(int id)
        {
            return await _context.users.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Users>> GetUserByLogin(string login)
        {
            //return await _context.users.Where(u => u.Login == login).ToListAsync();
            //return await _context.users.Where(u => u.Login.Contains(login) && u.Login.StartsWith("aaa")).ToListAsync();
            return await _context.users.Where(u => u.Login.Contains(login)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Users user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
