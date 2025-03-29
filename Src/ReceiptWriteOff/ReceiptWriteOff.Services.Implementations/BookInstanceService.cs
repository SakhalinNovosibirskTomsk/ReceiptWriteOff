using AutoMapper;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Services.Abstractions;
using ReceiptWriteOff.Services.Contracts.BookInstance;
// ReSharper disable InconsistentNaming

namespace ReceiptWriteOff.Services.Implementations;

public class BookInstanceService(IBookInstanceRepository _bookInstanceRepository, IMapper _mapper) 
    : IBookInstanceService
{
    public async Task<IEnumerable<BookInstanceShortDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var bookInstances = await _bookInstanceRepository.GetAllAsync(cancellationToken);
        return bookInstances.Select(_mapper.Map<BookInstanceShortDto>);
    }

    public async Task<BookInstanceDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        var bookInstance = await _bookInstanceRepository.GetAsync(id, cancellationToken);
        return _mapper.Map<BookInstanceDto>(bookInstance);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _bookInstanceRepository.DeleteAsync(id, cancellationToken);
    }
}