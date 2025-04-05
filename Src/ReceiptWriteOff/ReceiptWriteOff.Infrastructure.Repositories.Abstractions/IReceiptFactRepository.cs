using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

public interface IReceiptFactRepository : IRepository<ReceiptFact, int>
{
    ReceiptFact GetByBookInstance(int bookInstanceId);
    bool ContainsWithBookInventoryNumber(int inventoryNumber);
}