using AutoMapper;
using MediatR;
using UserService.Domain.Exceptions;
using UserService.Domain.Interfaces;

namespace UserService.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler
    (ILogger<ChangePasswordCommandHandler> logger,
    IUserRepository userRepository,
    IUserContext userContext) : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("Changing password for user with id {UserId}", currentUser.Id);

        var user = await userRepository.GetByIdAsync(currentUser.Id)
            ?? throw new UserNotFoundException(currentUser.Id.ToString());

        bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash);
        if (!isValidPassword)
            throw new BadRequestException("Current password is incorrect");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        user.PasswordChangedAt = DateTime.UtcNow;

        await userRepository.UpdateAsync(user);
    }
}
