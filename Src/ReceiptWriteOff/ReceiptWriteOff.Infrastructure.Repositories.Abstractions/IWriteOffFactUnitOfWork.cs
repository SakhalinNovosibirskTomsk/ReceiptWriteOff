namespace ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

public interface IWriteOffFactUnitOfWork
{
    IBookInstanceRepository BookInstanceRepository { get; }
    IWriteOffFactRepository WriteOffFactRepository { get; }
    IWriteOffReasonRepository WriteOffReasonRepository { get; }
    public IReceiptFactRepository ReceiptFactRepository { get; }
}