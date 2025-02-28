using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Users.Commands.RegisterUser;

namespace UserService.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}
