using LuzyceApi.Data;
using LuzyceApi.Dtos.User;
using LuzyceApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext context;

    public UserController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        var users = context.Users.ToList().Select(x => new { x.Id, x.Name, x.LastName, x.Login });
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult Get(int id)
    {
        var user = context.Users.SingleOrDefault(x => x.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        var result = new { user.Id, user.Name, user.LastName, user.Login };
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateUserDto dto)
    {
        var user = dto.ToUserFromCreateDto();
        var hashedUser = dto.ToUserFromCreateDto();
        hashedUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        context.Users.Add(hashedUser);
        context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

}
