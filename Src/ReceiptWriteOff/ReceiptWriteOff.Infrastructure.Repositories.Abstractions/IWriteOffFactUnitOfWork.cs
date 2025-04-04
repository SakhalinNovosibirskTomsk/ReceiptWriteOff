namespace ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

public interface IWriteOffFactUnitOfWork
{
    IBookInstanceRepository BookInstanceRepository { get; }
    IWriteOffFactRepository WriteOffFactRepository { get; set; }
    IWriteOffReasonRepository WriteOffReasonRepository { get; set; }
}