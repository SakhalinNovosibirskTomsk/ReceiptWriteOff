using ReceiptWriteOff.Services.Contracts.WriteOffFact;

namespace ReceiptWriteOff.Services.Abstractions;

public interface IWriteOffFactService
{
    Task<IEnumerable<WriteOffFactShortDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<WriteOffFactDto> GetAsync(int id, CancellationToken cancellationToken);
    Task<WriteOffFactDto> RegisterAsync(RegisterWriteOffFactDto registerWriteOffFactDto, CancellationToken cancellationToken);
    Task EditAsync(int id, RegisterWriteOffFactDto registerWriteOffFactDto, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}