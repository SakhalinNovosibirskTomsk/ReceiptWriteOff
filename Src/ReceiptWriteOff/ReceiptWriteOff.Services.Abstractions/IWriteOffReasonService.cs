using ReceiptWriteOff.Services.Contracts.WriteOffFact;
using ReceiptWriteOff.Services.Contracts.WriteOffReason;

namespace ReceiptWriteOff.Services.Abstractions;

public interface IWriteOffReasonService
{
    Task<IEnumerable<WriteOffReasonDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<WriteOffReasonDto> GetAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<WriteOffFactDto>> GetWriteOffFactsAsync(int writeOffReasonId, CancellationToken cancellationToken);
    Task<WriteOffReasonDto> CreateAsync(CreateOrEditWriteOffReasonDto createOrEditWriteOffReasonDto, CancellationToken cancellationToken);
    Task<bool> EditAsync(int id, CreateOrEditWriteOffReasonDto createOrEditWriteOffReasonDto, CancellationToken cancellationToken);
    Task<bool> DeleteToArchiveAsync(int id, CancellationToken cancellationToken);
}