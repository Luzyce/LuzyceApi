using Luzyce.Core.Models.User;
using LuzyceApi.Domain.Models;

namespace LuzyceApi.Mappers;

public static class UserMappers
{
    public static User ToUserFromCreateDto(this CreateUserDto dto)
    {
        return new User
        {
            Name = dto.Name,
            LastName = dto.LastName,
            Email = dto.Email,
            Login = dto.Login,
            Password = dto.Password,
            Hash = dto.Hash
        };
    }

    public static User UpdateUserFromDto(this UpdateUserDto dto, User user)
    {
        user.Name = dto.Name;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.Login = dto.Login;
        user.Hash = dto.Hash;
        user.RoleId = dto.RoleId;
        return user;
    }

    public static User UpdateUserPasswordFromDto(this UpdatePasswordDto dto, User user)
    {
        user.Password = dto.NewPassword;
        return user;
    }
}
