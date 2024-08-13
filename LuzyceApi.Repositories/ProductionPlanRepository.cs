using Luzyce.Core.Models.Document;
using Luzyce.Core.Models.Lampshade;
using Luzyce.Core.Models.ProductionOrder;
using Luzyce.Core.Models.ProductionPlan;
using Luzyce.Core.Models.User;
using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.AppDb.Models;
using Microsoft.EntityFrameworkCore;

namespace LuzyceApi.Repositories;

public class ProductionPlanRepository(ApplicationDbContext applicationDbContext)
{
    public GetProductionPlans GetProductionPlans(GetMonthProductionPlanRequest request)
    {
        return new GetProductionPlans
        {
            ProductionPlans = applicationDbContext.ProductionPlans
                .Where(x => x.Date.Month == request.ProductionPlanDate.Month && x.Date.Year == request.ProductionPlanDate.Year)
                .Select(x => new GetProductionPlanForCalendar
                {
                    Id = x.Id,
                    Date = x.Date,
                    Change = x.Change,
                    Team = x.Team,
                    ShiftSupervisor = x.ShiftSupervisor == null ? null : new GetUserResponseDto
                    {
                        Id = x.ShiftSupervisor.Id,
                        Name = x.ShiftSupervisor.Name
                    },
                    Status = x.Status == null ? null : new GetStatusResponseDto
                    {
                        Id = x.Status.Id,
                        Name = x.Status.Name
                    }
                })
                .ToList()
        };
    }
    
    public int AddPositionsToProductionPlan(AddPositionsToProductionPlan request)
    {
        using var transaction = applicationDbContext.Database.BeginTransaction();
        try
        {
            var productionPlan = applicationDbContext.ProductionPlans
                .FirstOrDefault(x => x.Date == request.Date && x.Team == request.Team && x.Change == request.Change);
                
            if (productionPlan == null)
            {
                productionPlan = new ProductionPlan
                {
                    Date = request.Date,
                    Change = request.Change,
                    Team = request.Team,
                    StatusId = 1
                };
                applicationDbContext.ProductionPlans.Add(productionPlan);
                applicationDbContext.SaveChanges();
            }
                
            foreach (var position in request.Positions)
            {
                var productionPlanPositions = new ProductionPlanPositions
                {
                    ProductionPlanId = productionPlan.Id,
                    Quantity = position.Value,
                    DocumentPositionId = position.Key
                };
                    
                applicationDbContext.ProductionPlanPositions.Add(productionPlanPositions);
                applicationDbContext.SaveChanges();
                    
                var currentYear = DateTime.Now.Year;
                var docNumber = applicationDbContext.Documents
                    .Where(d => d.WarehouseId == Dictionaries.Warehouses.MAG_ID
                                && d.Year == currentYear
                                && d.DocumentsDefinitionId == Dictionaries.DocumentsDefinitions.KW_ID)
                    .Select(d => d.DocNumber)
                    .ToList()
                    .DefaultIfEmpty(0)
                    .Max() + 1;
                    
                var kwit = new Document
                {
                    DocNumber = docNumber,
                    WarehouseId = Dictionaries.Warehouses.MAG_ID,
                    Year = currentYear,
                    Number = $"{Dictionaries.Warehouses.MAG_CODE}/{docNumber:D4}/{Dictionaries.DocumentsDefinitions.KW_CODE}/{currentYear}",
                    DocumentsDefinitionId = Dictionaries.DocumentsDefinitions.KW_ID,
                    OperatorId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    ClosedAt = null,
                    StatusId = 1,
                    ProductionPlanPositionsId = productionPlanPositions.Id
                };
                    
                applicationDbContext.Documents.Add(kwit);
                applicationDbContext.SaveChanges();
                    
                var documentPosition = applicationDbContext.DocumentPositions
                    .Include(dp => dp.Document)
                    .Include(dp => dp.Lampshade)
                    .Include(dp => dp.LampshadeNorm)
                    .ThenInclude(ln => ln!.Variant)
                    .Include(op => op.OrderPositionForProduction)
                    .FirstOrDefault(dp => dp.Id == position.Key);
                    
                if (documentPosition == null)
                {
                   transaction.Rollback();
                   return 0;
                }

                var newDocumentPosition = new DocumentPositions
                {
                    DocumentId = kwit.Id,
                    OperatorId = documentPosition.OperatorId,
                    StartTime = DateTime.Now,
                    LampshadeId = documentPosition.LampshadeId,
                    LampshadeNormId = documentPosition.LampshadeNormId,
                    Remarks = documentPosition.Remarks,
                    OrderPositionForProductionId = documentPosition.OrderPositionForProductionId
                };
                    
                applicationDbContext.DocumentPositions.Add(newDocumentPosition);
            }
                
            applicationDbContext.SaveChanges();
            transaction.Commit();
                
            return 1;
        }
        catch (Exception)
        {
            transaction.Rollback();
            return 0;
        }
    }
    
