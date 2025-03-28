using ReceiptWriteOff.Services.Contracts.Book;

namespace ReceiptWriteOff.Services.Abstractions;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<BookDto> GetAsync(int id, CancellationToken cancellationToken);
    Task<BookDto> CreateAsync(CreateOrEditBookDto createOrEditBookDto, CancellationToken cancellationToken);
    Task<bool> EditAsync(int id, CreateOrEditBookDto createOrEditBookDto, CancellationToken cancellationToken);
    Task<bool> DeleteToArchiveAsync(int id, CancellationToken cancellationToken);
    Task<bool> RestoreFromArchiveAsync(int id, CancellationToken cancellationToken);
}