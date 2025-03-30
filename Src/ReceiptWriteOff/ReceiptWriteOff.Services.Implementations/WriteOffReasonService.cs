using AutoMapper;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Services.Abstractions;
using ReceiptWriteOff.Services.Contracts.WriteOffFact;
using ReceiptWriteOff.Services.Contracts.WriteOffReason;
//Resharper disable InconsistentNaming

namespace ReceiptWriteOff.Services.Implementations;

public class WriteOffReasonService(IWriteOffReasonRepository _writeOffReasonRepository, IMapper _mapper) 
    : IWriteOffReasonService
{
    public async Task<IEnumerable<WriteOffReasonDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var writeOffReasons = await _writeOffReasonRepository
            .GetAllAsync(cancellationToken);
        return writeOffReasons.Select(_mapper.Map<WriteOffReasonDto>);
    }

    public async Task<WriteOffReasonDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        var writeOffReason = await _writeOffReasonRepository
            .GetAsync(id, cancellationToken);
        return _mapper.Map<WriteOffReasonDto>(writeOffReason);
    }

    public async Task<IEnumerable<WriteOffFactDto>> GetWriteOffFactsAsync(
        int writeOffReasonId,
        CancellationToken cancellationToken)
    {
        var writeOffReason = await _writeOffReasonRepository
            .GetAsync(writeOffReasonId, cancellationToken);
        return writeOffReason.WriteOffFacts.Select(_mapper.Map<WriteOffFactDto>);
    }

    public async Task<WriteOffReasonDto> CreateAsync(CreateOrEditWriteOffReasonDto createOrEditWriteOffReasonDto, CancellationToken cancellationToken)
    {
        var writeOffReason = _mapper.Map<WriteOffReason>(createOrEditWriteOffReasonDto);
        await _writeOffReasonRepository.AddAsync(writeOffReason, cancellationToken);
        await _writeOffReasonRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<WriteOffReasonDto>(writeOffReason);
    }

    public async Task EditAsync(int id, CreateOrEditWriteOffReasonDto createOrEditWriteOffReasonDto,
        CancellationToken cancellationToken)
    {
        var writeOffReason = await _writeOffReasonRepository.GetAsync(id, cancellationToken);
        _mapper.Map(createOrEditWriteOffReasonDto, writeOffReason);
        await _writeOffReasonRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _writeOffReasonRepository.DeleteAsync(id, cancellationToken);
        await _writeOffReasonRepository.SaveChangesAsync(cancellationToken);
    }
}