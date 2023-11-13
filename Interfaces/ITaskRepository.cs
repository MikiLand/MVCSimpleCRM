using MVCSimpleCRM.Models;
using MVCSimpleCRM.ViewModels;

namespace MVCSimpleCRM.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tasks>> GetAll();
        Task<IEnumerable<Tasks>> GetAllTasksCreatedByUser(string UserID);

        Task<Tasks> GetByIdAsync(int id);
        //Task<IEnumerable<UsersTasks> GetUserByLogin(string login);

        bool Add(EditTaskViewModel task);
        bool Update(Tasks task);
        bool Delete(Tasks task);
        bool Save();
    }
}
