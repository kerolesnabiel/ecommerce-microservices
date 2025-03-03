using FluentValidation;

namespace UserService.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(25)
            .When(x => x.FirstName != null);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(25)
            .When(x => x.LastName != null);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(15)
            .When(x => x.PhoneNumber != null);

        RuleFor(x => x)
            .Must(x => x.FirstName != null || x.LastName != null || x.PhoneNumber != null)
            .WithMessage("At least one field must be modified.");
    }
}
