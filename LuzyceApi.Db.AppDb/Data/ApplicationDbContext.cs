using Microsoft.EntityFrameworkCore;
using LuzyceApi.Db.AppDb.Data.Models;

namespace LuzyceApi.Db.AppDb.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<User> Users { get; set; }
}
