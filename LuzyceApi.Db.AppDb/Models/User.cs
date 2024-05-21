using System.ComponentModel.DataAnnotations;

namespace LuzyceApi.Db.AppDb.Data.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Hash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public required bool Admin { get; set; } = false;
}