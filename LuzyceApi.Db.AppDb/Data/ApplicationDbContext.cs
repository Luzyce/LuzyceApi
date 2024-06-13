using Microsoft.EntityFrameworkCore;
using LuzyceApi.Db.AppDb.Data.Models;
using Microsoft.Extensions.Configuration;
using LuzyceApi.Db.AppDb.Models;

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
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(config.GetConnectionString("AppDbConnection"), ServerVersion.AutoDetect(config.GetConnectionString("AppDbConnection")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var adminRole = new Role
        {
            Id = 1,
            Name = "Admin"
        };

        modelBuilder.Entity<Role>().HasData(adminRole);

        var adminUser = new User
        {
            Id = 1,
            Name = "Admin",
            LastName = "Admin",
            Email = "admin@gmail.com",
            Login = "admin",
            Password = BCrypt.Net.BCrypt.HashPassword("admin"),
            Hash = "admin",
            RoleId = 1
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
            Code = "M",
            Name = "Magazyn"
        };

        modelBuilder.Entity<Warehouse>().HasData(magazyn);

        var exampleDocument = new Document
        {
            Id = 1,
            DocNumber = 1,
            Warehouse = null!,
            WarehouseId = magazyn.Id,
            Year = 2023,
            Number = "0001/M/2024",
            DocumentsDefinition = null!,
            DocumentsDefinitionId = kwit.Id,
            Operator = null!,
            OperatorId = adminUser.Id,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Status = null!,
            StatusId = open.Id
        };

        modelBuilder.Entity<Document>().HasData(exampleDocument);

        var exampleLampshade = new Lampshade
        {
            Id = 1,
            Code = "KL4124",
        };

        modelBuilder.Entity<Lampshade>().HasData(exampleLampshade);

        var exampleDocumentPosition = new DocumentPositions
        {
            Id = 1,
            DocumentId = exampleDocument.Id,
            Document = null!,
            QuantityNetto = 0,
            QuantityLoss = 0,
            QuantityToImprove = 0,
            QuantityGross = 0,
            OperatorId = adminUser.Id,
            Operator = null!,
            StartTime = DateTime.Now,
            EndTime = null,
            StatusId = open.Id,
            Status = null!,
            LampshadeId = 1,
            Lampshade = null!
        };

        modelBuilder.Entity<DocumentPositions>().HasData(exampleDocumentPosition);
    }
}
