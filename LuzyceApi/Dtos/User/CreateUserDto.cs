using System.ComponentModel.DataAnnotations;

namespace LuzyceApi.Dtos.User;

public class CreateUserDto
{
    public required string Name { get; set; }
    public required string LastName { get; set; }

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required string Hash { get; set; }
}
