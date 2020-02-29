using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public ToDoTasksController(ITaskRepository repository)
        {
            _taskRepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ToDoTask), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ToDoTask>>> Get()
        {
            return await _taskRepository.GetAllTasksAsync();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, ToDoTask toDoTask)
        {
            if (id != toDoTask.Id)
            {
                return BadRequest();
            }

            var exists = await _taskRepository.TaskExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            await _taskRepository.UpdateAsync(toDoTask);

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ToDoTask), StatusCodes.Status201Created)]
        public async Task<ActionResult<ToDoTask>> Post(ToDoTask toDoTask)
        {
            await _taskRepository.AddTask(toDoTask);

            return CreatedAtAction("Get", new { id = toDoTask.Id }, toDoTask);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            var taskExists = await _taskRepository.TaskExistsAsync(id);
            if (!taskExists)
            {
                return NotFound();
            }

            await _taskRepository.RemoveTaskAsync(id);
            return NoContent();
        }
    }
}
