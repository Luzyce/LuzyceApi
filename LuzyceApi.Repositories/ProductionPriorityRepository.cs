using Luzyce.Core.Models.ProductionPriority;
using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.AppDb.Models;

namespace LuzyceApi.Repositories;

public class ProductionPriorityRepository(ApplicationDbContext applicationDbContext)
{
    private readonly ApplicationDbContext applicationDbContext = applicationDbContext;
    
    public int UpdatePriorities(UpdateProductionPrioritiesRequest createProductionPriorityRequest)
    {
        using var transaction = applicationDbContext.Database.BeginTransaction();

        try
        {
            for (var i = 0; i < createProductionPriorityRequest.Positions.Count; i++)
            {
                var documentPosition = applicationDbContext.DocumentPositions
                    .FirstOrDefault(x => x.Id == createProductionPriorityRequest.Positions[i].Id);
                
                if (documentPosition == null)
                {
                    transaction.Rollback();
                    return 0;
                }
                
                documentPosition.Priority = (i + 1) * 10;
                applicationDbContext.DocumentPositions.Update(documentPosition);
            }
            
            applicationDbContext.SaveChanges();
            transaction.Commit();

            return 1;
        }
        catch
        {
            transaction.Rollback();
            return 0;
        }
    }
}