using System.ComponentModel.DataAnnotations;

namespace LuzyceApi.Db.AppDb.Models;

public class ProductionPlan
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    [Range(1, 3)]
    public int Change { get; set; }
    [Range(1, 3)]
    public int Team { get; set; }
    public int MetallurgistId { get; set; }
    public User? Metallurgist { get; set; }
    public int StatusId { get; set; }
    public Status? Status { get; set; }
}