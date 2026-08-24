using Authentication.Aggregator;
using Authentication.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repository;

public class UserRepository(AuthDbContext context) : IUserRepository
{
    private readonly AuthDbContext _context = context;

    public async Task<UserAggregatorRoot?> GetByIdAsync(Guid id)
        => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<UserAggregatorRoot?> GetByEmailAsync(string email)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant());

    public async Task<UserAggregatorRoot?> GetByRefreshTokenAsync(string refreshToken)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

    public async Task<UserAggregatorRoot> AddAsync(UserAggregatorRoot user)
    {
        await _context.Users.AddAsync(user);
        return user;
    }

    public Task UpdateAsync(UserAggregatorRoot user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }
}
