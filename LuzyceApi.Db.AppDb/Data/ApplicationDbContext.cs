using Microsoft.EntityFrameworkCore;
using LuzyceApi.Db.AppDb.Data.Models;
using Microsoft.Extensions.Configuration;
using LuzyceApi.Db.AppDb.Models;

namespace LuzyceApi.Db.AppDb.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions, IConfiguration config)
    : DbContext(dbContextOptions)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentItemRelationships> DocumentItemRelationships { get; set; }
    public DbSet<DocumentPositions> DocumentPositions { get; set; }
    public DbSet<DocumentRelations> DocumentRelations { get; set; }
    public DbSet<DocumentsDefinition> DocumentsDefinitions { get; set; }
    public DbSet<Error> Errors { get; set; }
    public DbSet<Lampshade> Lampshades { get; set; }
    public DbSet<LampshadeNorm> LampshadeNorms { get; set; }
    public DbSet<LampshadeVariant> LampshadeVariants { get; set; }
    public DbSet<LampshadeDekor> LampshadeDekors { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<OrderForProduction> OrdersForProduction { get; set; }
    public DbSet<OrderPositionForProduction> OrderPositionsForProduction { get; set; }

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

        var userRole = new Role
        {
            Id = 2,
            Name = "User"
        };

        modelBuilder.Entity<Role>().HasData(adminRole, userRole);

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
            Number = "M/0001/KW/2024",
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
        
        var exampleOrderForProduction = new OrderForProduction()
        {
            Id = 1,
            Date = DateTime.Now,
            Number = "1",
            CustomerId = 1,
            CustomerSymbol = "TEST",
            CustomerName = "Testowanie"
        };
        
        modelBuilder.Entity<OrderForProduction>().HasData(exampleOrderForProduction);
        
        var exampleOrderPositionForProduction = new OrderPositionForProduction()
        {
            Id = 1,
            OrderId = 1,
            OrderNumber = "1",
            Symbol = "TEST",
            ProductId = exampleLampshade.Id,
            Product = null!,
            Description = "Test",
            OrderPositionLp = 1,
            Quantity = 1,
            QuantityInStock = 1,
            Unit = "szt",
            SerialNumber = "1",
            ProductSymbol = "KL4124",
            ProductName = "KL4124",
            ProductDescription = "Test"
        };
        
        modelBuilder.Entity<OrderPositionForProduction>().HasData(exampleOrderPositionForProduction);
        
        var ProductionOrder = new DocumentsDefinition
        {
            Id = 2,
            Code = "ZP",
            Name = "Zlecenie Produkcji"
        };

        modelBuilder.Entity<DocumentsDefinition>().HasData(ProductionOrder);
        
        var produkcja = new Warehouse()
        {
            Id = 2,
            Code = "P",
            Name = "Produkcja"
        };
        
        modelBuilder.Entity<Warehouse>().HasData(produkcja);

        var lampshadeVariants = new List<LampshadeVariant>
        {
            new LampshadeVariant
            {
                Id = 1,
                Name = "Opal",
                ShortName = ""
            },
            new LampshadeVariant
            {
                Id = 2,
                Name = "Opal Mat",
                ShortName = "M"
            },
            new LampshadeVariant
            {
                Id = 3,
                Name = "Opal Alabaster",
                ShortName = "AL"
            },
            new LampshadeVariant
            {
                Id = 4,
                Name = "Opal Falbanka",
                ShortName = "FA"
            },
            new LampshadeVariant
            {
                Id = 5,
                Name = "Jasny",
                ShortName = "J"
            },
            new LampshadeVariant
            {
                Id = 6,
                Name = "Jasny Kier",
                ShortName = "J-KR"
            },
            new LampshadeVariant
            {
                Id = 7,
                Name = "Jasny Pladry",
                ShortName = "J-PL"
            },
            new LampshadeVariant
            {
                Id = 8,
                Name = "Jasny Antiko",
                ShortName = "J-AC"
            },
            new LampshadeVariant
            {
                Id = 9,
                Name = "Jasny Alabaster",
                ShortName = "J-AL"
            },
            new LampshadeVariant
            {
                Id = 10,
                Name = "Jasny Mat",
                ShortName = "J-M"
            },
            new LampshadeVariant
            {
                Id = 11,
                Name = "Jasny Mrożony",
                ShortName = "J-MR"
            }
        };

        modelBuilder.Entity<LampshadeVariant>().HasData(lampshadeVariants);
        
        var lampshadeDekors = new List<LampshadeDekor>
        {
            new LampshadeDekor
            {
                Id = 1,
                Name = "farba",
                ShortName = "F"
            },
            new LampshadeDekor
            {
                Id = 2,
                Name = "Dekor",
                ShortName = "D"
            },
            new LampshadeDekor
            {
                Id = 3,
                Name = "Dekor D1",
                ShortName = "D1"
            },
            new LampshadeDekor
            {
                Id = 4,
                Name = "Dekor D2",
                ShortName = "D2"
            },
            new LampshadeDekor
            {
                Id = 5,
                Name = "Dekor D3",
                ShortName = "D3"
            },
            new LampshadeDekor
            {
                Id = 6,
                Name = "Dekor D4",
                ShortName = "D4"
            },
            new LampshadeDekor
            {
                Id = 7,
                Name = "Dekor D5",
                ShortName = "D5"
            },
            new LampshadeDekor
            {
                Id = 8,
                Name = "Dekor D6",
                ShortName = "D6"
            },
            new LampshadeDekor
            {
                Id = 9,
                Name = "Dekor D7",
                ShortName = "D7"
            },
            new LampshadeDekor
            {
                Id = 10,
                Name = "Dekor D8",
                ShortName = "D8"
            },
            new LampshadeDekor
            {
                Id = 11,
                Name = "Dekor D9",
                ShortName = "D9"
            },
            new LampshadeDekor
            {
                Id = 12,
                Name = "Dekor D10",
                ShortName = "D10"
            },
            new LampshadeDekor
            {
                Id = 13,
                Name = "Dekor D11",
                ShortName = "D11"
            },
            new LampshadeDekor
            {
                Id = 14,
                Name = "Dekor D12",
                ShortName = "D12"
            },
            new LampshadeDekor
            {
                Id = 15,
                Name = "Dekor D13",
                ShortName = "D13"
            },
            new LampshadeDekor
            {
                Id = 16,
                Name = "Dekor D14",
                ShortName = "D14"
            },
            new LampshadeDekor
            {
                Id = 17,
                Name = "Dekor D15",
                ShortName = "D15"
            },
            new LampshadeDekor
            {
                Id = 18,
                Name = "Dekor D16",
                ShortName = "D16"
            },
            new LampshadeDekor
            {
                Id = 19,
                Name = "Dekor D17",
                ShortName = "D17"
            },
            new LampshadeDekor
            {
                Id = 20,
                Name = "Dekor D18",
                ShortName = "D18"
            },
            new LampshadeDekor
            {
                Id = 21,
                Name = "Dekor D19",
                ShortName = "D19"
            },
            new LampshadeDekor
            {
                Id = 22,
                Name = "Dekor D20",
                ShortName = "D20"
            },
            new LampshadeDekor
            {
                Id = 23,
                Name = "Dekor D21",
                ShortName = "D21"
            },
            new LampshadeDekor
            {
                Id = 24,
                Name = "Dekor D22",
                ShortName = "D22"
            },
            new LampshadeDekor
            {
                Id = 25,
                Name = "Dekor D23",
                ShortName = "D23"
            },
            new LampshadeDekor
            {
                Id = 26,
                Name = "Dekor D24",
                ShortName = "D24"
            },
            new LampshadeDekor
            {
                Id = 27,
                Name = "Dekor D25",
                ShortName = "D25"
            },
            new LampshadeDekor
            {
                Id = 28,
                Name = "Dekor D26",
                ShortName = "D26"
            },
            new LampshadeDekor
            {
                Id = 29,
                Name = "Dekor D27",
                ShortName = "D27"
            },
            new LampshadeDekor
            {
                Id = 30,
                Name = "Dekor D28",
                ShortName = "D28"
            },
            new LampshadeDekor
            {
                Id = 31,
                Name = "Dekor D29",
                ShortName = "D29"
            },
            new LampshadeDekor
            {
                Id = 32,
                Name = "Dekor D30",
                ShortName = "D30"
            },
            new LampshadeDekor
            {
                Id = 33,
                Name = "Dekor D31",
                ShortName = "D31"
            },
            new LampshadeDekor
            {
                Id = 34,
                Name = "Dekor D32",
                ShortName = "D32"
            }
        };

        modelBuilder.Entity<LampshadeDekor>().HasData(lampshadeDekors);
        
        var exampleLampshadeNorm = new LampshadeNorm
        {
            Id = 1,
            LampshadeId = exampleLampshade.Id,
            Lampshade = null!,
            VariantId = lampshadeVariants[0].Id,
            Variant = null!,
            QuantityPerChange = 50
        };
        
        modelBuilder.Entity<LampshadeNorm>().HasData(exampleLampshadeNorm);
        
        var exampleProductionOrder = new Document
        {
            Id = 2,
            DocNumber = 1,
            Warehouse = null!,
            WarehouseId = produkcja.Id,
            Year = 2024,
            Number = "P/0001/ZP/2024",
            DocumentsDefinition = null!,
            DocumentsDefinitionId = ProductionOrder.Id,
            Operator = null!,
            OperatorId = adminUser.Id,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Status = null!,
            StatusId = open.Id
        };
        
        modelBuilder.Entity<Document>().HasData(exampleProductionOrder);
        
        var exampleProductionOrderPosition = new DocumentPositions
        {
            Id = 2,
            DocumentId = exampleProductionOrder.Id,
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
            LampshadeId = exampleLampshade.Id,
            Lampshade = null!,
            LampshadeNormId = exampleLampshadeNorm.Id,
            LampshadeNorm = null!,
            LampshadeDekorId = lampshadeDekors[0].Id,
            LampshadeDekor = null!,
            OrderPositionForProductionId = exampleOrderPositionForProduction.Id,
            OrderPositionForProduction = null!
        };
        
        modelBuilder.Entity<DocumentPositions>().HasData(exampleProductionOrderPosition);
    }
}
