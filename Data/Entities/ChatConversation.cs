namespace PopPop.Data.Entities;

public class ChatConversation : IBaseEntity<int>
{
    public int Id { get; set; }
    public required string CustomerUserId { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }

    public virtual User? Customer { get; set; }
    public virtual ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
}
