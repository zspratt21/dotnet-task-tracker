using Microsoft.EntityFrameworkCore;
using TaskTrackerAPI.Models;
using DotEnv.Core;

new EnvLoader()
    .AddEnvFile("../env/.env")
    .Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// @todo replace hardcoded variables with env variables.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TaskContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32))));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://"+EnvReader.Instance["HOST"]+":"+EnvReader.Instance["PORT"]+"");
