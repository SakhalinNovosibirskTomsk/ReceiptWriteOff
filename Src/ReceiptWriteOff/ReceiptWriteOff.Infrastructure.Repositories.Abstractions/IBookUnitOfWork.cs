namespace ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

public interface IBookUnitOfWork
{
    IBookRepository BookRepository { get; }
    IBookInstanceRepository BookInstanceRepository { get; }
}