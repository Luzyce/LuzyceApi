namespace LuzyceApi.Db.AppDb.Data.Models;

public class Document
{
    public int Id { get; set; }
    public required string Number { get; set; }
    public required Warehouse Warehouse { get; set; }
    public required int Year { get; set; }
    public required User Operator { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime ClosedAt { get; set; }
    public required Status Status { get; set; }
    public required DocumentsDefinition DocumentsDefinition { get; set; }
}
