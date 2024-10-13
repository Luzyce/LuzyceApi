using Luzyce.Core.Models.Document;
using Luzyce.Core.Models.Kwit;
using Luzyce.Core.Models.Log;
using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.AppDb.Models;
using Microsoft.EntityFrameworkCore;
using DocumentPositions = LuzyceApi.Domain.Models.DocumentPositions;

namespace LuzyceApi.Repositories;

public class KwitRepository(ApplicationDbContext applicationDbContext)
{
    private readonly ApplicationDbContext applicationDbContext = applicationDbContext;

    public GetKwit? GetKwit(int id)
    {
        var kwit = applicationDbContext.Documents
            .Include(d => d.Warehouse)
            .Include(d => d.Operator)
            .Include(d => d.Status)
            .Include(d => d.DocumentsDefinition)
            .Include(d => d.LockedBy)
            .Include(d => d.ProductionPlanPositions)
            .Include(d => d.DocumentPositions)
            .FirstOrDefault(x => x.Id == id);

        if (kwit == null)
        {
            return null;
        }

        var errorsInKwit = applicationDbContext.Operations
            .Include(x => x.ErrorCode)
            .Where(x => x.DocumentId == id &&
                        x.QuantityLossDelta == 1 &&
                        x.Client != null &&
                        x.Client.Type == "Terminal" &&
                        x.IsCancelled == false)
            .Select(x => x.ErrorCode)
            .ToList();

        var lacks = new List<GetLacks>();
        foreach (var error in errorsInKwit)
        {
            if (error == null || lacks.Any(x => x.ErrorName == error.Name))
            {
                continue;
            }

            lacks.Add(new GetLacks
            {
                Quantity = errorsInKwit.Count(x => x?.Code == error.Code),
                ErrorName = error.Name,
                ErrorShortName = error.ShortName
            });
        }

        return new GetKwit
        {
            Id = kwit.Id,
            DocNumber = kwit.DocNumber,
            Warehouse = kwit.Warehouse == null
                ? null
                : new GetWarehouseResponseDto
                {
                    Id = kwit.Warehouse.Id,
                    Name = kwit.Warehouse.Name,
                    Code = kwit.Warehouse.Code
                },
            Year = kwit.Year,
            Number = kwit.Number,
            DocumentsDefinition = kwit.DocumentsDefinition == null
                ? null
                : new GetDocumentsDefinitionResponseDto
                {
                    Id = kwit.DocumentsDefinition.Id,
                    Code = kwit.DocumentsDefinition.Code,
                    Name = kwit.DocumentsDefinition.Name
                },
            CreatedAt = kwit.CreatedAt,
            UpdatedAt = kwit.UpdatedAt,
            ClosedAt = kwit.ClosedAt,
            Status = kwit.Status == null
                ? null
                : new GetStatusResponseDto
                {
                    Id = kwit.Status.Id,
                    Name = kwit.Status.Name,
                    Priority = kwit.Status.Priority
                },
            LockedBy = kwit.LockedBy == null
                ? null
                : new GetClient
                {
                    Id = kwit.LockedBy.Id,
                    Name = kwit.LockedBy.Name,
                    IpAddress = kwit.LockedBy.IpAddress
                },
            Quantity = kwit.ProductionPlanPositions?.Quantity ?? 0,
            QuantityNetto = kwit.DocumentPositions.First().QuantityNetto,
            QuantityGross = kwit.DocumentPositions.First().QuantityGross,
            QuantityLoss = kwit.DocumentPositions.First().QuantityLoss,
            QuantityToImprove = kwit.DocumentPositions.First().QuantityToImprove,
            Lacks = lacks
        };
    }

    public void RevertKwit(int id)
    {
        var kwit = applicationDbContext.Documents.Find(id);
        if (kwit == null)
        {
            return;
        }

        kwit.ClosedAt = null;
        kwit.StatusId = 1;
        kwit.UpdatedAt = DateTime.Now;
        applicationDbContext.SaveChanges();
    }

