using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Application.Implementations.Exceptions;
using ReceiptWriteOff.Contracts.ReceiptFact;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;
// ReSharper disable InconsistentNaming

namespace ReceiptWriteOff.Controllers;

[ApiController]
[Route("api/v1/receipt-facts")]
public class ReceiptFactController(IReceiptFactService _receiptFactService, IMapper _mapper)
    : ControllerBase
{
    /// <summary>
    /// Получить данные всех фактов получения
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ReceiptFactShortResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var receiptFacts = (await _receiptFactService.GetAllAsync(cancellationToken))
            .Select(_mapper.Map<ReceiptFactShortResponse>).ToList();

        return Ok(receiptFacts);
    }

    /// <summary>
    /// Получить данные факта получения по id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReceiptFactResponse>> GetAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var receiptFact = await _receiptFactService.GetAsync(id, cancellationToken);
            var receiptFactResponse = _mapper.Map<ReceiptFactResponse>(receiptFact);
            return Ok(receiptFactResponse);
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Receipt Fact with Id {id} found");
        }
    }

    /// <summary>
    /// Зарегистрировать новый факт получения
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ReceiptFactResponse>> RegisterAsync([FromBody] RegisterReceiptFactRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var registerReceiptFactDto = _mapper.Map<RegisterReceiptFactDto>(request);
            var receiptFact = await _receiptFactService.RegisterAsync(registerReceiptFactDto, cancellationToken);
            var receiptFactResponse = _mapper.Map<ReceiptFactResponse>(receiptFact);
            return CreatedAtAction(nameof(GetAsync), new { id = receiptFactResponse.Id }, receiptFactResponse);
        }
        catch (AlreadyExistsException e)
        {
            Console.WriteLine(e);
            return Conflict(e.Message);
        }
    }

    /// <summary>
    /// Редактировать факт получения по id
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditAsync(int id, [FromBody] RegisterReceiptFactRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var registerReceiptFactDto = _mapper.Map<RegisterReceiptFactDto>(request);
            await _receiptFactService.EditAsync(id, registerReceiptFactDto, cancellationToken);
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Receipt Fact with Id {id} found");
        }
    }

    /// <summary>
    /// Удалить факт получения по id
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _receiptFactService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Receipt Fact with Id {id} found");
        }
    }
}