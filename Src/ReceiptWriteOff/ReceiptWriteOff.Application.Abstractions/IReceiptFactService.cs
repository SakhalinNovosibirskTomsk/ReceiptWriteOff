using ReceiptWriteOff.Application.Contracts.ReceiptFact;

namespace ReceiptWriteOff.Application.Abstractions;

public interface IReceiptFactService
{
    Task<IEnumerable<ReceiptFactShortDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ReceiptFactDto> GetAsync(int id, CancellationToken cancellationToken);
    Task<ReceiptFactDto> RegisterAsync(RegisterReceiptFactDto registerReceiptFactDto, CancellationToken cancellationToken);
    Task EditAsync(int id, RegisterReceiptFactDto registerReceiptFactDto, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}