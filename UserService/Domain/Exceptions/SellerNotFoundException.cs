namespace UserService.Domain.Exceptions;

public class SellerNotFoundException : Exception
{
    public SellerNotFoundException(string userId) 
        : base($"Seller account not found for the user with id: {userId}")
    {}

    public SellerNotFoundException(Guid id)
    : base($"Seller account with id: {id} not found")
    { }
}
