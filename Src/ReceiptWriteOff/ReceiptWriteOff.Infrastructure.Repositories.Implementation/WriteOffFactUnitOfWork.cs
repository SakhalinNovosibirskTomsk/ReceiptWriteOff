using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public class WriteOffFactUnitOfWork : IWriteOffFactUnitOfWork
{
    public WriteOffFactUnitOfWork(IDatabaseContext databaseContext,
        IQueryableExtensionsWrapper<BookInstance> bookInstanceQueryableExtensionsWrapper,
        IQueryableExtensionsWrapper<WriteOffFact> writeOffFactQueryableExtensionsWrapper,
        IQueryableExtensionsWrapper<WriteOffReason> writeOffReasonQueryableExtensionsWrapper)
    {
        BookInstanceRepository = new BookInstanceRepository(databaseContext, bookInstanceQueryableExtensionsWrapper);
        WriteOffFactRepository = new WriteOffFactRepository(databaseContext, writeOffFactQueryableExtensionsWrapper);
        WriteOffReasonRepository = new WriteOffReasonRepository(databaseContext, writeOffReasonQueryableExtensionsWrapper);
    }

    public IBookInstanceRepository BookInstanceRepository { get; }
    public IWriteOffFactRepository WriteOffFactRepository { get; set; }
    public IWriteOffReasonRepository WriteOffReasonRepository { get; set; }
}