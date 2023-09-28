using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Interfaces
{
    public class IAccountRepository
    {
        Task<IEnumerable<AspNetUsers>> GetAll();

        Task<AspNetUsers> GetByIdAsync(int id);
        Task<IEnumerable<AspNetUsers>> GetUserByLogin(string username);

        /*bool Add(Users user);
        bool Update(Users user);
        bool Delete(Users user);
        //bool Edit(Users user);
        bool Save();*/
    }
}
