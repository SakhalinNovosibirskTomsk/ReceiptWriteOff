using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public class ReceiptFactRepository : EntityFrameworkRepository<ReceiptFact, int>, IReceiptFactRepository
{
    public ReceiptFactRepository(IDatabaseContext databaseContext, IQueryableExtensionsWrapper<ReceiptFact> queryableExtensionsWrapper) : base(databaseContext, queryableExtensionsWrapper)
    {
    }
}