using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface IAddressRepository
{
    Task<Address> AddAsync(Address address);
    Task<IEnumerable<Address>> GetByUserIdAsync(Guid userId);
}
