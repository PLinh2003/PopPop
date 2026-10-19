namespace PopPop.Data.Entities
{
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
