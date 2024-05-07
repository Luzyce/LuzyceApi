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
            Name = "Admin",
            LastName = "Admin",
            Email = "admin@gmail.com",
            Login = "admin",
            Password = BCrypt.Net.BCrypt.HashPassword("admin"),
            Hash = "admin",
            Admin = true
        };

        var DocumentDefinitionID = new DocumentsDefinition
        {
            Code = "D",
            Name = "Dokument"
        };

        var open = new Status
        {
            Name = "Otwarty",
            Priority = 1
        };

        var kwit = new Warehouse
        {
            Code = "KW",
            Name = "Kwit"
        };

        var exampleDocument = new Document
        {
            Number = "0001/KW/2023",
            Year = 2023,
            WarehouseID = kwit,
            OperatorID = adminUser,
            CreatedAt = DateTime.Now,
            StatusID = open,
            DocumentDefinitionID = DocumentDefinitionID
        };

        modelBuilder.Entity<User>().HasData(adminUser);
        modelBuilder.Entity<Warehouse>().HasData(kwit);
        modelBuilder.Entity<Status>().HasData(open);
        modelBuilder.Entity<Document>().HasData(exampleDocument);
    }
}
