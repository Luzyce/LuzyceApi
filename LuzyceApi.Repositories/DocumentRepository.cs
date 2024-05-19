using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.AppDb.Data.Models;
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
