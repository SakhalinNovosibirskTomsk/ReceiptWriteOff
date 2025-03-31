using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;

namespace ReceiptWriteOff.Application.Abstractions;

public interface IWriteOffReasonService
{
    Task<IEnumerable<WriteOffReasonDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<WriteOffReasonDto> GetAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<WriteOffFactDto>> GetWriteOffFactsAsync(int writeOffReasonId, CancellationToken cancellationToken);
    Task<WriteOffReasonDto> CreateAsync(CreateOrEditWriteOffReasonDto createOrEditWriteOffReasonDto, CancellationToken cancellationToken);
    Task EditAsync(int id, CreateOrEditWriteOffReasonDto createOrEditWriteOffReasonDto, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}