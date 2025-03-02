﻿using MediatR;
using UserService.Application.Services;
using UserService.Application.Users.DTOs;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> logger,
    IUserRepository userRepository,
    JwtService jwtService
       ) : IRequestHandler<RegisterUserCommand, UserTokenDto>
{
    public async Task<UserTokenDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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

        user = await userRepository.AddAsync(user);

        var token = jwtService.GetUserTokenDto(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(token.RefreshTokenExpiryDays);
        await userRepository.UpdateAsync(user);

        return token;
    }
}
