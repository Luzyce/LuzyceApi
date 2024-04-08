using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuzyceApi.Dtos.User
{
    public class CreateUserDto
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required string Hash { get; set; }
    }
}