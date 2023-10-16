using Microsoft.AspNetCore.Builder;
using DotEnv.Core;
using TaskTracker.Routing;

new EnvLoader()
    .AddEnvFile("../env/.env")
    .Load();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

IndexRoutes.MapRoutes(app);
UserRoutes.MapRoutes(app);
TaskRoutes.MapRoutes(app);
    
app.Run("http://"+EnvReader.Instance["HOST"]+":"+EnvReader.Instance["PORT"]+"");