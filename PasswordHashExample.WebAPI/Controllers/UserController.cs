using Microsoft.AspNetCore.Mvc;
using PasswordHashExample.WebAPI.Resources;
using PasswordHashExample.WebAPI.Services;

namespace PasswordHashExample.WebAPI.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterResource resource, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userService.Register(resource, cancellationToken);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginResource resource, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userService.Login(resource, cancellationToken);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }
}