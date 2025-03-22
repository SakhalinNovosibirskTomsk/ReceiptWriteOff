namespace ReceiptWriteOff.Domain.Entities.Abstractions;

public interface IEntity<out TKey> where TKey: struct
{
    TKey Id { get; }
}