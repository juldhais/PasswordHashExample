using Microsoft.EntityFrameworkCore;
using PasswordHashExample.WebAPI.Data;
using PasswordHashExample.WebAPI.Entities;
using PasswordHashExample.WebAPI.Resources;

namespace PasswordHashExample.WebAPI.Services;

public sealed class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly string _pepper;
    private readonly int _iteration = 3;

    public UserService(DataContext context)
    {
        _context = context;
        _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
    }
    
    public async Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Username = resource.Username,
            Email = resource.Email,
            PasswordSalt = PasswordHasher.GenerateSalt()
        };
        user.PasswordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, _pepper, _iteration);
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new UserResource(user.Id, user.Username, user.Email);
    }

    public async Task<UserResource> Login(LoginResource resource, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Username == resource.Username, cancellationToken);

        if (user == null)
            throw new Exception("Username or password did not match.");

        var passwordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, _pepper, _iteration);
        if (user.PasswordHash != passwordHash)
            throw new Exception("Username or password did not match.");
        
        return new UserResource(user.Id, user.Username, user.Email);
    }
}