using LuzyceApi.Db.AppDb.Models;

namespace LuzyceApi.Db.AppDb.Data.Models;

public class DocumentPositions
{
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public Document? Document { get; set; }
    public int QuantityNetto { get; set; }
    public int QuantityLoss { get; set; }
    public int QuantityToImprove { get; set; }
    public int QuantityGross { get; set; }
    public int OperatorId { get; set; }
    public User? Operator { get; set; }
    public required DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int StatusId { get; set; }
    public Status? Status { get; set; }
    public int LampshadeId { get; set; }
    public Lampshade? Lampshade { get; set; }
    public int? LampshadeNormId { get; set; }
    public LampshadeNorm? LampshadeNorm { get; set; }
    public int? LampshadeDekorId { get; set; }
    public LampshadeDekor? LampshadeDekor { get; set; }
    public int? OrderPositionForProductionId { get; set; }
    public OrderPositionForProduction? OrderPositionForProduction { get; set; }
}
