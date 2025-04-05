using AutoMapper;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Application.Implementations.Exceptions;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

// ReSharper disable InconsistentNaming

namespace ReceiptWriteOff.Application.Implementations;

public class ReceiptFactService(IReceiptFactRepository _receiptFactRepository, IMapper _mapper) : IReceiptFactService
{
    public async Task<IEnumerable<ReceiptFactShortDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var receiptFacts = await _receiptFactRepository.GetAllAsync(
            cancellationToken);
        return receiptFacts.Select(_mapper.Map<ReceiptFactShortDto>);
    }

    public async Task<ReceiptFactDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        var receiptFact = await _receiptFactRepository.GetAsync(id, cancellationToken);
        return _mapper.Map<ReceiptFactDto>(receiptFact);
    }

    public async Task<ReceiptFactDto> RegisterAsync(RegisterReceiptFactDto registerReceiptFactDto, CancellationToken cancellationToken)
    {
        if (_receiptFactRepository.ContainsWithBookInventoryNumber(registerReceiptFactDto.InventoryNumber))
        {
            throw new AlreadyExistsException($"Book instance with inventory number {registerReceiptFactDto.InventoryNumber} already exists.");
        }
        
        var receiptFact = _mapper.Map<ReceiptFact>(registerReceiptFactDto);
        var bookInstance = _mapper.Map<BookInstance>(registerReceiptFactDto);
        
        receiptFact.BookInstance = bookInstance;
        
        await _receiptFactRepository.AddAsync(receiptFact, cancellationToken);
        await _receiptFactRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ReceiptFactDto>(receiptFact);
    }

    public async Task EditAsync(int id, RegisterReceiptFactDto registerReceiptFactDto, CancellationToken cancellationToken)
    {
        var receiptFact = await _receiptFactRepository.GetAsync(id, cancellationToken);
        
        _mapper.Map(registerReceiptFactDto, receiptFact);
        _mapper.Map(registerReceiptFactDto, receiptFact.BookInstance);
        
        await _receiptFactRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _receiptFactRepository.DeleteAsync(id, cancellationToken);
        await _receiptFactRepository.SaveChangesAsync(cancellationToken);
    }
}