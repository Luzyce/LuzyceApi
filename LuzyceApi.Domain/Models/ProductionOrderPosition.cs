namespace LuzyceApi.Domain.Models;

public class ProductionOrderPosition
{
    public int DocumentPositionId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public int VariantId { get; set; }
    public int DekorId { get; set; }
    public int Gross { get; set; }
    public int Net { get; set; }
}