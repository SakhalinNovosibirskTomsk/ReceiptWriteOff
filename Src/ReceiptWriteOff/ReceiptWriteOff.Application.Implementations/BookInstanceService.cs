using AutoMapper;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

// ReSharper disable InconsistentNaming

namespace ReceiptWriteOff.Application.Implementations;

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
        await _bookInstanceRepository.SaveChangesAsync(cancellationToken);
    }
}