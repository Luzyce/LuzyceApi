using Microsoft.EntityFrameworkCore;
using LuzyceApi.Db.AppDb.Data.Models;

namespace LuzyceApi.Db.AppDb.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions, IConfiguration config) : DbContext
{
    private readonly DbContextOptions<ApplicationDbContext> dbContextOptions = dbContextOptions;
    private readonly IConfiguration config = config;

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(config.GetConnectionString("AppDbConnection"), ServerVersion.AutoDetect(connectionString));
    }
}
