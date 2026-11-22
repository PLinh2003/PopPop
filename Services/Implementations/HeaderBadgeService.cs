using Microsoft.EntityFrameworkCore;
using PopPop.Data;
using PopPop.Data.Entities.Enums;
using PopPop.Services.Interfaces;

namespace PopPop.Services.Implementations;

public sealed class HeaderBadgeService(IDbContextFactory<PopPopDbContext> dbFactory) : IHeaderBadgeService
{
    public async Task<(int CartQuantity, int UnreadChatCount)> GetCountsAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        await using var db = await dbFactory.CreateDbContextAsync(cancellationToken);

        var cartQuantity = await db.CartItems
            .Where(item => item.Cart!.UserId == userId)
            .Select(item => (int?)item.Quantity)
            .SumAsync(cancellationToken) ?? 0;

        var unreadChatCount = await db.ChatMessages
            .CountAsync(message => message.Conversation!.CustomerUserId == userId
                && message.SenderType == ChatMessageSenderType.Shop
                && message.ReadAtUtc == null, cancellationToken);

        return (cartQuantity, unreadChatCount);
    }
}
