using Microsoft.EntityFrameworkCore;
using DotEnv.Core;

public class TaskContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        new EnvLoader()
            .AddEnvFile("../env/.env")
            .Load();
		string conn = "Server=host.docker.internal,3308;Database="+EnvReader.Instance["DB_DATABASE"]+";User="+EnvReader.Instance["DB_USERNAME"]+";Password="+EnvReader.Instance["DB_PASSWORD"]+";";
		optionsBuilder.UseSqlServer(conn);
    }
}