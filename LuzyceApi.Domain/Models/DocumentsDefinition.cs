using System.ComponentModel.DataAnnotations;

namespace LuzyceApi.Domain.Models;

public class DocumentsDefinition
{
    public int Id { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(2)]
    public required string Code { get; set; }
    public required string Name { get; set; }
}
