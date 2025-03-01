namespace UserService.Application.Users.DTOs;

public class UserTokenDto
{
    public string Token = string.Empty;
    public int TokenExpiryMinutes;
}
