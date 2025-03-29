using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public class BookInstanceRepository : EntityFrameworkRepository<BookInstance, int>, IBookInstanceRepository
{
    public BookInstanceRepository(IDatabaseContext databaseContext, IQueryableExtensionsWrapper<BookInstance> queryableExtensionsWrapper)
        : base(databaseContext, queryableExtensionsWrapper)
    {
    }
}