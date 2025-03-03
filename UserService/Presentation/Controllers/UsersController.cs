using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Users.Commands.UpdateUser;

namespace UserService.Presentation.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPut("me")]
    public async Task<IActionResult> UpdateUserAsync(UpdateUserCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}
