using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
// using LuzyceApi.Data;
using LuzyceApi.Dtos.User;
using LuzyceApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using LuzyceApi.Repositories;

namespace LuzyceApi.Controllers;
[Route("api/login")]
[ApiController]
public class LoginController(IConfiguration config, UsersRepository usersRepository) : Controller
{
    private readonly IConfiguration config = config;
    private readonly UsersRepository usersRepository = usersRepository;

    private string generateJSONWebToken(User user, bool isHashLogin)
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.Login),
                new Claim(ClaimTypes.Email, string.IsNullOrEmpty(user.Email) ? "" : user.Email),
                new Claim(ClaimTypes.Role, user.Admin ? Roles.ADMIN : Roles.USER),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.SerialNumber, string.IsNullOrEmpty(user.Hash) ? "" : user.Hash)
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: (isHashLogin) ? DateTime.Now.AddHours(13) : DateTime.Now.AddHours(1),
            notBefore: DateTime.Now,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SigningKey"] ?? "")),
                SecurityAlgorithms.HmacSha256
                )
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        bool isHashLogin = !string.IsNullOrEmpty(dto.Hash);
        User? user = isHashLogin ? usersRepository.GetUserByHash(dto.Hash)
            : usersRepository.GetUserByLoginAndPassword(dto.Login, dto.Password);

        if (user == null)
        {
            return NotFound();
        }

        var tokenString = generateJSONWebToken(user, isHashLogin);
        var result = new { user.Id, user.Name, user.LastName, user.Login };
        return Ok(new { token = tokenString, result });
    }
}