    public GetProductionPlan GetProductionPlan(GetProductionPlanPositionsRequest request)
    {
        var productionPlan = applicationDbContext.ProductionPlans
            .Include(x => x.ShiftSupervisor)
            .Include(x => x.Status)
            .FirstOrDefault(x => x.Date == request.Date && x.Change == request.Change && x.Team == request.Team);
        
        if (productionPlan == null)
        {
            return new GetProductionPlan();
        }
        
        return new GetProductionPlan
        {
            Id = productionPlan.Id,
            Date = productionPlan.Date,
            Change = productionPlan.Change,
            Team = productionPlan.Team,
            ShiftSupervisor = productionPlan.ShiftSupervisor == null ? null : new GetUserResponseDto
            {
                Id = productionPlan.ShiftSupervisor.Id,
                Name = productionPlan.ShiftSupervisor.Name,
                LastName = productionPlan.ShiftSupervisor.LastName
            },
            Status = productionPlan.Status == null ? null : new GetStatusResponseDto
            {
                Id = productionPlan.Status.Id,
                Name = productionPlan.Status.Name
            },
            ProductionPlanPositions = applicationDbContext.ProductionPlanPositions
                .Where(x => x.ProductionPlan!.Id == productionPlan.Id)
                .Include(x => x.DocumentPosition)
                    .ThenInclude(dp => dp!.Document)
                .Include(x => x.DocumentPosition)
                    .ThenInclude(dp => dp!.Lampshade)
                .Include(x => x.DocumentPosition)
                    .ThenInclude(dp => dp!.LampshadeNorm)
                        .ThenInclude(ln => ln!.Variant)
                .Include(x => x.DocumentPosition)
                    .ThenInclude(dp => dp!.LampshadeNorm)
                        .ThenInclude(ln => ln!.Lampshade)
                .Include(x => x.DocumentPosition)
                    .ThenInclude(dp => dp!.OrderPositionForProduction)
                        .ThenInclude(op => op!.Order)
                .Include(x => x.Kwit)
                    .ThenInclude(k => k.DocumentPositions)
                .Include(x => x.HeadsOfMetallurgicalTeams)
                .ToList()
                .Select(x => new GetProductionPlanPosition
                {
                    Id = x.Id,
                    DocumentPosition = new GetProductionOrderPosition
                    {
                        Id = x.DocumentPosition!.Id,
                        QuantityNetto = x.DocumentPosition.QuantityNetto,
                        QuantityGross = x.DocumentPosition.QuantityGross,
                        ExecutionDate = x.DocumentPosition.EndTime,
                        Lampshade = new GetLampshade
                        {
                            Id = x.DocumentPosition.Lampshade!.Id,
                            Code = x.DocumentPosition.Lampshade.Code
                        },
                        LampshadeNorm = new GetLampshadeNorm()
                        {
                            Id = x.DocumentPosition.LampshadeNorm!.Id,
                            Lampshade = new GetLampshade
                            {
                                Id = x.DocumentPosition.LampshadeNorm.Lampshade!.Id,
                                Code = x.DocumentPosition.LampshadeNorm.Lampshade.Code
                            },
                            Variant = new GetVariantResponseDto
                            {
                                Id = x.DocumentPosition.LampshadeNorm.Variant!.Id,
                                Name = x.DocumentPosition.LampshadeNorm.Variant.Name,
                                ShortName = x.DocumentPosition.LampshadeNorm.Variant.ShortName
                            },
                            QuantityPerChange = x.DocumentPosition.LampshadeNorm.QuantityPerChange ?? 0,
                            WeightBrutto = x.DocumentPosition.LampshadeNorm.WeightBrutto,
                            WeightNetto = x.DocumentPosition.LampshadeNorm.WeightNetto
                        },
                        LampshadeDekor = x.DocumentPosition.LampshadeDekor,
                        Remarks = x.DocumentPosition.Remarks,
                        NumberOfChanges = x.DocumentPosition.po_NumberOfChanges,
                        QuantityMade = x.DocumentPosition.po_QuantityMade,
                        MethodOfPackaging = x.DocumentPosition.MethodOfPackaging,
                        QuantityPerPack = x.DocumentPosition.QuantityPerPack,
                        ProductId = x.DocumentPosition.SubiektProductId ?? 0,
                        Unit = x.DocumentPosition.OrderPositionForProduction!.Unit!,
                        ProductionOrderNumber = x.DocumentPosition.Document!.Number,
                        Client = x.DocumentPosition.OrderPositionForProduction.Order!.CustomerName,
                        Priority = x.DocumentPosition.Priority ?? 0
                    },
                    HeadsOfMetallurgicalTeamsId = x.HeadsOfMetallurgicalTeamsId,
                    HeadsOfMetallurgicalTeams = x.HeadsOfMetallurgicalTeams == null ? null : new GetUserResponseDto
                    {
                        Id = x.HeadsOfMetallurgicalTeams.Id,
                        Name = x.HeadsOfMetallurgicalTeams.Name,
                        LastName = x.HeadsOfMetallurgicalTeams.LastName
                    },
                    NumberOfHours = x.NumberOfHours,
                    Kwit = x.Kwit.Count == 0 ? null : new GetDocumentWithPositions
                    {
                        Id = x.Kwit.First().Id,
                        Number = x.Kwit.First().Number,
                        DocumentPositions = x.Kwit.First().DocumentPositions
                            .Select(dp => new GetDocumentPositionResponseDto
                            {
                                Id = dp.Id,
                                QuantityNetto = dp.QuantityNetto,
                                QuantityLoss = dp.QuantityLoss,
                                QuantityToImprove = dp.QuantityToImprove
                            })
                            .ToList()
                    }
                })
                .ToList()
        };
    }
    
