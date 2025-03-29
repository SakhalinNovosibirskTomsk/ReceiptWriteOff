using ReceiptWriteOff.Services.Contracts.Book;
using ReceiptWriteOff.Services.Contracts.BookInstance;

namespace ReceiptWriteOff.Services.Abstractions;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<BookDto> GetAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<BookInstanceDto>> GetBookInstancesAsync(int bookId, CancellationToken cancellationToken);
    Task<BookDto> CreateAsync(CreateOrEditBookDto createOrEditBookDto, CancellationToken cancellationToken);
    Task EditAsync(int id, CreateOrEditBookDto createOrEditBookDto, CancellationToken cancellationToken);
    Task DeleteToArchiveAsync(int id, CancellationToken cancellationToken);
    Task RestoreFromArchiveAsync(int id, CancellationToken cancellationToken);
}