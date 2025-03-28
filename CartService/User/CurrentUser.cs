namespace CartService.User;

public record CurrentUser(Guid Id, string Role, string? SellerId)
{
}
