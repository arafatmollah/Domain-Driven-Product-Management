using Authentication.Aggregator;

namespace Authentication.Repository;

public interface IUserRepository
{
    Task<UserAggregatorRoot?> GetByIdAsync(Guid id);

    Task<UserAggregatorRoot?> GetByEmailAsync(string email);

    Task<UserAggregatorRoot?> GetByRefreshTokenAsync(string refreshToken);

    Task<UserAggregatorRoot> AddAsync(UserAggregatorRoot user);

    Task UpdateAsync(UserAggregatorRoot user);
}
