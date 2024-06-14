using LuzyceApi.Db.AppDb.Data;
using Microsoft.EntityFrameworkCore;
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
            .Include(d => d.Role)
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
                    RoleId = x.RoleId,
                    Role = RoleDomainFromDb(x.Role!)
                }
            )
            .FirstOrDefault(x => x.Hash == hash);
    }

    public Domain.Models.User? GetUserByLoginAndPassword(string login, string password)
    {
        logger.LogInformation($"Getting user by login: {login}");

        var user = applicationDbContext.Users
                .Include(d => d.Role)
                .FirstOrDefault(x => x.Login == login);

        logger.LogInformation($"Getting user by login: {user.RoleId}");

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
            RoleId = user.RoleId,
            Role = RoleDomainFromDb(user.Role!)
        };
    }

    public IEnumerable<Domain.Models.User> GetUsers()
    {
        logger.LogInformation("Getting all users");
        return applicationDbContext
            .Users
            .Include(d => d.Role)
            .Select(
                x => new Domain.Models.User
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                    Email = x.Email,
                    Login = x.Login,
                    Hash = x.Hash,
                    CreatedAt = x.CreatedAt,
                    RoleId = x.RoleId,
                    Role = RoleDomainFromDb(x.Role!)
                }
            )
            .ToList();
    }

    public Domain.Models.User? GetUserById(int id)
    {
        logger.LogInformation($"Getting user by id: {id}");
        return applicationDbContext
            .Users
            .Include(d => d.Role)
            .Select(
                x => new Domain.Models.User
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                    Email = x.Email,
                    Login = x.Login,
                    Hash = x.Hash,
                    CreatedAt = x.CreatedAt,
                    RoleId = x.RoleId,
                    Role = RoleDomainFromDb(x.Role!)
                }
            )
            .FirstOrDefault(x => x.Id == id);
    }

    public void AddUser(Domain.Models.User user)
    {
        logger.LogInformation($"Adding user: {user.Login}");

        if (user.RoleId == 0)
        {
            user.RoleId = 2;
        }

        var dbUser = new Db.AppDb.Data.Models.User
        {
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Login = user.Login,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            Hash = user.Hash,
            CreatedAt = user.CreatedAt,
            RoleId = user.RoleId
        };

        applicationDbContext.Users.Add(dbUser);
        applicationDbContext.SaveChanges();

        user.Id = dbUser.Id;
    }

    public void UpdateUser(Domain.Models.User user)
    {
        logger.LogInformation($"Updating user: {user.Login}");

        var userToUpdate = applicationDbContext.Users
                    .Include(d => d.Role)
                    .FirstOrDefault(x => x.Id == user.Id);

        if (userToUpdate == null)
        {
            return;
        }

        userToUpdate.Name = user.Name;
        userToUpdate.LastName = user.LastName;
        userToUpdate.Email = user.Email;
        userToUpdate.Login = user.Login;
        userToUpdate.Hash = user.Hash;
        userToUpdate.RoleId = user.RoleId;
        userToUpdate.Role = RoleDbFromDomain(user.Role!);

        applicationDbContext.SaveChanges();
    }

    public void DeleteUser(Domain.Models.User user)
    {
        logger.LogInformation($"Deleting user: {user.Login}");

        var userToDelete = applicationDbContext.Users.FirstOrDefault(x => x.Id == user.Id);

        if (userToDelete == null)
        {
            return;
        }

        applicationDbContext.Users.Remove(userToDelete);
        applicationDbContext.SaveChanges();
    }

    public Domain.Models.Role? GetRole(int id)
    {
        logger.LogInformation($"Getting role by id: {id}");
        var role = applicationDbContext.Roles.FirstOrDefault(x => x.Id == id);
        if (role == null)
        {
            return null;
        }
        return RoleDomainFromDb(role);
    }
    public static Domain.Models.Role RoleDomainFromDb(Db.AppDb.Models.Role role)
    {
        return new Domain.Models.Role
        {
            Id = role.Id,
            Name = role.Name
        };
    }
    public static Db.AppDb.Models.Role RoleDbFromDomain(Domain.Models.Role role)
    {
        return new Db.AppDb.Models.Role
        {
            Id = role.Id,
            Name = role.Name
        };
    }
}
