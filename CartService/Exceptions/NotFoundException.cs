namespace CartService.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) {}

    public NotFoundException(string type, string id)
        : base($"{type} with identifier: {id} not found") { }
}
