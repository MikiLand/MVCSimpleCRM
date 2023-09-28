using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<AspNetUsers>> GetAll();

        Task<AspNetUsers> GetByIdAsync(int id);
        Task<IEnumerable<AspNetUsers>> GetUserByLogin(string username);

        /*bool Add(AspNetUsers account);
        bool Update(AspNetUsers account);
        bool Delete(AspNetUsers account);
        //bool Edit(Users user);
        bool Save();*/
    }
}
