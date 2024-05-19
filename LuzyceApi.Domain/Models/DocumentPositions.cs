namespace LuzyceApi.Domain.Models;

public class DocumentPositions
{
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public required Document Document { get; set; }
    public required int NetQuantity { get; set; }
    public required int QuantityLoss { get; set; }
    public required int QuantityToImprove { get; set; }
    public required int GrossQuantity { get; set; }
    public int OperatorId { get; set; }
    public required User Operator { get; set; }
    public required DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int StatusId { get; set; }
    public required Status Status { get; set; }
    public int LampshadeId { get; set; }
    public required Lampshade Lampshade { get; set; }
}
