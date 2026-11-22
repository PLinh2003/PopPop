using PopPop.Data.Entities.Enums;

namespace PopPop.Data.Entities;

public class ChatMessage : IBaseEntity<long>
{
    public long Id { get; set; }
    public int ConversationId { get; set; }
    public required string SenderUserId { get; set; }
    public ChatMessageSenderType SenderType { get; set; }
    public required string Content { get; set; }
    public DateTime SentAtUtc { get; set; }
    public DateTime? ReadAtUtc { get; set; }

    public virtual ChatConversation? Conversation { get; set; }
    public virtual User? Sender { get; set; }
}
