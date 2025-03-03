using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Persistence;

namespace UserService.Infrastructure.Repositories;

internal class AddressRepository(UserServiceDbContext dbContext) : IAddressRepository
{
    public async Task<Address> AddAsync(Address address)
    {
        dbContext.Addresses.Add(address);
        await dbContext.SaveChangesAsync();
        return address;
    }

    public async Task<IEnumerable<Address>> GetByUserIdAsync(Guid userId)
    {
        return await dbContext.Addresses.Where(a => a.UserId == userId).ToListAsync();
    }
}
