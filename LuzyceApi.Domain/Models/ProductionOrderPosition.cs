namespace LuzyceApi.Domain.Models;

public class ProductionOrderPosition
{
    public int DocumentId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public int Gross { get; set; }
    public int Net { get; set; }
}