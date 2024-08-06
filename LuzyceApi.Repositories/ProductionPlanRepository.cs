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
            applicationDbContext.ProductionPlanPositions.Add(new ProductionPlanPositions()
            {
                ProductionPlanId = productionPlan.Id,
                Quantity = position.Value,
                DocumentPositionId = position.Key
            });
        }
        
        applicationDbContext.SaveChanges();
        
        return 1;
    }
    
    public GetProductionPlan GetProductionPlan(GetProductionPlanPositionsRequest request)
    {
        var productionPlan = applicationDbContext.ProductionPlans
            .Include(x => x.ShiftSupervisor)
            .Include(x => x.Status)
            .FirstOrDefault(x => x.Date == request.Date && x.Team == request.Team && x.Change == request.Change);
        
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
                    .ThenInclude(dp => dp!.OrderPositionForProduction)
                        .ThenInclude(op => op!.Order)
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
                    NumberOfHours = x.NumberOfHours
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
        
        if (applicationDbContext.ProductionPlanPositions.Count(x => x.ProductionPlanId == productionPlan.Id) == 1)
        {
            applicationDbContext.ProductionPlans.Remove(productionPlan);
        }
        
        applicationDbContext.ProductionPlanPositions.Remove(position);
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
}