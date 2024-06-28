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
    
    public LampshadeVariant? GetLampshadeVariant(string name)
    {
        return applicationDbContext.LampshadeVariants.FirstOrDefault(x => x.Name == name);
    }
    
}