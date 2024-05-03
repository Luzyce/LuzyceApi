using LuzyceApi.Dtos.User;
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
            Hash = dto.Hash,
            Admin = false
        };
    }
}
