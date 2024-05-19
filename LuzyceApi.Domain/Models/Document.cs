namespace LuzyceApi.Domain.Models;

public class Document
{
    public int Id { get; set; }
    public required string Number { get; set; }
    public int WarehouseId { get; set; }
    public required Warehouse Warehouse { get; set; }
    public required int Year { get; set; }
    public int OperatorId { get; set; }
    public required User Operator { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public int StatusId { get; set; }
    public required Status Status { get; set; }
    public int DocumentsDefinitionId { get; set; }
    public required DocumentsDefinition DocumentsDefinition { get; set; }

    public object Select(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}
