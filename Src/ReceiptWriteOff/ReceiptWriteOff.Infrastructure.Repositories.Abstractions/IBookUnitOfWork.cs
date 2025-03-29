namespace ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

public interface IBookUnitOfWork
{
    IBookRepository BookRepository { get; }
    IBookRepository BookArchiveRepository { get; }
    IBookInstanceRepository BookInstanceRepository { get; }
}