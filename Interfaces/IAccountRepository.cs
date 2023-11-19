using MVCSimpleCRM.Models;
using System.Diagnostics;

namespace MVCSimpleCRM.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<AspNetUsers>> GetAll();

        Task<List<Tasks>> GetAllUserCreatedTasks();

        Task<AspNetUsers> GetByIdAsync(string id);
        Task<IEnumerable<AspNetUsers>> GetUserByLogin(string username);
        Task<IList<AspNetUsers>> GetUserByLoginList(string username);

        //bool Add(AspNetUsers account);
        bool Update(AspNetUsers account);
        bool Delete(AspNetUsers account);
        //Task GetByIdAsync(string id);
        //bool Edit(Users user);
        //bool Save();
    }
}
