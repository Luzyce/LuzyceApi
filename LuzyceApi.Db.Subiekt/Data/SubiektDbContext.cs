using Microsoft.EntityFrameworkCore;

namespace LuzyceApi.Db.Subiekt.Data;

public class SubiektDbContext : DbContext
{
    public SubiektDbContext(DbContextOptions<SubiektDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
}
