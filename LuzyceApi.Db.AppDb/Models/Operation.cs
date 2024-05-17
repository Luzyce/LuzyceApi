namespace LuzyceApi.Db.AppDb.Data.Models;

public class Operation
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }
    public required Document Document { get; set; }
    public required User Operator { get; set; }
    public required int NetDeltaQuantity { get; set; }
    public required int QuantityLossDelta { get; set; }
    public required int QuantityToImproveDelta { get; set; }
}
