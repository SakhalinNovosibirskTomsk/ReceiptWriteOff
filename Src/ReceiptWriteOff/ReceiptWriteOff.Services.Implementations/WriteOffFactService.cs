using AutoMapper;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Services.Abstractions;
using ReceiptWriteOff.Services.Contracts.WriteOffFact;
// ReSharper disable InconsistentNaming

namespace ReceiptWriteOff.Services.Implementations;

public class WriteOffFactService(IWriteOffFactRepository _writeOffFactRepository, IMapper _mapper) 
    : IWriteOffFactService
{
    public async Task<IEnumerable<WriteOffFactShortDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var writeOffFacts = await _writeOffFactRepository.GetAllAsync(
            cancellationToken);
        return writeOffFacts.Select(_mapper.Map<WriteOffFactShortDto>);
    }

    public async Task<WriteOffFactDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        var writeOffFact = await _writeOffFactRepository.GetAsync(id, cancellationToken);
        return _mapper.Map<WriteOffFactDto>(writeOffFact);
    }

    public async Task<WriteOffFactDto> RegisterAsync(
        RegisterWriteOffFactDto registerWriteOffFactDto, 
        CancellationToken cancellationToken)
    {
        var writeOffFact = _mapper.Map<WriteOffFact>(registerWriteOffFactDto);
        
        await _writeOffFactRepository.AddAsync(writeOffFact, cancellationToken);
        await _writeOffFactRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<WriteOffFactDto>(writeOffFact);
    }

    public async Task EditAsync(int id, RegisterWriteOffFactDto registerWriteOffFactDto, CancellationToken cancellationToken)
    {
        var writeOffFact = await _writeOffFactRepository.GetAsync(id, cancellationToken);
        
        _mapper.Map(registerWriteOffFactDto, writeOffFact);
        
        await _writeOffFactRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _writeOffFactRepository.DeleteAsync(id, cancellationToken);
        await _writeOffFactRepository.SaveChangesAsync(cancellationToken);
    }
}