namespace ProductService.Exceptions;

public class ProductNotFoundException(Guid id) 
    : Exception($"Product with Id: {id} not found")
{
}
