using LuzyceApi.Dtos.User;
using LuzyceApi.Mappers;
using LuzyceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController(UsersRepository usersRepository) : ControllerBase
{

    private readonly UsersRepository usersRepository = usersRepository;

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        var users = usersRepository.GetUsers().Select(x => new { x.Id, x.Name, x.LastName, x.Login });
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult Get(int id)
    {
        var user = usersRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        var result = new { user.Id, user.Name, user.LastName, user.Login };
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post([FromBody] CreateUserDto dto)
    {
        var user = UserMappers.ToUserFromCreateDto(dto);
        usersRepository.AddUser(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Put(int id, [FromBody] UpdateUserDto dto)
    {
        var user = usersRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        usersRepository.UpdateUser(UserMappers.UpdateUserFromDto(dto, user));
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        var user = usersRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        usersRepository.DeleteUser(user);
        return NoContent();
    }

}
