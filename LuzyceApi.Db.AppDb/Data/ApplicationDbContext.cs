using Microsoft.EntityFrameworkCore;
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
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<OrderForProduction> OrdersForProduction { get; set; }
    public DbSet<OrderPositionForProduction> OrderPositionsForProduction { get; set; }
    public DbSet<ProductionPlan> ProductionPlans { get; set; }
    public DbSet<ProductionPlanPositions> ProductionPlanPositions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseMySql(config.GetConnectionString("AppDbConnection"), ServerVersion.AutoDetect(config.GetConnectionString("AppDbConnection")))
            .UseValidationCheckConstraints(options => options.UseRegex(false));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var roles = new List<Role>
        {
            new()
            {
                Id = 1,
                Name = "Admin"
            },
            new()
            {
                Id = 2,
                Name = "User"
            }
        };

        modelBuilder.Entity<Role>().HasData(roles);

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
        
        var documentsDefinitionsList = new List<DocumentsDefinition>
        {
            new()
            {
                Id = 1,
                Code = "KW",
                Name = "Kwit"
            },
            new()
            {
                Id = 2,
                Code = "ZP",
                Name = "Zlecenie Produkcji"
            }
        };
        
        modelBuilder.Entity<DocumentsDefinition>().HasData(documentsDefinitionsList);
        
        var statusList = new List<Status>
        {
            new()
            {
                Id = 1,
                Name = "Otwarty",
                Priority = 10
            },
            new()
            {
                Id = 2,
                Name = "Anulowany",
                Priority = 20
            },
            new()
            {
                Id = 3,
                Name = "Zamknięty",
                Priority = 30
            },
            new()
            {
                Id = 4,
                Name = "Anulowany",
                Priority = 40
            }
        };
        
        modelBuilder.Entity<Status>().HasData(statusList);

        var warehouseList = new List<Warehouse>
        {
            new()
            {
                Id = 1,
                Code = "M",
                Name = "Magazyn"
            },
            new()
            {
                Id = 2,
                Code = "P",
                Name = "Produkcja"
            }
        };
        
        modelBuilder.Entity<Warehouse>().HasData(warehouseList);
        
        var exampleDocumentList = new List<Document>
        {
            new()
            {
                Id = 1,
                DocNumber = 1,
                WarehouseId = warehouseList[0].Id,
                Year = 2023,
                Number = "M/0001/KW/2024",
                DocumentsDefinitionId = 1,
                OperatorId = adminUser.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                StatusId = 1
            },
            new()
            {
                Id = 2,
                DocNumber = 1,
                WarehouseId = warehouseList[1].Id,
                Year = 2024,
                Number = "P/0001/ZP/2024",
                DocumentsDefinitionId = 2,
                OperatorId = adminUser.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                StatusId = 1
            }
        };
        
        modelBuilder.Entity<Document>().HasData(exampleDocumentList);

        var exampleLampshade = new Lampshade
        {
            Id = 1,
            Code = "KL4124",
        };

        modelBuilder.Entity<Lampshade>().HasData(exampleLampshade);
        
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
        
        var exampleOrderPositionForProduction = new OrderPositionForProduction
        {
            Id = 1,
            OrderId = 1,
            Order = null!,
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

        var lampshadeVariants = new List<LampshadeVariant>
        {
            new()
            {
                Id = 1,
                Name = "Opal",
                ShortName = ""
            },
            new()
            {
                Id = 2,
                Name = "Opal Mat",
                ShortName = "M"
            },
            new()
            {
                Id = 3,
                Name = "Opal Alabaster",
                ShortName = "AL"
            },
            new()
            {
                Id = 4,
                Name = "Opal Falbanka",
                ShortName = "FA"
            },
            new()
            {
                Id = 5,
                Name = "Jasny",
                ShortName = "J"
            },
            new()
            {
                Id = 6,
                Name = "Jasny Kier",
                ShortName = "J-KR"
            },
            new()
            {
                Id = 7,
                Name = "Jasny Pladry",
                ShortName = "J-PL"
            },
            new()
            {
                Id = 8,
                Name = "Jasny Antiko",
                ShortName = "J-AC"
            },
            new()
            {
                Id = 9,
                Name = "Jasny Alabaster",
                ShortName = "J-AL"
            },
            new()
            {
                Id = 10,
                Name = "Jasny Mat",
                ShortName = "J-M"
            },
            new()
            {
                Id = 11,
                Name = "Jasny Mrożony",
                ShortName = "J-MR"
            }
        };

        modelBuilder.Entity<LampshadeVariant>().HasData(lampshadeVariants);
        
        var exampleLampshadeNorm = new LampshadeNorm
        {
            Id = 1,
            LampshadeId = exampleLampshade.Id,
            Lampshade = null!,
            VariantId = lampshadeVariants[0].Id,
            Variant = null!,
            QuantityPerChange = 50,
            WeightBrutto = 3,
            WeightNetto = 0.45,
            MethodOfPackaging = "300x300x110",
            QuantityPerPack = 16
        };
        
        modelBuilder.Entity<LampshadeNorm>().HasData(exampleLampshadeNorm);

        var exampleDocumentPositionList = new List<DocumentPositions>
        {
            new()
            {
                Id = 1,
                DocumentId = exampleDocumentList[0].Id,
                Document = null!,
                QuantityNetto = 0,
                QuantityLoss = 0,
                QuantityToImprove = 0,
                QuantityGross = 0,
                OperatorId = adminUser.Id,
                Operator = null!,
                StartTime = DateTime.Now,
                EndTime = null,
                LampshadeId = 1,
                Lampshade = null!
            },
            new()
            {
                Id = 2,
                DocumentId = exampleDocumentList[1].Id,
                Document = null!,
                QuantityNetto = 0,
                QuantityLoss = 0,
                QuantityToImprove = 0,
                QuantityGross = 0,
                OperatorId = adminUser.Id,
                Operator = null!,
                StartTime = DateTime.Now,
                EndTime = null,
                LampshadeId = exampleLampshade.Id,
                Lampshade = null!,
                LampshadeNormId = exampleLampshadeNorm.Id,
                LampshadeNorm = null!,
                LampshadeDekor = "F",
                Remarks = "Test",
                OrderPositionForProductionId = exampleOrderPositionForProduction.Id,
                OrderPositionForProduction = null!,
                po_NumberOfChanges = 1,
                po_QuantityMade = 0,
                SubiektProductId = 2628
            }
        };
        
        modelBuilder.Entity<DocumentPositions>().HasData(exampleDocumentPositionList);
        
        var exampleProductionPlan = new ProductionPlan
        {
            Id = 1,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Change = 1,
            Team = 1,
            ShiftSupervisorId = adminUser.Id,
            ShiftSupervisor = null!,
            StatusId = 1
        };
        
        modelBuilder.Entity<ProductionPlan>().HasData(exampleProductionPlan);
        
        var exampleProductionPlanPosition = new ProductionPlanPositions
        {
            Id = 1,
            Quantity = 50,
            ProductionPlanId = exampleProductionPlan.Id,
            ProductionPlan = null!,
            DocumentPositionId = exampleDocumentPositionList[1].Id,
            DocumentPosition = null!,
            HeadsOfMetallurgicalTeamsId = 1,
            HeadsOfMetallurgicalTeams = null!,
            NumberOfHours = 8
        };
        
        modelBuilder.Entity<ProductionPlanPositions>().HasData(exampleProductionPlanPosition);
    }
}
