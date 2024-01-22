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
        //Task<IEnumerable<AspNetUsers>> GetAllUsersAttachedToTask(string UserID);
    }
}
