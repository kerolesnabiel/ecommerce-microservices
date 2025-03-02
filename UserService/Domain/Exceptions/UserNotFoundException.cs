namespace UserService.Domain.Exceptions;

public class UserNotFoundException(string id) : Exception($"User with identifier: {id} not found")
{
}
