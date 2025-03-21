namespace ReceiptWriteOff.Domain.Entities;

public interface IEntity<out TKey> where TKey: struct
{
    TKey Id { get; }
}