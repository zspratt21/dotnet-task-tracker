using DotEnv.Core;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;

new EnvLoader()
    .Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
	.Replace("localhost", EnvReader.Instance["DB_HOST"])
    .Replace("3306", EnvReader.Instance["DB_PORT"])
    .Replace("mydb", EnvReader.Instance["DB_DATABASE"])
    .Replace("myuser", EnvReader.Instance["DB_USERNAME"])
    .Replace("mypassword", EnvReader.Instance["DB_PASSWORD"]);

builder.Services.AddDbContext<TaskContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32))));
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorPages();

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
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run("http://"+EnvReader.Instance["HOST"]+":"+EnvReader.Instance["PORT"]);
