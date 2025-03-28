using ReceiptWriteOff.Services.Contracts.BookInstance;

namespace ReceiptWriteOff.Services.Abstractions;

public interface IBookInstanceService
{
    Task<IEnumerable<BookInstanceShortDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<BookInstanceDto> GetAsync(int id, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}