using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tasks>> GetAll();

        Task<Tasks> GetByIdAsync(int id);
        //Task<IEnumerable<UsersTasks> GetUserByLogin(string login);

        bool Add(Tasks user);
        bool Update(Tasks user);
        bool Delete(Tasks user);
        //bool Edit(Users user);
        bool Save();
    }
}
