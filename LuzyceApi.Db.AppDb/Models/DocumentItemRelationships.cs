namespace LuzyceApi.Db.AppDb.Data.Models;

public class DocumentItemRelationships
{
    public int Id { get; set; }
    public required Document ParentDocumentID { get; set; }
    public required Document SubordinateDocumentID { get; set; }
    public required DocumentPositions ParentPositionID { get; set; }
    public required DocumentPositions SubordinatePositionID { get; set; }
    public required int NetQuantityParent { get; set; }
    public required int QuantityLossParent { get; set; }
}
