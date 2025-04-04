using ReceiptWriteOff.Domain.Entities;
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
    
    public async Task<IEnumerable<Book>> GetAllAsync(
        bool isArchived,
        CancellationToken cancellationToken,
        bool asNoTracking = false)
    {
        var books = GetAll(asNoTracking);
        var booksArchived = _queryableExtensionsWrapper.Where(books, b => b.IsArchived == isArchived);
        return await _queryableExtensionsWrapper.ToListAsync(booksArchived, cancellationToken);
    }
}