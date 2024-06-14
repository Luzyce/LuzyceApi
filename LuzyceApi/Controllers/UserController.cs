using Luzyce.Core.Models.User;
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
        return Ok(
            usersRepository.GetUsers()
                .Select(x => new GetUserResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                    Login = x.Login
                })
                .ToList());
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

        return Ok(
            new GetUserForUpdateDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email ?? "",
                Login = user.Login,
                Hash = user.Hash,
                Role = new GetRoleDto
                {
                    Id = user.Role!.Id,
                    Name = user.Role.Name
                }

            });
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post([FromBody] CreateUserDto dto)
    {
        var user = UserMappers.ToUserFromCreateDto(dto);
        usersRepository.AddUser(user);

        return CreatedAtAction(
            nameof(Get),
            new { id = user.Id },
            new GetUserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Login = user.Login
            });
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

        user = UserMappers.UpdateUserFromDto(dto, user);

        usersRepository.UpdateUser(user);
        return Ok(new GetUserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Login = user.Login
        });
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
        return Ok();
    }

    [HttpGet("roles")]
    [Authorize]
    public IActionResult GetRoles()
    {
        return Ok(
            usersRepository.GetRoles()
                .Select(x => new GetRoleDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList());
    }
}
