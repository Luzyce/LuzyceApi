using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuzyceApi.Dtos.User
{
    public class LoginDto
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}