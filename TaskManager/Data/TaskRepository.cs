using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Data
{
    public class TaskRepository : ITaskRepository, IDisposable
    {
        private readonly TaskManagerContext _context;


        public TaskRepository(TaskManagerContext context)
        {
            _context = context;
        }


        public Task<List<ToDoTask>> GetAllTasksAsync()
        {
            return _context.Tasks.AsNoTracking().ToListAsync();
        }

        public Task<bool> TaskExistsAsync(int id)
        {
            return _context.Tasks.AnyAsync(t => t.Id == id);
        }

        public Task AddTask(ToDoTask task)
        {
            _context.Tasks.Add(task);

            return _context.SaveChangesAsync();
        }

        public Task RemoveTaskAsync(int id)
        {
            _context.Tasks.Remove(new ToDoTask { Id = id });

            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(ToDoTask task)
        {
            _context.Update(task);
            return _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
