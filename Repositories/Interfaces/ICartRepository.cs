using PopPop.Data.Entities;

namespace PopPop.Repositories.Interfaces;

public interface ICartRepository : IRepository<Cart, int>
{
    Task<Cart?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
}
