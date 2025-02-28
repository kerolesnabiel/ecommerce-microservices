using MediatR;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> logger,
    IUserRepository userRepository

       ) : IRequestHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Registering a new user");

        string hashed = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hashed,
            CreatedAt = DateTime.UtcNow,
        };

        await userRepository.AddAsync(user);
    }
}
