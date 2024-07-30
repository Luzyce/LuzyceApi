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
                .Select(x => new GetProductionPlan
                {
                    Id = x.Id,
                    Date = x.Date,
                    Change = x.Change,
                    Team = x.Team,
                    Metallurgist = x.Metallurgist == null ? null : new GetUserResponseDto
                    {
                        Id = x.Metallurgist.Id,
                        Name = x.Metallurgist.Name
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
                DocumentPositionId = position.Key
            });
        }
        
        applicationDbContext.SaveChanges();
        
        return 1;
    }
    
    public GetProductionPlanPositions GetProductionPlanPositions(GetProductionPlanPositionsRequest request)
    {
        return new GetProductionPlanPositions
        {
            ProductionPlanPositions = applicationDbContext.ProductionPlanPositions
                .Where(x => x.ProductionPlan!.Date == request.Date && x.ProductionPlan.Team == request.Team && x.ProductionPlan.Change == request.Change)
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
                    Id = x.DocumentPosition!.Id,
                    DocumentPosition = new GetProductionOrderPosition
                    {
                        Id = x.DocumentPosition.Id,
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
                            QuantityPerChange = x.DocumentPosition.LampshadeNorm.QuantityPerChange ?? 0
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
                    NumberOfHours = x.NumberOfHours
                })
                .ToList()
        };
    }
}