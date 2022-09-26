using PasswordHashExample.WebAPI.Resources;

namespace PasswordHashExample.WebAPI.Services;

public interface IUserService
{
    Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken);
    Task<UserResource> Login(LoginResource resource, CancellationToken cancellationToken);
}