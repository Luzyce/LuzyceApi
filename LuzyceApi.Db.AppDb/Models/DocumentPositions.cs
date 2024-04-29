namespace LuzyceApi.Db.AppDb.Data.Models;

public class DocumentPositions
{
    public int Id { get; set; }
    public required Document DocumentID { get; set; }
    public required int NetQuantity { get; set; }
    public required int QuantityLoss { get; set; }
    public required int QuantityToImprove { get; set; }
    public required int GrossQuantity { get; set; }
    public required User OperatorID { get; set; }
    public required DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public required Status StatusID { get; set; }
    public required Lampshade LampshadeID { get; set; }
}
