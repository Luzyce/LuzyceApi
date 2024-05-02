using Microsoft.EntityFrameworkCore;
using LuzyceApi.Db.AppDb.Data.Models;
using Microsoft.Extensions.Configuration;

namespace LuzyceApi.Db.AppDb.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration config;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions, IConfiguration config) : base(dbContextOptions)
    {
        this.config = config;
    }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(config.GetConnectionString("AppDbConnection"), ServerVersion.AutoDetect(config.GetConnectionString("AppDbConnection")));
    }
}
