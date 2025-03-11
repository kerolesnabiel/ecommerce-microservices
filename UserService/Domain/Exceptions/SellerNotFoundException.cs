namespace UserService.Domain.Exceptions;

public class SellerNotFoundException(string userId) : Exception($"Seller account not found for the user with id: {userId}")
{
}
