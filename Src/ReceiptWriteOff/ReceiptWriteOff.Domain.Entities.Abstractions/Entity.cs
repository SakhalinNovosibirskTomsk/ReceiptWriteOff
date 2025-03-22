namespace ReceiptWriteOff.Domain.Entities.Abstractions;

public abstract class Entity : IEntity<int>
{
    public int Id { get; set; }
}