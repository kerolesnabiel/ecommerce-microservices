using MediatR;
using UserService.Application.Services;
using UserService.Application.Users.DTOs;
using UserService.Domain.Entities;
using UserService.Domain.Exceptions;
using UserService.Domain.Interfaces;

namespace UserService.Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler(
    ILogger<LoginUserCommandHandler> logger,
    IUserRepository userRepository,
    JwtService jwtService) 
        : IRequestHandler<LoginUserCommand, UserTokenDto>
{
    public async Task<UserTokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Login user: {@Email}", request.Email);

        var user = await userRepository.GetByEmailAsync(request.Email) 
            ?? throw new IncorrectException("Email or password");

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
            throw new IncorrectException("Email or password");

        var token = new UserTokenDto()
        {
            Token = jwtService.GenerateToken(user),
            TokenExpiryMinutes = jwtService.GetTokenExpiryMinutes()
        };

        return token;
    }
}
