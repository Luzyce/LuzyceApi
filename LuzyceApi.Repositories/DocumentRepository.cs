using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.AppDb.Models;
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
            .Include(d => d.DocumentsDefinition).Include(document => document.LockedBy)
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
            Status = StatusDomainFromDb(document.Status!),
            LockedBy = ClientDomainFromDb(document.LockedBy)
        };
    }
    public Domain.Models.Document? GetDocumentByNumber(string number)
    {
        logger.LogInformation("Getting document by number");
        var document = applicationDbContext.Documents
                        .Include(d => d.Warehouse)
                        .Include(d => d.Operator)
                        .Include(d => d.Status)
                        .Include(d => d.DocumentsDefinition)
                        .FirstOrDefault(x => x.Number == number);

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
    public bool LockDocument(int id, int clientId)
    {
        logger.LogInformation("Updating document");
        var dbDocument = applicationDbContext.Documents.Find(id);
        if (dbDocument == null)
        {
            return false;
        }

        dbDocument.LockedById = clientId;
        dbDocument.UpdatedAt = DateTime.Now;
        applicationDbContext.SaveChanges();
        return true;
    }

    public bool UnlockDocument(int id)
    {
        logger.LogInformation("Updating document");
        var dbDocument = applicationDbContext.Documents.Find(id);
        if (dbDocument == null)
        {
            return false;
        }
        dbDocument.LockedBy = null;
        dbDocument.UpdatedAt = DateTime.Now;
        applicationDbContext.SaveChanges();
        return true;
    }
    
    public bool IsDocumentLocked(int id)
    {
        logger.LogInformation("Checking if document is locked");
        var dbDocument = applicationDbContext.Documents.Find(id);
        return dbDocument?.LockedBy != null;
    }
    public bool IsDocumentLockedByUser(int id, int clientId)
    {
        logger.LogInformation("Checking if document is locked");
        var dbDocument = applicationDbContext.Documents
            .Include(x => x.LockedBy)
            .FirstOrDefault(x => x.Id == id);
        return dbDocument?.LockedById == clientId;
    }
    public Domain.Models.DocumentPositions AddDocumentPosition(Domain.Models.DocumentPositions documentPosition)
    {
        logger.LogInformation("Adding document position" + documentPosition.DocumentId);
        var dbDocumentPosition = new DocumentPositions
        {
            QuantityNetto = documentPosition.QuantityNetto,
            QuantityLoss = documentPosition.QuantityLoss,
            QuantityToImprove = documentPosition.QuantityToImprove,
            QuantityGross = documentPosition.QuantityGross,
            DocumentId = documentPosition.DocumentId,
            OperatorId = documentPosition.OperatorId,
            StartTime = documentPosition.StartTime,
            LampshadeId = documentPosition.LampshadeId
        };
        applicationDbContext.DocumentPositions.Add(dbDocumentPosition);
        applicationDbContext.SaveChanges();
        return documentPosition;
    }
    public Domain.Models.DocumentPositions? GetDocumentPosition(int id)
    {
        logger.LogInformation("Getting document positions");
        var documentPositions = applicationDbContext.DocumentPositions
                        .Include(d => d.Document)
                        .ThenInclude(d => d!.Warehouse)
                        .Include(d => d.Document)
                        .ThenInclude(d => d!.DocumentsDefinition)
                        .Include(d => d.Document)
                        .ThenInclude(d => d!.Operator)
                        .Include(d => d.Operator)
                        .Include(d => d.Lampshade)
                        .FirstOrDefault(x => x.DocumentId == id);

        if (documentPositions == null)
        {
            return null;
        }

        return new Domain.Models.DocumentPositions
        {
            Id = documentPositions.Id,
            DocumentId = documentPositions.DocumentId,
            Document = DocumentDomainFromDb(documentPositions.Document!),
            QuantityNetto = documentPositions.QuantityNetto,
            QuantityLoss = documentPositions.QuantityLoss,
            QuantityToImprove = documentPositions.QuantityToImprove,
            QuantityGross = documentPositions.QuantityGross,
            Operator = UserDomainFromDb(documentPositions.Operator!),
            StartTime = documentPositions.StartTime,
            LampshadeId = documentPositions.LampshadeId,
            Lampshade = LampshadeDomainFromDb(documentPositions.Lampshade!)
        };
    }
    public IEnumerable<Domain.Models.DocumentPositions> GetDocumentPositions(int documentId)
    {
        logger.LogInformation("Getting document positions");
        return applicationDbContext.DocumentPositions
            .Include(d => d.Document)
            .Include(d => d.Operator)
            .Include(d => d.Lampshade)
            .Where(d => d.DocumentId == documentId)
            .Select(
                x => new Domain.Models.DocumentPositions
                {
                    Id = x.Id,
                    Document = DocumentDomainFromDb(x.Document!),
                    QuantityNetto = x.QuantityNetto,
                    QuantityLoss = x.QuantityLoss,
                    QuantityToImprove = x.QuantityToImprove,
                    QuantityGross = x.QuantityGross,
                    Operator = UserDomainFromDb(x.Operator!),
                    StartTime = x.StartTime,
                    LampshadeId = x.LampshadeId,
                    Lampshade = LampshadeDomainFromDb(x.Lampshade!)
                }
            )
            .ToList();
    }
    public Domain.Models.DocumentPositions? UpdateDocumentPosition(Domain.Models.DocumentPositions documentPosition)
    {
        logger.LogInformation("Updating document position");
        var dbDocumentPosition = applicationDbContext.DocumentPositions.Find(documentPosition.Id);
        if (dbDocumentPosition == null)
        {
            return null;
        }
        dbDocumentPosition.EndTime = documentPosition.EndTime;
        dbDocumentPosition.QuantityNetto = documentPosition.QuantityNetto;
        dbDocumentPosition.QuantityLoss = documentPosition.QuantityLoss;
        dbDocumentPosition.QuantityToImprove = documentPosition.QuantityToImprove;
        dbDocumentPosition.QuantityGross = documentPosition.QuantityGross;
        applicationDbContext.SaveChanges();
        return documentPosition;
    }
    public Domain.Models.Operation AddOperation(Domain.Models.Operation operation)
    {
        logger.LogInformation("Adding operation");
        var dbOperation = new Operation
        {
            Time = DateTime.Now,
            DocumentId = operation.DocumentId,
            OperatorId = operation.OperatorId,
            QuantityNetDelta = operation.QuantityNetDelta,
            QuantityLossDelta = operation.QuantityLossDelta,
            QuantityToImproveDelta = operation.QuantityToImproveDelta,
        };
        applicationDbContext.Operations.Add(dbOperation);
        applicationDbContext.SaveChanges();
        operation.Id = dbOperation.Id;
        return operation;
    }
    public Domain.Models.Error? GetError(string code)
    {
        logger.LogInformation("Getting error by id");
        var error = applicationDbContext.Errors
                        .FirstOrDefault(x => x.Code == code);

        if (error == null)
        {
            return null;
        }
        return new Domain.Models.Error
        {
            Id = error.Id,
            Code = error.Code,
            ShortName = error.ShortName,
            Name = error.Name
        };
    }
    public Domain.Models.DocumentsDefinition? GetDocumentsDefinition(int id)
    {
        logger.LogInformation("Getting document definition by id");
        var documentsDefinition = applicationDbContext.DocumentsDefinitions
                        .FirstOrDefault(x => x.Id == id);

        if (documentsDefinition == null)
        {
            return null;
        }
        return new Domain.Models.DocumentsDefinition
        {
            Id = documentsDefinition.Id,
            Code = documentsDefinition.Code,
            Name = documentsDefinition.Name
        };
    }

    private static Domain.Models.Warehouse WarehouseDomainFromDb(Warehouse warehouse)
    {
        ArgumentNullException.ThrowIfNull(warehouse);

        return new()
        {
            Id = warehouse.Id,
            Code = warehouse.Code,
            Name = warehouse.Name
        };
    }

    private static Domain.Models.User UserDomainFromDb(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

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
            RoleId = user.RoleId
        };
    }
    public static Domain.Models.Role RoleDomainFromDb(Db.AppDb.Models.Role role)
    {
        ArgumentNullException.ThrowIfNull(role);

        return new()
        {
            Id = role.Id,
            Name = role.Name
        };
    }

    private static Domain.Models.Status StatusDomainFromDb(Status status)
    {
        ArgumentNullException.ThrowIfNull(status);

        return new()
        {
            Id = status.Id,
            Name = status.Name,
            Priority = status.Priority
        };
    }

    private static Domain.Models.DocumentsDefinition DocumentsDefinitionDomainFromDb(DocumentsDefinition documentsDefinition)
    {
        ArgumentNullException.ThrowIfNull(documentsDefinition);

        return new()
        {
            Id = documentsDefinition.Id,
            Code = documentsDefinition.Code,
            Name = documentsDefinition.Name
        };
    }

    private static Domain.Models.Lampshade LampshadeDomainFromDb(Lampshade lampshade)
    {
        ArgumentNullException.ThrowIfNull(lampshade);

        return new()
        {
            Id = lampshade.Id,
            Code = lampshade.Code,
        };
    }

    private static Domain.Models.Document DocumentDomainFromDb(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);

        return new()
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

    private static Domain.Models.Client? ClientDomainFromDb(Client? client)
    {
        if (client == null)
        {
            return null;
        }

        return new Domain.Models.Client
        {
            Id = client.Id,
            Name = client.Name,
            IpAddress = client.IpAddress
        };
    }
}
