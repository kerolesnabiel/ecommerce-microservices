using AutoMapper;
using MediatR;
using UserService.Application.Users.DTOs;
using UserService.Domain.Exceptions;
using UserService.Domain.Interfaces;

namespace UserService.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(
    ILogger<GetUserQueryHandler> logger,
    IUserRepository userRepository,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("Getting user information with id: {UserId}", currentUser.Id);

        var user = await userRepository.GetByIdAsync(currentUser.Id)
            ?? throw new UserNotFoundException(currentUser.Id.ToString());

        return mapper.Map<UserDto>(user);
    }
}
