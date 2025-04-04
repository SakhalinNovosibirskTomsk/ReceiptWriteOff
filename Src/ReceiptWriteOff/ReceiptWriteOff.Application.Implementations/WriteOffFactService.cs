using AutoMapper;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

// ReSharper disable InconsistentNaming

namespace ReceiptWriteOff.Application.Implementations;

public class WriteOffFactService(IWriteOffFactUnitOfWork _writeOffFactUnitOfWork, IMapper _mapper) 
    : IWriteOffFactService
{
    public async Task<IEnumerable<WriteOffFactShortDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var writeOffFacts = await _writeOffFactUnitOfWork.WriteOffFactRepository.GetAllAsync(
            cancellationToken);
        return writeOffFacts.Select(_mapper.Map<WriteOffFactShortDto>);
    }

    public async Task<WriteOffFactDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        var writeOffFact = await _writeOffFactUnitOfWork.WriteOffFactRepository.GetAsync(id, cancellationToken);
        return _mapper.Map<WriteOffFactDto>(writeOffFact);
    }

    public async Task<WriteOffFactDto> RegisterAsync(
        RegisterWriteOffFactDto registerWriteOffFactDto, 
        CancellationToken cancellationToken)
    {
        var bookInstance = await _writeOffFactUnitOfWork.BookInstanceRepository.GetAsync(
            registerWriteOffFactDto.BookInstanceId, 
            cancellationToken);
        
        var writeOffReason = await _writeOffFactUnitOfWork.WriteOffReasonRepository.GetAsync(
            registerWriteOffFactDto.WriteOffReasonId, 
            cancellationToken);
            
        var writeOffFact = _mapper.Map<WriteOffFact>(registerWriteOffFactDto);
        writeOffFact.BookInstance = bookInstance;
        writeOffFact.WriteOffReason = writeOffReason;

        var writeOffFactRepository = _writeOffFactUnitOfWork.WriteOffFactRepository;
        await writeOffFactRepository.AddAsync(writeOffFact, cancellationToken);
        await writeOffFactRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<WriteOffFactDto>(writeOffFact);
    }

    public async Task EditAsync(int id, RegisterWriteOffFactDto registerWriteOffFactDto, CancellationToken cancellationToken)
    {
        var writeOffFactRepository = _writeOffFactUnitOfWork.WriteOffFactRepository;
        var writeOffFact = await writeOffFactRepository.GetAsync(id, cancellationToken);
        _mapper.Map(registerWriteOffFactDto, writeOffFact);
        await writeOffFactRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var writeOffFactRepository = _writeOffFactUnitOfWork.WriteOffFactRepository;
        await writeOffFactRepository.DeleteAsync(id, cancellationToken);
        await writeOffFactRepository.SaveChangesAsync(cancellationToken);
    }
}