using System.Security.Claims;
using UserService.Domain.Exceptions;

namespace UserService.Application.Users;

public interface IUserContext
{
    CurrentUser GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User
            ?? throw new InvalidOperationException("User context is not present");

        if (user.Identity == null || !user.Identity.IsAuthenticated)
            throw new UnauthorizedException();

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var role = user.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;

        return new CurrentUser(Guid.Parse(userId), role);
    }
}
