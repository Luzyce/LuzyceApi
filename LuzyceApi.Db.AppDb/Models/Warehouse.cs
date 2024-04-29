using System.ComponentModel.DataAnnotations;

namespace LuzyceApi.Db.AppDb.Data.Models;

public class Warehouse
{
    public int Id { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(1)]
    public required char Code { get; set; }
    public required string Name { get; set; }


}
