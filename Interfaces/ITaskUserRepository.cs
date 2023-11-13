using MVCSimpleCRM.Data;
using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Interfaces
{
    public interface ITaskUserRepository
    {
        Task<IEnumerable<AspNetUsers>> GetAllUsersAttachedToTask(string UserID);
    }
}
