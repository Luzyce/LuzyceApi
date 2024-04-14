namespace LuzyceApi.Models;

public class Document
{
    public int Id { get; set; }
    public required string Number { get; set; }
    public required Warehouse WarehouseID { get; set; }
    public required int Year { get; set; }
    public required User OperatorID { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ClosedAt { get; set; }
    public required Status StatusID { get; set; }
    public required DocumentsDefinition DocumentDefinitionID { get; set; }
}