    public void CloseKwit(int id)
    {
        var kwit = applicationDbContext.Documents.Find(id);
        if (kwit == null)
        {
            return;
        }

        kwit.ClosedAt = DateTime.Now;
        kwit.StatusId = 3;
        kwit.UpdatedAt = DateTime.Now;
        applicationDbContext.SaveChanges();
    }

    public void UpdateKwit(UpdateKwit updateKwit)
    {
        var kwit = applicationDbContext.Documents
            .Include(d => d.DocumentPositions)
            .FirstOrDefault(x => x.Id == updateKwit.Id);
        if (kwit == null)
        {
            return;
        }

        kwit.DocumentPositions.First().QuantityNetto = updateKwit.QuantityNetto;
        kwit.DocumentPositions.First().QuantityLoss = updateKwit.QuantityLoss;
        kwit.DocumentPositions.First().QuantityToImprove = updateKwit.QuantityToImprove;
        kwit.UpdatedAt = DateTime.Now;
        applicationDbContext.SaveChanges();
    }

    public Document? GetKwitForOperation(int id)
    {
        return applicationDbContext.Documents
            .Include(d => d.DocumentPositions)
            .FirstOrDefault(x => x.Id == id);
    }

    public Domain.Models.Document? TerminalGetKwit(int id)
    {
        var kwit = applicationDbContext.Documents
            .Include(d => d.Warehouse)
            .Include(d => d.Operator)
            .Include(d => d.Status)
            .Include(d => d.DocumentsDefinition)
            .Include(d => d.LockedBy)
            .FirstOrDefault(x => x.Id == id);

        if (kwit == null)
        {
            return null;
        }

        return new Domain.Models.Document
        {
            Id = kwit.Id,
            DocNumber = kwit.DocNumber,
            Warehouse = WarehouseDomainFromDb(kwit.Warehouse!),
            Year = kwit.Year,
            Number = kwit.Number,
            DocumentsDefinition = DocumentsDefinitionDomainFromDb(kwit.DocumentsDefinition!),
            Operator = UserDomainFromDb(kwit.Operator!),
            CreatedAt = kwit.CreatedAt,
            UpdatedAt = kwit.UpdatedAt,
            ClosedAt = kwit.ClosedAt,
            Status = StatusDomainFromDb(kwit.Status!),
            LockedBy = ClientDomainFromDb(kwit.LockedBy)
        };
    }

    public void LockKwit(int id, int clientId)
    {
        var kwit = applicationDbContext.Documents.Find(id);
        if (kwit == null)
        {
            return;
        }

        kwit.LockedById = clientId;
        kwit.UpdatedAt = DateTime.Now;
        applicationDbContext.SaveChanges();
    }

    public void UnlockKwit(int id)
    {
        var kwit = applicationDbContext.Documents.Find(id);
        if (kwit == null)
        {
            return;
        }
        kwit.ClosedAt = DateTime.Now;
        kwit.LockedBy = null;
        kwit.StatusId = 3;
        kwit.UpdatedAt = DateTime.Now;
        applicationDbContext.SaveChanges();
    }
    
    public bool IsKwitLocked(int id)
    {
        var kwit = applicationDbContext.Documents
            .Include(x => x.LockedBy)
            .FirstOrDefault(x => x.Id == id);
        return kwit?.LockedBy != null;
    }

    public bool IsKwitClosed(int id)
    {
        var kwit = applicationDbContext.Documents
            .Include(x => x.Status)
            .FirstOrDefault(x => x.Id == id);
        return kwit?.StatusId == 3;
    }
    public bool IsKwitLockedByUser(int id, int clientId)
    {
        var kwit = applicationDbContext.Documents
            .Include(x => x.LockedBy)
            .FirstOrDefault(x => x.Id == id);
        return kwit?.LockedById == clientId;
    }

