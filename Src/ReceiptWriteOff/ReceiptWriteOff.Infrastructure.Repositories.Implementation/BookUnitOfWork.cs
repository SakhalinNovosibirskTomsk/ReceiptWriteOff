using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public class BookUnitOfWork : IBookUnitOfWork
{
    public BookUnitOfWork(IDatabaseContext databaseContext,
        IQueryableExtensionsWrapper<Book> bookQueryableExtensionsWrapper,
        IQueryableExtensionsWrapper<BookInstance> bookInstanceQueryableExtensionsWrapper)
    {
        BookRepository = new BookRepository(databaseContext, bookQueryableExtensionsWrapper);
        BookInstanceRepository = new BookInstanceRepository(databaseContext, bookInstanceQueryableExtensionsWrapper);
    }

    public IBookRepository BookRepository { get; }
    public IBookInstanceRepository BookInstanceRepository { get; }
}