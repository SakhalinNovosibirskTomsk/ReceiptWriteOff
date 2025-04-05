using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public class WriteOffReasonRepository : EntityFrameworkRepository<WriteOffReason, int>, IWriteOffReasonRepository
{
    public WriteOffReasonRepository(IDatabaseContext databaseContext, IQueryableExtensionsWrapper<WriteOffReason> queryableExtensionsWrapper) : base(databaseContext, queryableExtensionsWrapper)
    {
    }

    public bool ContainsWithDescription(string description) => _entitySet.Any(e => e.Description == description);
}