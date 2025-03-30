using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public class WriteOffReasonRepository : EntityFrameworkRepository<WriteOffReason, int>, IWriteOffReasonRepository
{
    public WriteOffReasonRepository(IDatabaseContext databaseContext, IQueryableExtensionsWrapper<WriteOffReason> queryableExtensionsWrapper) : base(databaseContext, queryableExtensionsWrapper)
    {
    }
}