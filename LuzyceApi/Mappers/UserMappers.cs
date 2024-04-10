using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuzyceApi.Dtos.User;
using LuzyceApi.Models;

namespace LuzyceApi.Mappers
{
    public static class UserMappers
    {
        public static User ToUserFromCreateDto(this CreateUserDto dto)
        {
            return new User
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Login = dto.Login,
                Password = dto.Password,
                Hash = dto.Hash,
                Admin = false
            };
        }
    }
}
