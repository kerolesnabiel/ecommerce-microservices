using BuildingBlocks.User;
using CartService.Data;
using static PaymentService.PaymentServiceProto;

namespace CartService.Features.Cart.Checkout;

public class CheckoutCommandHandler(
    PaymentServiceProtoClient paymentService,
    ICartRepository cartRepository,
    IUserContext userContext) 
        : IRequestHandler<CheckoutCommand>
{
    public async Task Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser().Id;
        var cart = await cartRepository.GetByIdAsync(userId);

        if (cart == null || cart.Items.Count == 0)
            throw new BadHttpRequestException("Your cart is empty");

        decimal total = cart.Items.Sum(i => i.Price * i.Quantity);
        long amount = (long)total * 100;

        var transactionId = await paymentService.ChargeAsync(new()
                            {
                                UserId = userId.ToString(),
                                CardToken = request.CardToken,
                                Amount = amount,
                                Currency = "usd"
                            }, cancellationToken: cancellationToken);

        // Create the order
        // Clear the cart
    }
} 