using AutoMapper;
using UserService.Application.Addresses.Commands.AddAddress;
using UserService.Domain.Entities;

namespace UserService.Application.Addresses.DTOs;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddAddressCommand, Address>();
    }
}
