namespace LuzyceApi.Db.AppDb.Data.Models;

public class DocumentRelations
{
    public int Id { get; set; }
    public required Document ParentDocumentID { get; set; }
    public required Document SubordinateDocumentID { get; set; }

}
