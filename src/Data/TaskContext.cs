using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;

namespace TaskTracker.Data;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options) : base(options)
    {
    }

    public DbSet<TaskItem> Tasks { get; set; }
}