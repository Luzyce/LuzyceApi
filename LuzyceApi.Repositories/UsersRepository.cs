using LuzyceApi.Db.AppDb.Data;
using Microsoft.Extensions.Logging;

namespace LuzyceApi.Repositories;

public class UsersRepository(ApplicationDbContext applicationDbContext, ILogger<UsersRepository> logger)
{
    private readonly ApplicationDbContext applicationDbContext = applicationDbContext;

    public Domain.Models.User? GetUserByHash(string hash)
    {
        logger.LogInformation($"Getting user by hash: {hash}");
        return applicationDbContext
            .Users
            .Select(
                x => new Domain.Models.User
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                    Email = x.Email,
                    Login = x.Login,
                    Password = x.Password,
                    Hash = x.Hash,
                    CreatedAt = x.CreatedAt,
                    Admin = x.Admin
                }
            )
            .FirstOrDefault(x => x.Hash == hash);
    }

    public Domain.Models.User? GetUserByLoginAndPassword(string login, string password)
    {
        logger.LogInformation($"Getting user by login: {login}");

        var user = applicationDbContext.Users.FirstOrDefault(x => x.Login == login);

        if (user == null)
        {
            return null;
        }

        // Verify the password
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }

        return new Domain.Models.User
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Login = user.Login,
            Password = user.Password,
            Hash = user.Hash,
            CreatedAt = user.CreatedAt,
            Admin = user.Admin
        };
    }
}
