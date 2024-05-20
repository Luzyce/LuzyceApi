using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.AppDb.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LuzyceApi.Repositories;

public class DocumentRepository(ApplicationDbContext applicationDbContext, ILogger<UsersRepository> logger)
{
    private readonly ApplicationDbContext applicationDbContext = applicationDbContext;

    public IEnumerable<Domain.Models.Document> GetDocuments()
    {
        logger.LogInformation("Getting all users");
        return applicationDbContext
        .Documents
        .Select(
            x => new Domain.Models.Document
            {
                Id = x.Id,
                DocNumber = x.DocNumber,
                Warehouse = WarehouseDomainFromDb(x.Warehouse!),
                Year = x.Year,
                Number = x.Number,
                DocumentsDefinition = DocumentsDefinitionDomainFromDb(x.DocumentsDefinition!),
                Operator = UserDomainFromDb(x.Operator!),
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                ClosedAt = x.ClosedAt,
                Status = StatusDomainFromDb(x.Status!)
            }
        )
        .ToList();
    }
    public Domain.Models.Document? GetDocument(int id)
    {
        logger.LogInformation("Getting document by id");
        var document = applicationDbContext.Documents
                        .Include(d => d.Warehouse)
                        .Include(d => d.Operator)
                        .Include(d => d.Status)
                        .Include(d => d.DocumentsDefinition)
                        .FirstOrDefault(x => x.Id == id);

        if (document == null)
        {
            return null;
        }
        return new Domain.Models.Document
        {
            Id = document.Id,
            DocNumber = document.DocNumber,
            Warehouse = WarehouseDomainFromDb(document.Warehouse!),
            Year = document.Year,
            Number = document.Number,
            DocumentsDefinition = DocumentsDefinitionDomainFromDb(document.DocumentsDefinition!),
            Operator = UserDomainFromDb(document.Operator!),
            CreatedAt = document.CreatedAt,
            UpdatedAt = document.UpdatedAt,
            ClosedAt = document.ClosedAt,
            Status = StatusDomainFromDb(document.Status!)
        };
    }
    public Domain.Models.Document AddDocument(Domain.Models.Document document)
    {
        logger.LogInformation("Adding document");
        int currentYear = DateTime.Now.Year;
        int nextDocNumber = applicationDbContext.Documents
        .Where(d => d.WarehouseId == document.WarehouseId
                    && d.Year == currentYear
                    && d.DocumentsDefinitionId == document.DocumentsDefinitionId)
        .Select(d => d.DocNumber)
        .ToList()
        .DefaultIfEmpty(0)
        .Max() + 1;

        var dbDocument = new Document
        {
            DocNumber = nextDocNumber,
            WarehouseId = document.WarehouseId,
            Year = currentYear,
            Number = $"{nextDocNumber:D4}/{applicationDbContext.Warehouses
                .Where(w => w.Id == document.WarehouseId)
                .Select(w => w.Code)
                .FirstOrDefault()}/{currentYear}",
            DocumentsDefinitionId = document.DocumentsDefinitionId,
            OperatorId = document.OperatorId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            ClosedAt = null,
            StatusId = 1
        };
        applicationDbContext.Documents.Add(dbDocument);
        applicationDbContext.SaveChanges();
        document.Id = dbDocument.Id;
        return document;
    }
    public Domain.Models.Document? UpdateDocument(Domain.Models.Document document)
    {
        logger.LogInformation("Updating document");
        var dbDocument = applicationDbContext.Documents.Find(document.Id);
        if (dbDocument == null)
        {
            return null;
        }
        dbDocument.StatusId = document.StatusId;
        dbDocument.UpdatedAt = DateTime.Now;
        applicationDbContext.SaveChanges();
        return document;
    }
    public static Domain.Models.Warehouse WarehouseDomainFromDb(Warehouse wherehouse)
    {
        return new()
        {
            Id = wherehouse.Id,
            Code = wherehouse.Code,
            Name = wherehouse.Name
        };
    }

    public static Domain.Models.User UserDomainFromDb(User user)
    {
        return new()
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Login = user.Login,
            Password = user.Password,
            Hash = user.Hash,
            CreatedAt = user.CreatedAt,
            Admin = user.Admin
        };
    }

    public static Domain.Models.Status StatusDomainFromDb(Status status)
    {
        return new()
        {
            Id = status.Id,
            Name = status.Name,
            Priority = status.Priority
        };
    }

    public static Domain.Models.DocumentsDefinition DocumentsDefinitionDomainFromDb(DocumentsDefinition documentsDefinition)
    {
        return new()
        {
            Id = documentsDefinition.Id,
            Code = documentsDefinition.Code,
            Name = documentsDefinition.Name
        };
    }
}
