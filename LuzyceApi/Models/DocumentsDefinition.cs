using System.ComponentModel.DataAnnotations;

namespace LuzyceApi.Models;

public class DocumentsDefinition
{
    public int Id { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(1)]
    public required char Code { get; set; }
    public required string Name { get; set; }
}
