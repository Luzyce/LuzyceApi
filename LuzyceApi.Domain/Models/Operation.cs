namespace LuzyceApi.Domain.Models;

public class Operation
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }
    public int DocumentId { get; set; }
    public Document? Document { get; set; }
    public int OperatorId { get; set; }
    public required User? Operator { get; set; }
    public required int NetDeltaQuantity { get; set; }
    public required int QuantityLossDelta { get; set; }
    public required int QuantityToImproveDelta { get; set; }
}
