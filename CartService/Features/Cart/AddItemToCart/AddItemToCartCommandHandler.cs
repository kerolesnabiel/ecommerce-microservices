using CartService.Data;
using CartService.Models;
using CartService.User;

namespace CartService.Features.Cart.AddItemToCart;

public class AddItemToCartCommandHandler(
    ICartRepository cartRepository, IUserContext userContext) 
        : IRequestHandler<AddItemToCartCommand>
{
    public async Task Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        // Check for product availability

        CartItem cartItem = request.Adapt<CartItem>();
        var cart = await cartRepository.GetByIdAsync(currentUser.Id);
        if(cart != null)
        {
            cart.Items.Add(cartItem);
            cart.UpdatedAt = DateTime.UtcNow;
            await cartRepository.UpdateCartAsync(cart, cancellationToken);
        }
        else
        {
            cart = new(currentUser.Id);
            cart.Items.Add(cartItem);
            await cartRepository.AddCartAsync(cart, cancellationToken);
        }
    }
}
