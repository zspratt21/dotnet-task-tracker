using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;
using TaskTracker.Data;

namespace TaskTracker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly TaskContext _context;

    public TasksController(TaskContext context)
    {
        _context = context;
    }

    // GET: api/Tasks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
    {
        if (_context.Tasks == null) return NotFound();
        return await _context.Tasks.ToListAsync();
    }

    // GET: api/Tasks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
    {
        if (_context.Tasks == null) return NotFound();
        var taskItem = await _context.Tasks.FindAsync(id);

        if (taskItem == null) return NotFound();

        return taskItem;
    }

    // PUT: api/Tasks/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTaskItem(int id, TaskItem taskItem)
    {
        if (id != taskItem.Id) return BadRequest();

        _context.Entry(taskItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TaskItemExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/Tasks
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
    {
        if (taskItem == null) return BadRequest("Task item cannot be null.");

        if (_context.Tasks == null) return Problem("Entity set 'TaskContext.Tasks'  is null.");

        _context.Tasks.Add(taskItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTaskItem", new { id = taskItem.Id }, taskItem);
    }

    // DELETE: api/Tasks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskItem(int id)
    {
        if (_context.Tasks == null) return NotFound();
        var taskItem = await _context.Tasks.FindAsync(id);
        if (taskItem == null) return NotFound();

        _context.Tasks.Remove(taskItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TaskItemExists(int id)
    {
        return (_context.Tasks?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}