using System.Security.Claims;
using LuzyceApi.Db.AppDb.Data;
using LuzyceApi.Db.AppDb.Models;

namespace LuzyceApi.Repositories;

public class LogRepository(ApplicationDbContext applicationDbContext)
{
    private readonly ApplicationDbContext applicationDbContext = applicationDbContext;

    public void AddLog(int clientId, string hash, string data)
    {
        applicationDbContext.Logs.Add(new Log
        {
            Timestamp = DateTime.Now,
            ClientId = clientId,
            Operation = "Nieudane logowanie",
            Hash = hash,
            Data = data
        });
        applicationDbContext.SaveChanges();
    }

    public void AddLog(ClaimsPrincipal? user, string operation, string? data)
    {
        applicationDbContext.Logs.Add(new Log
        {
            Timestamp = DateTime.Now,
            ClientId = GetId(ClaimTypes.PrimarySid),
            Operation = operation,
            UserId = GetId(ClaimTypes.NameIdentifier),
            Hash = user?.FindFirstValue(ClaimTypes.Hash) ?? "",
            Data = data
        });
        applicationDbContext.SaveChanges();
        return;

        int? GetId(string claimType) =>
            int.TryParse(user?.FindFirstValue(claimType), out var id) && id > 0 ? id : null;
    }
}