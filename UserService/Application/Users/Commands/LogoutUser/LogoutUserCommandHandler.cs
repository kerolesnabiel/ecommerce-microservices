﻿using MediatR;
using UserService.Domain.Exceptions;
using UserService.Domain.Interfaces;

namespace UserService.Application.Users.Commands.LogoutUser;

public class LogoutUserCommandHandler(
    IUserRepository userRepository,
    IUserContext userContext) : IRequestHandler<LogoutUserCommand>
{
    public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var currentUser =  userContext.GetCurrentUser();

        var user = await userRepository.GetByIdAsync(currentUser.Id)
            ?? throw new UserNotFoundException(currentUser.Id.ToString());

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        await userRepository.UpdateAsync(user);
    }
}
