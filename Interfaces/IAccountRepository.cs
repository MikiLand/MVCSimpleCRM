using MVCSimpleCRM.Models;
using MVCSimpleCRM.ViewModels;
using System.Diagnostics;

namespace MVCSimpleCRM.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<AspNetUsers>> GetAll();
        Task<List<AspNetUsers>> GetAllList();
        Task<List<Tasks>> GetAllUserCreatedTasks();
        Task<AspNetUsers> GetByIdAsync(string id);
        Task<AspNetUsers> GetUserByUserName(string username);
        Task<IEnumerable<AspNetUsers>> GetUserByLogin(string username);
        Task<IList<AspNetUsers>> GetUserByLoginList(string username);
        Task<List<AspNetUsers>> GetSearchedUsers(string SearchedUser);

        //bool Add(AspNetUsers account);
        bool Update(AspNetUsers account);
        bool Delete(AspNetUsers account);
        //Task GetByIdAsync(string id);
        //bool Edit(Users user);
        //bool Save();
    }
}
