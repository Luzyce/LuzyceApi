using Luzyce.Core.Models.Document;
using Luzyce.Core.Models.ProductionPlan;
using Luzyce.Core.Models.User;
using LuzyceApi.Db.AppDb.Data;

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
}