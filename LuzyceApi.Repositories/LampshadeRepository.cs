using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.AppDb.Models;

namespace LuzyceApi.Repositories;

public class LampshadeRepository(ApplicationDbContext applicationDbContext)
{
    private readonly ApplicationDbContext applicationDbContext = applicationDbContext;
    
    public List<LampshadeVariant> GetLampshadeVariants()
    {
        return applicationDbContext.LampshadeVariants.ToList();
    }
    
    public LampshadeVariant? GetLampshadeVariant(string shortName)
    {
        return applicationDbContext.LampshadeVariants.FirstOrDefault(x => x.ShortName == shortName);
    }
    
    public List<LampshadeDekor> GetLampshadeDekors()
    {
        return applicationDbContext.LampshadeDekors.ToList();
    }
    
    public LampshadeDekor? GetLampshadeDekor(string shortName)
    {
        return applicationDbContext.LampshadeDekors.FirstOrDefault(x => x.ShortName == shortName);
    }
    
}