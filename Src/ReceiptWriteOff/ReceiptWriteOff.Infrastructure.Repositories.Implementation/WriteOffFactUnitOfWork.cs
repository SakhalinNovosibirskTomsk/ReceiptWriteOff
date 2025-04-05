using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public class WriteOffFactUnitOfWork : IWriteOffFactUnitOfWork
{
    public WriteOffFactUnitOfWork(IDatabaseContext databaseContext,
        IQueryableExtensionsWrapper<BookInstance> bookInstanceQueryableExtensionsWrapper,
        IQueryableExtensionsWrapper<WriteOffFact> writeOffFactQueryableExtensionsWrapper,
        IQueryableExtensionsWrapper<WriteOffReason> writeOffReasonQueryableExtensionsWrapper,
        IQueryableExtensionsWrapper<ReceiptFact> receiptFactQueryableExtensionsWrapper)
    {
        BookInstanceRepository = new BookInstanceRepository(databaseContext, bookInstanceQueryableExtensionsWrapper);
        WriteOffFactRepository = new WriteOffFactRepository(databaseContext, writeOffFactQueryableExtensionsWrapper);
        WriteOffReasonRepository = new WriteOffReasonRepository(databaseContext, writeOffReasonQueryableExtensionsWrapper);
        ReceiptFactRepository = new ReceiptFactRepository(databaseContext, receiptFactQueryableExtensionsWrapper);
    }

    public IBookInstanceRepository BookInstanceRepository { get; }
    public IWriteOffFactRepository WriteOffFactRepository { get; }
    public IWriteOffReasonRepository WriteOffReasonRepository { get; }
    public IReceiptFactRepository ReceiptFactRepository { get; }
}