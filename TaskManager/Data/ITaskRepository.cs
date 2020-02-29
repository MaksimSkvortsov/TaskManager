using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Data
{
    public interface ITaskRepository
    {
        Task<List<ToDoTask>> GetAllTasksAsync();

        Task<bool> TaskExistsAsync(int id);

        Task AddTask(ToDoTask task);

        Task UpdateAsync(ToDoTask task);

        Task RemoveTaskAsync(int id);
    }
}
