namespace UserService.Domain.Exceptions;

public class IncorrectException(string value) : Exception($"{value} is incorrect")
{
}
