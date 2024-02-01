using MVCSimpleCRM.Models;
using MVCSimpleCRM.ViewModels;

namespace MVCSimpleCRM.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tasks>> GetAll();
        Task<IEnumerable<Tasks>> GetAllTasksCreatedByUser(string UserID);
        Task<IEnumerable<Tasks>> GetByTitleLike(string TitleLike);
        Task<Tasks> GetByIdAsync(int id);
        Task<IEnumerable<Tasks>> RefreshTasks(string SearchedTaskTitle, int SortBy, DateTime DateFrom, DateTime DateTo, string DateType);
        //Task<IEnumerable<UsersTasks> GetUserByLogin(string login);

        //bool Add(EditTaskViewModel task);
        bool Add(Tasks task);
        bool Update(Tasks task);
        bool Delete(Tasks task);
        bool Save();
    }
}
