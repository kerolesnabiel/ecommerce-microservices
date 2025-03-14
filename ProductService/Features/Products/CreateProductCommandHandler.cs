using ProductService.Models;

namespace ProductService.Features.Products;

public class CreateProductCommandHandler(IDocumentSession session) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = request.Adapt<Product>();

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}