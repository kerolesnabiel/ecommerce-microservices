using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.SellerAccounts.Commands.AddSellerAccount;
using UserService.Application.SellerAccounts.Commands.UpdateSellerAccount;

namespace UserService.Presentation.Controllers;

[Route("api/users/me/seller")]
[ApiController]
[Authorize]
public class SellerAccountsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddSellerAccount(AddSellerAccountCommand command)
    {
        await mediator.Send(command);
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSellerAccount(UpdateSellerAccountCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}
