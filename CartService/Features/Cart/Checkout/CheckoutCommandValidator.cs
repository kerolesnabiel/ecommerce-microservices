namespace CartService.Features.Cart.Checkout;

public class CheckoutCommandValidator : AbstractValidator<CheckoutCommand>
{
    public CheckoutCommandValidator()
    {
        RuleFor(x => x.CardToken)
            .NotEmpty()
            .WithMessage("Card token is required.");
    }
}
