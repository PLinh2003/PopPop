using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PopPop.Data;
using PopPop.Data.Entities;
using PopPop.Data.Entities.Enums;

namespace PopPop.Hubs;

[Authorize]
public sealed class SupportChatHub(IDbContextFactory<PopPopDbContext> dbFactory) : Hub
{
    private const string ShopInboxGroup = "support-chat:shop-inbox";
    private const int MessageLimit = 2000;

    public override async Task OnConnectedAsync()
    {
        if (IsShopUser())
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, ShopInboxGroup);
        }

        await base.OnConnectedAsync();
    }

    public async Task<IReadOnlyList<ChatMessageDto>> GetCustomerHistory()
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        var userId = GetUserId();
        if (IsShopUser())
        {
            throw new HubException("Chức năng này chỉ dành cho khách hàng.");
        }

        var conversation = await db.ChatConversations
            .Include(c => c.Messages).ThenInclude(m => m.Sender)
            .SingleOrDefaultAsync(c => c.CustomerUserId == userId);

        if (conversation is null)
        {
            conversation = new ChatConversation
            {
                CustomerUserId = userId,
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            };
            db.ChatConversations.Add(conversation);
            await db.SaveChangesAsync();
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, ConversationGroup(conversation.Id));
        await MarkUnreadAsRead(db, conversation, ChatMessageSenderType.Shop);
        return conversation.Messages.OrderBy(m => m.SentAtUtc).Select(ToDto).ToList();
    }

    public async Task<IReadOnlyList<ConversationDto>> GetInbox()
    {
        EnsureShopUser();
        await using var db = await dbFactory.CreateDbContextAsync();
        return await db.ChatConversations
            .AsNoTracking()
            .Include(c => c.Customer)
            .Include(c => c.Messages)
            .OrderByDescending(c => c.UpdatedAtUtc)
            .Select(c => new ConversationDto(
                c.Id,
                c.Customer!.FullName ?? c.Customer.UserName ?? c.Customer.Email ?? "Khách hàng",
                c.Customer.Email,
                c.UpdatedAtUtc,
                c.Messages.OrderByDescending(m => m.SentAtUtc).Select(m => m.Content).FirstOrDefault() ?? "",
                c.Messages.Count(m => m.SenderType == ChatMessageSenderType.Customer && m.ReadAtUtc == null)))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ChatMessageDto>> GetConversationHistory(int conversationId)
    {
        EnsureShopUser();
        await using var db = await dbFactory.CreateDbContextAsync();
        var conversation = await db.ChatConversations
            .Include(c => c.Messages).ThenInclude(m => m.Sender)
            .SingleOrDefaultAsync(c => c.Id == conversationId)
            ?? throw new HubException("Không tìm thấy cuộc trò chuyện.");

        await Groups.AddToGroupAsync(Context.ConnectionId, ConversationGroup(conversation.Id));
        await MarkUnreadAsRead(db, conversation, ChatMessageSenderType.Customer);
        return conversation.Messages.OrderBy(m => m.SentAtUtc).Select(ToDto).ToList();
    }

    public async Task SendCustomerMessage(string content)
    {
        await using var db = await dbFactory.CreateDbContextAsync();
        if (IsShopUser())
        {
            throw new HubException("Không thể gửi tin nhắn với quyền cửa hàng.");
        }

        var text = NormalizeContent(content);
        var userId = GetUserId();
        var conversation = await db.ChatConversations.SingleOrDefaultAsync(c => c.CustomerUserId == userId);
        if (conversation is null)
        {
            conversation = new ChatConversation
            {
                CustomerUserId = userId,
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            };
            db.ChatConversations.Add(conversation);
        }

        await SaveAndBroadcast(db, conversation, userId, ChatMessageSenderType.Customer, text);
    }

    public async Task SendShopMessage(int conversationId, string content)
    {
        EnsureShopUser();
        await using var db = await dbFactory.CreateDbContextAsync();
        var conversation = await db.ChatConversations
            .SingleOrDefaultAsync(c => c.Id == conversationId)
            ?? throw new HubException("Không tìm thấy cuộc trò chuyện.");
        await SaveAndBroadcast(db, conversation, GetUserId(), ChatMessageSenderType.Shop, NormalizeContent(content));
    }

    private async Task SaveAndBroadcast(PopPopDbContext db, ChatConversation conversation, string senderId, ChatMessageSenderType senderType, string content)
    {
        var message = new ChatMessage
        {
            Conversation = conversation,
            SenderUserId = senderId,
            SenderType = senderType,
            Content = content,
            SentAtUtc = DateTime.UtcNow
        };
        conversation.UpdatedAtUtc = message.SentAtUtc;
        db.ChatMessages.Add(message);
        await db.SaveChangesAsync();

        var sender = await db.Users.AsNoTracking().SingleAsync(u => u.Id == senderId);
        var dto = new ChatMessageDto(message.Id, conversation.Id, senderType.ToString(),
            sender.FullName ?? sender.UserName ?? sender.Email ?? "", content, message.SentAtUtc);
        await Clients.Group(ConversationGroup(conversation.Id)).SendAsync("MessageReceived", dto);
        await Clients.Group(ShopInboxGroup).SendAsync("ConversationUpdated", conversation.Id);
    }

    private async Task MarkUnreadAsRead(PopPopDbContext db, ChatConversation conversation, ChatMessageSenderType senderType)
    {
        var now = DateTime.UtcNow;
        var unread = await db.ChatMessages
            .Where(m => m.ConversationId == conversation.Id && m.SenderType == senderType && m.ReadAtUtc == null)
            .ToListAsync();
        if (unread.Count == 0)
        {
            return;
        }

        foreach (var message in unread)
        {
            message.ReadAtUtc = now;
        }

        await db.SaveChangesAsync();
        await Clients.Group(ShopInboxGroup).SendAsync("ConversationUpdated", conversation.Id);
    }

    private static string NormalizeContent(string? content)
    {
        var normalized = content?.Trim();
        if (string.IsNullOrWhiteSpace(normalized) || normalized.Length > MessageLimit)
        {
            throw new HubException($"Tin nhắn phải có từ 1 đến {MessageLimit} ký tự.");
        }

        return normalized;
    }

    private string GetUserId() => Context.User?.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new HubException("Vui lòng đăng nhập lại.");

    private bool IsShopUser() => Context.User?.IsInRole("Admin") == true || Context.User?.IsInRole("Staff") == true;

    private void EnsureShopUser()
    {
        if (!IsShopUser())
        {
            throw new HubException("Bạn không có quyền truy cập hộp thư cửa hàng.");
        }
    }

    private static string ConversationGroup(int id) => $"support-chat:conversation:{id}";

    private static ChatMessageDto ToDto(ChatMessage message) => new(
        message.Id,
        message.ConversationId,
        message.SenderType.ToString(),
        message.Sender?.FullName ?? message.Sender?.UserName ?? message.Sender?.Email ?? "",
        message.Content,
        message.SentAtUtc);
}

public sealed record ChatMessageDto(long Id, int ConversationId, string SenderType, string SenderName, string Content, DateTime SentAtUtc);
public sealed record ConversationDto(int Id, string CustomerName, string? Email, DateTime UpdatedAtUtc, string LastMessage, int UnreadCount);
