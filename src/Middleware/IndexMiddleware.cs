using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace app.Middlewares
{
    public class IndexMiddleware
    {
        private readonly RequestDelegate _next;

        public IndexMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value == "/")
            {
                context.Response.Redirect("/api/Index");
                return;
            }

            await _next(context);
        }
    }
}