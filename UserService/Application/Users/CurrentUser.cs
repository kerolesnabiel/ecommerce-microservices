namespace UserService.Application.Users;

public record CurrentUser(Guid Id, string Username, string Role)
{
}
