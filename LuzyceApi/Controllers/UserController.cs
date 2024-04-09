using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuzyceApi.Data;
using LuzyceApi.Dtos.User;
using LuzyceApi.Mappers;
using LuzyceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LuzyceApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        
        public UserController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        private string GenerateJSONWebToken(int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == dto.Login && x.Password == dto.Password);
            if (user == null)
            {
                return NotFound();
            }
            var tokenString = GenerateJSONWebToken(user.Id);
            return Ok(new { token = tokenString });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserDto dto)
        {
            var user = dto.ToUserFromCreateDto();
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
        
    }
}