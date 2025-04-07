namespace CartService.Features.Cart.Checkout;

public class CheckoutCommand : IRequest
{
    public string CardToken { get; set; } = default!;
}
