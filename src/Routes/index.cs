using Microsoft.AspNetCore.Builder;
using TaskTracker.Controllers;

namespace TaskTracker.Routing
{
    public static class IndexRoutes
    {
        public static void MapRoutes(WebApplication app)
        {
            var controller = new IndexController();
            app.MapGet("/", () => controller.Index());
        }
    }
}