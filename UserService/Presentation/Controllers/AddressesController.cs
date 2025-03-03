using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Addresses.Commands.AddAddress;
using UserService.Application.Users.Queries.GetUser;

namespace UserService.Presentation.Controllers;

[Route("api/users/me/addresses")]
[ApiController]
[Authorize]
public class AddressesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddAddress(AddAddressCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}
