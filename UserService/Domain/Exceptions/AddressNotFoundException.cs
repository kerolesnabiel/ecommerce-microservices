namespace UserService.Domain.Exceptions;

public class AddressNotFoundException(string id) 
    : Exception($"Address with identifier: {id} not found")
{
}
