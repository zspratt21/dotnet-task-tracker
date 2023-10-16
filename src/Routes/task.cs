using Microsoft.AspNetCore.Builder;
using TaskTracker.Controllers;

namespace TaskTracker.Routing
{
    public static class TaskRoutes
    {
        public static void MapRoutes(WebApplication app)
        {
            var controller = new TaskController();
        }
    }
}