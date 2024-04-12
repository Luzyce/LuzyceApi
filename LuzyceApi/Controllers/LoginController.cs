using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LuzyceApi.Data;
using LuzyceApi.Dtos.User;
using LuzyceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LuzyceApi.Controllers;
[Route("api/login")]
[ApiController]
public class LoginController : Controller
{
    private readonly ApplicationDbContext context;
    private readonly IConfiguration config;

    public LoginController(ApplicationDbContext context, IConfiguration config)
    {
        this.context = context;
        this.config = config;
    }

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
        User? user = null;
        bool isHashLogin = !string.IsNullOrEmpty(dto.Hash);
        if (isHashLogin)
        {
            user = context.Users.FirstOrDefault(x => x.Hash == dto.Hash);
            if (user == null)
            {
                return NotFound();
            }
        }
        else
        {
            user = context.Users.FirstOrDefault(x => x.Login == dto.Login);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return NotFound();
            }
        }
        var tokenString = generateJSONWebToken(user, isHashLogin);
        var result = new { user.Id, user.Name, user.LastName, user.Login };
        return Ok(new { token = tokenString, result });
    }
}
