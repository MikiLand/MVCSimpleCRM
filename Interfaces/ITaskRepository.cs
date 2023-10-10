using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tasks>> GetAll();

        Task<Tasks> GetByIdAsync(int id);
        //Task<IEnumerable<UsersTasks> GetUserByLogin(string login);

        bool Add(Tasks task);
        bool Update(Tasks task);
        bool Delete(Tasks task);
        //bool Edit(Tasks task);
        bool Save();
    }
}