    public void DeletePosition(int id)
    {
        var position = applicationDbContext.ProductionPlanPositions.FirstOrDefault(x => x.Id == id);

        if (position == null)
        {
            return;
        }
        
        var productionPlan = applicationDbContext.ProductionPlans.FirstOrDefault(x => x.Id == position.ProductionPlanId);
        
        if (productionPlan == null)
        {
            return;
        }
        
        var document = applicationDbContext.Documents.FirstOrDefault(x => x.DocumentPositions.Any(dp => dp.Id == position.Kwit.First().Id));
        
        if (document == null)
        {
            return;
        }
        
        var documentPosition = applicationDbContext.DocumentPositions.FirstOrDefault(x => x.Id == document.DocumentPositions.First().Id);
        
        if (documentPosition == null)
        {
            return;
        }
        
        if (applicationDbContext.ProductionPlanPositions.Count(x => x.ProductionPlanId == productionPlan.Id) == 1)
        {
            applicationDbContext.ProductionPlans.Remove(productionPlan);
        }
        
        applicationDbContext.ProductionPlanPositions.Remove(position);
        applicationDbContext.DocumentPositions.Remove(documentPosition);
        applicationDbContext.Documents.Remove(document);
        applicationDbContext.SaveChanges();
    }

    public IEnumerable<GetUserResponseDto> ShiftSupervisor()
    {
        return applicationDbContext.Users
            // .Where(x => x.Role!.Name == "ShiftSupervisor")
            .Select(x => new GetUserResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                LastName = x.LastName
            })
            .ToList();
    }
    
    public IEnumerable<GetUserResponseDto> GetHeadsOfMetallurgicalTeams()
    {
        return applicationDbContext.Users
            // .Where(x => x.Role!.Name == "HeadOfMetallurgicalTeam")
            .Select(x => new GetUserResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                LastName = x.LastName
            })
            .ToList();
    }
    
    public void UpdateProductionPlan(UpdateProductionPlan request)
    {
        var productionPlan = applicationDbContext.ProductionPlans
            .FirstOrDefault(x => x.Id == request.Id);
        
        if (productionPlan == null)
        {
            return;
        }
        
        productionPlan.ShiftSupervisorId = request.ShiftSupervisorId;
        
        foreach (var position in request.ProductionPlanPositions)
        {
            var productionPlanPosition = applicationDbContext.ProductionPlanPositions
                .Include(productionPlanPositions => productionPlanPositions.DocumentPosition!)
                .ThenInclude(documentPositions => documentPositions.LampshadeNorm!)
                .FirstOrDefault(x => x.Id == position.Id);
            
            if (productionPlanPosition == null)
            {
                continue;
            }
            
            productionPlanPosition.HeadsOfMetallurgicalTeamsId = position.GetHeadsOfMetallurgicalTeamsId;
            productionPlanPosition.NumberOfHours = position.NumberOfHours;
            productionPlanPosition.DocumentPosition!.LampshadeNorm!.WeightNetto = position.WeightNetto;
            productionPlanPosition.DocumentPosition!.LampshadeNorm!.WeightBrutto = position.WeightBrutto;
            productionPlanPosition.DocumentPosition!.LampshadeNorm!.QuantityPerChange = position.QuantityPerChange;
        }
        
        applicationDbContext.SaveChanges();
    }
    
    public Document? GetKwit(int id)
    {
        var document = applicationDbContext.Documents
            .Include(d => d.Warehouse)
            .Include(d => d.Operator)
            .Include(d => d.Status)
            .Include(d => d.DocumentsDefinition)
            .Include(d => d.ProductionPlanPositions)
            .Include(d => d.DocumentPositions)
            .ThenInclude(dp => dp.Lampshade)
            .Include(d => d.DocumentPositions)
            .ThenInclude(dp => dp.LampshadeNorm)
            .ThenInclude(ln => ln!.Variant)
            .Include(d => d.DocumentPositions)
            .ThenInclude(dp => dp.LampshadeNorm)
            .ThenInclude(ln => ln!.Lampshade)
            .Include(d => d.DocumentPositions)
            .ThenInclude(dp => dp.OrderPositionForProduction)
            .Where(x => x.DocumentsDefinitionId == Dictionaries.DocumentsDefinitions.KW_ID)
            .FirstOrDefault(x => x.Id == id);

        return document ?? null;
    }
    
    public List<ProductionPlan> GetProductionPlanPdf(DateOnly data)
    {
        return applicationDbContext.ProductionPlans
            .Include(x => x.ShiftSupervisor)
            .Include(x => x.Status)
            .Include(x => x.Positions)
            .ThenInclude(x => x.Kwit)
            .Where(x => x.Date == data)
            .ToList();
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
    public static Domain.Models.Role RoleDomainFromDb(Db.AppDb.Models.Role role)
    {
        ArgumentNullException.ThrowIfNull(role);

        return new Domain.Models.Role
        {
            Id = role.Id,
            Name = role.Name
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
}