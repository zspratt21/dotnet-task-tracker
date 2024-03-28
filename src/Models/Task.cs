using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Models;

public class TaskItem
{
    public int Id { get; set; }
    
    [Column(TypeName = "nvarchar(255)")]
    public string Name { get; set; }  
    
    [Column(TypeName = "nvarchar(max)")]
    public string Description { get; set; }
    
    public bool IsCompleted { get; set; }
}