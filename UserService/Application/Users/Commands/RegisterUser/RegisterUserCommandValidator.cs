using FluentValidation;

namespace UserService.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty()
            .MaximumLength(25);

        RuleFor(u => u.LastName)
            .NotEmpty()
            .MaximumLength(25);

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}
