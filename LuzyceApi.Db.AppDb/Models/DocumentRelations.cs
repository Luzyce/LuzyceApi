namespace LuzyceApi.Db.AppDb.Data.Models;

public class DocumentRelations
{
    public int Id { get; set; }
    public required Document ParentDocument { get; set; }
    public required Document SubordinateDocument { get; set; }

}
