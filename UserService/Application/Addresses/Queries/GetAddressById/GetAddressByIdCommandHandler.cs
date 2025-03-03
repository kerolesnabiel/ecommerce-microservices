﻿using AutoMapper;
using MediatR;
using UserService.Application.Addresses.DTOs;
using UserService.Application.Users;
using UserService.Domain.Exceptions;
using UserService.Domain.Interfaces;

namespace UserService.Application.Addresses.Queries.GetAddressById;

public class GetAddressByIdCommandHandler(
        ILogger<GetAddressByIdCommandHandler> logger,
    IAddressRepository addressRepository,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetAddressByIdCommand, AddressDto>
{
    public async Task<AddressDto> Handle(GetAddressByIdCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("Getting addresses for user: {UserId}", currentUser.Id);

        var address = await addressRepository.GetByIdAsync(request.Id)
            ?? throw new AddressNotFoundException(request.Id.ToString());

        if(address.UserId != currentUser.Id)
            throw new ForbiddenException();

        return mapper.Map<AddressDto>(address);
    }
}
