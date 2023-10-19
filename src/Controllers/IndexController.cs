using Microsoft.AspNetCore.Mvc;

namespace TaskTracker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IndexController : ControllerBase
{
    [HttpGet]
    public String Get()
    {
        return "Welcome to the Task Tracking API! Use this API to create new tasks and mark them as complete when you finish them.";
    }
}
