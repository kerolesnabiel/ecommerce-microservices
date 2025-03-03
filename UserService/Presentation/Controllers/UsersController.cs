using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Presentation.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{

}
