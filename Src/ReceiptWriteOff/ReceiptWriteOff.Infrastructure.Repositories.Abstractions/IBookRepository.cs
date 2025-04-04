using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

public interface IBookRepository : IRepository<Book, int>
{
    public Task<IEnumerable<Book>> GetAllAsync(
        bool isArchived,
        CancellationToken cancellationToken,
        bool asNoTracking = false);
}