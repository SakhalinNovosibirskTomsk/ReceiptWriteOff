namespace ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

public interface IWriteOffFactUnitOfWork
{
    IBookInstanceRepository BookInstanceRepository { get; }
    IWriteOffFactRepository WriteOffFactRepository { get; }
    IWriteOffReasonRepository WriteOffReasonRepository { get; }
    IReceiptFactRepository ReceiptFactRepository { get; }
}