using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Contracts.WriteOffFact;
using ReceiptWriteOff.Contracts.WriteOffReason;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;

// ReSharper disable InconsistentNaming

namespace ReceiptWriteOff.Controllers;

[ApiController]
[Route("api/v1/write-off-reasons")]
public class WriteOffReasonController(IWriteOffReasonService _writeOffReasonService, IMapper _mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<WriteOffReasonResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var writeOffReasons = (await _writeOffReasonService.GetAllAsync(cancellationToken))
            .Select(_mapper.Map<WriteOffReasonResponse>).ToList();

        return Ok(writeOffReasons);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WriteOffReasonResponse>> GetAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var writeOffReason = await _writeOffReasonService.GetAsync(id, cancellationToken);
            var writeOffReasonResponse = _mapper.Map<WriteOffReasonResponse>(writeOffReason);
            return Ok(writeOffReasonResponse);
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Write-Off Reason with Id {id} found");
        }
    }

    [HttpGet("{id}/write-off-facts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<WriteOffFactResponse>>> GetWriteOffFactsAsync(int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var writeOffFacts = (await _writeOffReasonService.GetWriteOffFactsAsync(id, cancellationToken))
                .Select(_mapper.Map<WriteOffFactResponse>).ToList();

            return Ok(writeOffFacts);
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Write-Off Reason with Id {id} found");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<WriteOffReasonResponse>> CreateAsync(
        [FromBody] CreateOrEditWriteOffReasonRequest request,
        CancellationToken cancellationToken)
    {
        var createOrEditWriteOffReasonDto = _mapper.Map<CreateOrEditWriteOffReasonDto>(request);
        var writeOffReason = await _writeOffReasonService.CreateAsync(createOrEditWriteOffReasonDto, cancellationToken);
        var writeOffReasonResponse = _mapper.Map<WriteOffReasonResponse>(writeOffReason);
        return CreatedAtAction(nameof(GetAsync), new { id = writeOffReasonResponse.Id }, writeOffReasonResponse);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditAsync(int id,
        [FromBody] CreateOrEditWriteOffReasonRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var createOrEditWriteOffReasonDto = _mapper.Map<CreateOrEditWriteOffReasonDto>(request);
            await _writeOffReasonService.EditAsync(id, createOrEditWriteOffReasonDto, cancellationToken);
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Write-Off Reason with Id {id} found");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _writeOffReasonService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Write-Off Reason with Id {id} found");
        }
    }
}