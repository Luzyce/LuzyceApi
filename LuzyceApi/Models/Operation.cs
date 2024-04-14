namespace LuzyceApi.Models;

public class Operation
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }
    public required Document DocumentID { get; set; }
    public required User OperatorID { get; set; }
    public required int NetDeltaQuantity { get; set; }
    public required int QuantityLossDelta { get; set; }
    public required int QuantityToImproveDelta { get; set; }
}
