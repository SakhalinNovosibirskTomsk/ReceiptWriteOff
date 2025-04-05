using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public class WriteOffFactRepository : EntityFrameworkRepository<WriteOffFact, int>, IWriteOffFactRepository
{
    public WriteOffFactRepository(IDatabaseContext databaseContext, IQueryableExtensionsWrapper<WriteOffFact> queryableExtensionsWrapper) : base(databaseContext, queryableExtensionsWrapper)
    {
    }
    
    public bool ContainsFactForBookInstance(int bookInstanceId) => _entitySet.Any(wof => wof.BookInstance.Id == bookInstanceId);
}