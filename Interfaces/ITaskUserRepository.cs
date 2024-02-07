using MVCSimpleCRM.Data;
using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Interfaces
{
    public interface ITaskUserRepository
    {
        bool Add(TaskUsers taskUser);
        bool Update(TaskUsers taskUser);
        bool Delete(TaskUsers taskUser);
        bool Save();

        Task<TaskUsers> GetTaskUserByID(string IDUser, int IDTask);
        Task<List<TaskUsers>> GetAllTaskUsersAttachedToTask(int TaskID);
        Task<List<int>> GetTopUserAttachedTasks(string userId);
        //Task<IEnumerable<AspNetUsers>> GetAllUsersAttachedToTask(string UserID);
    }
}
