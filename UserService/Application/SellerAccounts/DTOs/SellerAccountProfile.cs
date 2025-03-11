using AutoMapper;
using UserService.Application.SellerAccounts.Commands.AddSellerAccount;
using UserService.Domain.Entities;

namespace UserService.Application.SellerAccounts.DTOs;

public class SellerAccountProfile : Profile
{
    public SellerAccountProfile()
    {
        CreateMap<AddSellerAccountCommand, SellerAccount>()
            .ForMember(s => s.DateOfBirth, options => 
                options.MapFrom(c => c.DateOfBirth.ToUniversalTime()));
    }
}
