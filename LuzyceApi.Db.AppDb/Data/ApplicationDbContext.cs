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
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentItemRelationships> DocumentItemRelationships { get; set; }
    public DbSet<DocumentPositions> DocumentPositions { get; set; }
    public DbSet<DocumentRelations> DocumentRelations { get; set; }
    public DbSet<DocumentsDefinition> DocumentsDefinitions { get; set; }
    public DbSet<Error> Errors { get; set; }
    public DbSet<Lampshade> Lampshades { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(config.GetConnectionString("AppDbConnection"), ServerVersion.AutoDetect(config.GetConnectionString("AppDbConnection")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var adminUser = new User
        {
            Id = 1,
            Name = "Admin",
            LastName = "Admin",
            Email = "admin@gmail.com",
            Login = "admin",
            Password = BCrypt.Net.BCrypt.HashPassword("admin"),
            Hash = "admin",
            Admin = true
        };

        modelBuilder.Entity<User>().HasData(adminUser);

        var kwit = new DocumentsDefinition
        {
            Id = 1,
            Code = "KW",
            Name = "Kwit"
        };

        modelBuilder.Entity<DocumentsDefinition>().HasData(kwit);

        var open = new Status
        {
            Id = 1,
            Name = "Otwarty",
            Priority = 1
        };

        modelBuilder.Entity<Status>().HasData(open);

        var magazyn = new Warehouse
        {
            Id = 1,
            Code = "MG",
            Name = "Magazyn"
        };

        modelBuilder.Entity<Warehouse>().HasData(magazyn);

        var exampleDocument = new Document
        {
            Id = 1,
            Number = "0001/KW/2023",
            Year = 2023,
            Warehouse = null!,
            WarehouseId = magazyn.Id,
            Operator = null!,
            OperatorId = adminUser.Id,
            CreatedAt = DateTime.Now,
            Status = null!,
            StatusId = open.Id,
            DocumentsDefinition = null!,
            DocumentsDefinitionId = kwit.Id
        };

        modelBuilder.Entity<Document>().HasData(exampleDocument);
    }
}
