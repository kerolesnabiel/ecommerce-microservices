using AutoMapper;
using MediatR;
using UserService.Application.Users;
using UserService.Domain.Exceptions;
using UserService.Domain.Interfaces;

namespace UserService.Application.SellerAccounts.Commands.UpdateSellerAccount;

public class UpdateSellerAccountCommandHandler(
        ILogger<UpdateSellerAccountCommandHandler> logger,
        ISellerAccountRepository sellerAccountRepository,
        IUserRepository userRepository,
        IUserContext userContext,
        IMapper mapper
    ) : IRequestHandler<UpdateSellerAccountCommand>
{

    public async Task Handle(UpdateSellerAccountCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("Updating seller account for user: {UserId}", currentUser.Id);

        var sellerAccount = await sellerAccountRepository.GetByUserIdAsync(currentUser.Id)
            ?? throw new SellerNotFoundException(currentUser.Id.ToString());

        mapper.Map(request, sellerAccount);

        await sellerAccountRepository.UpdateAsync(sellerAccount);
    }
}
