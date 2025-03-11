using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface ISellerAccountRepository
{
    Task<SellerAccount> AddAsync(SellerAccount sellerAccount);
    Task<SellerAccount?> GetByUserIdAsync(Guid userId);
}
