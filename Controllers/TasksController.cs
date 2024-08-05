using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TodoContext _context;

        public TasksController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTasks()
        {
            return Ok(_context.TodoItems.ToList());
        }

        [HttpPost]
        public ActionResult<TodoItem> AddTask([FromBody] TodoItem newTask)
        {
            var task = new TodoItem { Description = newTask.Description };
            _context.TodoItems.Add(task);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(task);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
