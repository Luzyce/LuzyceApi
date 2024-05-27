using System.ComponentModel.DataAnnotations;

namespace LuzyceApi.Db.AppDb.Data.Models;

public class Document
{
    public int Id { get; set; }
    public int DocNumber { get; set; }
    public int WarehouseId { get; set; }

    [Required]
    public Warehouse? Warehouse { get; set; }
    public int Year { get; set; }
    public required string Number { get; set; }
    public int DocumentsDefinitionId { get; set; }

    [Required]
    public DocumentsDefinition? DocumentsDefinition { get; set; }
    public int OperatorId { get; set; }

    [Required]
    public User? Operator { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public int StatusId { get; set; }

    [Required]
    public Status? Status { get; set; }
    public string? lockedBy { get; set; }
}
