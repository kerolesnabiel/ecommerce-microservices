using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence;

class UserServiceDbContext : DbContext
{
    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options)
    {}

    public DbSet<User> Users { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}