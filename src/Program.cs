using Microsoft.AspNetCore.Builder;
using DotEnv.Core;

new EnvLoader()
    .AddEnvFile("../env/.env")
    .Load();

var env = new EnvReader();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello world!!!");

app.Run("http://"+env["HOST"]+":"+env["PORT"]+"");