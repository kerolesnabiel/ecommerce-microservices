using CartService.Data;
using CartService.Models;
using CartService.User;
using ProductService;

namespace CartService.Features.Cart.AddItemToCart;

public class AddItemToCartCommandHandler(
    ICartRepository cartRepository, IUserContext userContext,
    ProductServiceProto.ProductServiceProtoClient productService) 
        : IRequestHandler<AddItemToCartCommand>
{
    public async Task Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var product = await productService.GetProductAsync(new GetProductRequest { Id = request.ProductId }, cancellationToken: cancellationToken);

        if (product.StockQuantity < request.Quantity)
            throw new BadHttpRequestException($"Insufficient stock: Requested {request.Quantity} but only {product.StockQuantity} items are available.");

        var currentUser = userContext.GetCurrentUser();
        var cart = await cartRepository.GetByIdAsync(currentUser.Id)
            ?? new() { Id = currentUser.Id };

        CartItem cartItem = request.Adapt<CartItem>();
        cartItem.Price = (decimal)product.Price;

        var existingItem = cart.Items.FirstOrDefault(x => x.ProductId == cartItem.ProductId);

        if (existingItem != null)
            ValidateAndUpdateItemQuantity(existingItem, request.Quantity, product);
        else
            cart.Items.Add(cartItem);

        cart.UpdatedAt = DateTime.UtcNow;
        await cartRepository.AddCartAsync(cart, cancellationToken);
    }

    private void ValidateAndUpdateItemQuantity(CartItem existingItem, int requestedQuantity, GetProductResponse product)
    {
        if (existingItem.Quantity + requestedQuantity > product.StockQuantity)
        {
            throw new BadHttpRequestException(
                $"Insufficient stock: You currently have {existingItem.Quantity} of this item in your cart. You can only add {product.StockQuantity - existingItem.Quantity} more, but you are trying to add {requestedQuantity}. Available stock: {product.StockQuantity}.");
        }

        existingItem.Quantity += requestedQuantity;
        existingItem.Price = (decimal)product.Price;
    }
}
