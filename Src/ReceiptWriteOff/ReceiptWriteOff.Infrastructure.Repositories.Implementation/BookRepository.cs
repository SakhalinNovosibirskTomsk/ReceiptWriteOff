using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public class BookRepository : EntityFrameworkRepository<Book, int>, IBookRepository
{
    public BookRepository(
        IDatabaseContext databaseContext,
        IQueryableExtensionsWrapper<Book> queryableExtensionsWrapper) 
        : base(databaseContext, queryableExtensionsWrapper)
    {
    }
}