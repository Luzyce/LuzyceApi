using System.ComponentModel.DataAnnotations;

namespace LuzyceApi.Dtos.User;

public class UpdateUserDto
{
    public required string Name { get; set; }
    public required string LastName { get; set; }

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public required string Login { get; set; }
    public required string Hash { get; set; }
    public required bool Admin { get; set; }
}
