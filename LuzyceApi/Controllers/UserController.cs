using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuzyceApi.Data;
using LuzyceApi.Dtos.User;
using LuzyceApi.Mappers;
using LuzyceApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
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