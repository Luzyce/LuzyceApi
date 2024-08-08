using System.ComponentModel.DataAnnotations.Schema;

namespace LuzyceApi.Db.AppDb.Models;

public class Document
{
    public int Id { get; set; }
    public int DocNumber { get; set; }
    public int WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }
    public int Year { get; set; }
    public required string Number { get; set; }
    public int DocumentsDefinitionId { get; set; }
    public DocumentsDefinition? DocumentsDefinition { get; set; }
    public int OperatorId { get; set; }
    public User? Operator { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public int StatusId { get; set; }
    public Status? Status { get; set; }
    public string? LockedBy { get; set; }
    
    [Column("kw_ProductionPlanPositionsId")]
    public int? ProductionPlanPositionsId { get; set; }
    public ProductionPlanPositions? ProductionPlanPositions { get; set; }
    
    public List<DocumentPositions> DocumentPositions { get; set; } = [];
}