    public IEnumerable<DocumentPositions> GetKwitPositions(int documentId)
    {
        return applicationDbContext.DocumentPositions
            .Include(d => d.Document)
            .Include(d => d.Operator)
            .Include(d => d.Lampshade)
            .Where(d => d.DocumentId == documentId)
            .Select(
                x => new DocumentPositions
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
    public void AddOperation(Domain.Models.Operation operation)
    {
        var dbOperation = new Operation
        {
            Time = DateTime.Now,
            DocumentId = operation.DocumentId,
            OperatorId = operation.OperatorId,
            QuantityNetDelta = operation.QuantityNetDelta,
            QuantityLossDelta = operation.QuantityLossDelta,
            QuantityToImproveDelta = operation.QuantityToImproveDelta,
            ErrorCodeId = operation.ErrorCodeId,
            ClientId = operation.ClientId
        };
        applicationDbContext.Operations.Add(dbOperation);
        applicationDbContext.SaveChanges();
        operation.Id = dbOperation.Id;
    }
    public void UpdateKwitPosition(DocumentPositions documentPosition)
    {
        var dbDocumentPosition = applicationDbContext.DocumentPositions.Find(documentPosition.Id);
        if (dbDocumentPosition == null)
        {
            return;
        }
        dbDocumentPosition.EndTime = documentPosition.EndTime;
        dbDocumentPosition.QuantityNetto = documentPosition.QuantityNetto;
        dbDocumentPosition.QuantityLoss = documentPosition.QuantityLoss;
        dbDocumentPosition.QuantityToImprove = documentPosition.QuantityToImprove;
        dbDocumentPosition.QuantityGross = documentPosition.QuantityGross;
        applicationDbContext.SaveChanges();
    }

    public void CancelPreviousOperation(string field)
    {
        Func<Operation, bool>? predicate = field switch
        {
            "Dobrych" => x => x.QuantityNetDelta == 1,
            "Zlych" => x => x.QuantityLossDelta == 1,
            "DoPoprawy" => x => x.QuantityToImproveDelta == 1,
            _ => null
        };

        if (predicate == null)
        {
            return;
        }

        var lastOperation = applicationDbContext.Operations
            .OrderByDescending(x => x.Time)
            .Where(x => x.IsCancelled == false)
            .FirstOrDefault(predicate);

        if (lastOperation == null)
        {
            return;
        }

        lastOperation.IsCancelled = true;
        applicationDbContext.SaveChanges();
    }

    public Domain.Models.Error? GetError(string code)
    {
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

    private static Domain.Models.Warehouse WarehouseDomainFromDb(Warehouse warehouse)
    {
        ArgumentNullException.ThrowIfNull(warehouse);

        return new Domain.Models.Warehouse
        {
            Id = warehouse.Id,
            Code = warehouse.Code,
            Name = warehouse.Name
        };
    }

    private static Domain.Models.User UserDomainFromDb(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new Domain.Models.User
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

    private static Domain.Models.Status StatusDomainFromDb(Status status)
    {
        ArgumentNullException.ThrowIfNull(status);

        return new Domain.Models.Status
        {
            Id = status.Id,
            Name = status.Name,
            Priority = status.Priority
        };
    }

    private static Domain.Models.DocumentsDefinition DocumentsDefinitionDomainFromDb(DocumentsDefinition documentsDefinition)
    {
        ArgumentNullException.ThrowIfNull(documentsDefinition);

        return new Domain.Models.DocumentsDefinition
        {
            Id = documentsDefinition.Id,
            Code = documentsDefinition.Code,
            Name = documentsDefinition.Name
        };
    }

    private static Domain.Models.Lampshade LampshadeDomainFromDb(Lampshade lampshade)
    {
        ArgumentNullException.ThrowIfNull(lampshade);

        return new Domain.Models.Lampshade
        {
            Id = lampshade.Id,
            Code = lampshade.Code,
        };
    }

    private static Domain.Models.Document DocumentDomainFromDb(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);

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
