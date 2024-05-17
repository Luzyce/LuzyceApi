namespace LuzyceApi.Db.AppDb.Data.Models;

public class DocumentItemRelationships
{
    public int Id { get; set; }
    public required Document ParentDocument { get; set; }
    public required Document SubordinateDocument { get; set; }
    public required DocumentPositions ParentPosition { get; set; }
    public required DocumentPositions SubordinatePosition { get; set; }
    public required int NetQuantityParent { get; set; }
    public required int QuantityLossParent { get; set; }
}
