namespace PopPop.Services.Interfaces;

public interface IHeaderBadgeService
{
    Task<(int CartQuantity, int UnreadChatCount)> GetCountsAsync(
        string userId,
        CancellationToken cancellationToken = default);
}
