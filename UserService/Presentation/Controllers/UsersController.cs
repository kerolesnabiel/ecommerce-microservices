using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Users.Commands.LoginUser;
using UserService.Application.Users.Commands.RegisterUser;

namespace UserService.Presentation.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(IMediator mediator): ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(RegisterUserCommand command)
    {
        var token = await mediator.Send(command);
        return Ok(new
        {
            token = token.Token,
            expiryMinutes = token.TokenExpiryMinutes
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync(LoginUserCommand command)
    {
        var token = await mediator.Send(command);
        return Ok(new
        {
            token = token.Token,
            expiryMinutes = token.TokenExpiryMinutes
        });
    }
}
