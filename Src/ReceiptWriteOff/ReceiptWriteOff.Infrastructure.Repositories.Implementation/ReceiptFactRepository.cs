using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public class ReceiptFactRepository : EntityFrameworkRepository<ReceiptFact, int>, IReceiptFactRepository
{
    public ReceiptFactRepository(IDatabaseContext databaseContext, IQueryableExtensionsWrapper<ReceiptFact> queryableExtensionsWrapper) : base(databaseContext, queryableExtensionsWrapper)
    {
    }

    public ReceiptFact GetByBookInstance(int bookInstanceId)
    {
        var receiptFact = _entitySet.FirstOrDefault(rf => rf.BookInstanceId == bookInstanceId);
        if (receiptFact == null)
        {
            throw new EntityNotFoundException($"ReceiptFact with BookInstanceId {bookInstanceId} not found.");
        }
        
        return receiptFact;
    }

    public bool ContainsWithBookInventoryNumber(int inventoryNumber) => _entitySet.Any(rf => rf.BookInstance.InventoryNumber == inventoryNumber);
}