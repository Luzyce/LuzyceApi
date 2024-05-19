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
                Number = x.Number,
                Warehouse = WarehouseDomainFromDb(x.Warehouse),
                Year = x.Year,
                Operator = UserDomainFromDb(x.Operator),
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                ClosedAt = x.ClosedAt,
                Status = StatusDomainFromDb(x.Status),
                DocumentsDefinition = DocumentsDefinitionDomainFromDb(x.DocumentsDefinition)
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
            Number = document.Number,
            Warehouse = WarehouseDomainFromDb(document.Warehouse),
            Year = document.Year,
            Operator = UserDomainFromDb(document.Operator),
            CreatedAt = document.CreatedAt,
            UpdatedAt = document.UpdatedAt,
            ClosedAt = document.ClosedAt,
            Status = StatusDomainFromDb(document.Status),
            DocumentsDefinition = DocumentsDefinitionDomainFromDb(document.DocumentsDefinition)
        };
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
