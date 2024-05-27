namespace LuzyceApi.Db.AppDb.Data.Models;

public class Operation
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }
    public int DocumentId { get; set; }
    public Document? Document { get; set; }
    public int OperatorId { get; set; }
    public User? Operator { get; set; }
    public int QuantityNetDelta { get; set; }
    public int QuantityLossDelta { get; set; }
    public int QuantityToImproveDelta { get; set; }
    public int? ErrorCodeId { get; set; }
    public Error? ErrorCode { get; set; }
}
