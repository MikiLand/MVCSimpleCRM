using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAll();

        Task<Users> GetByIdAsync(int id);
        Task<IEnumerable<Users>> GetUserByLogin(string login);

        bool Add(Users user);
        bool Update(Users user);
        bool Delete(Users user);
        //bool Edit(Users user);
        bool Save();
    }
}
