using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReceiptWriteOff.Models.BookInstance;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;

//ReSharper disable InconsistentNaming

namespace ReceiptWriteOff.Controllers;

[ApiController]
[Route("api/v1/bookInstances")]
public class BookInstanceController(IBookInstanceService _bookInstanceService, IMapper _mapper)
    : ControllerBase
{
    /// <summary>
    /// Получить данные всех экземпляров книг
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BookInstanceShortResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var bookInstances = (await _bookInstanceService.GetAllAsync(cancellationToken))
            .Select(_mapper.Map<BookInstanceShortResponse>).ToList();

        return Ok(bookInstances);
    }

    /// <summary>
    /// Получить данные экземпляра книги по id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookInstanceResponse>> GetAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var bookInstance = await _bookInstanceService.GetAsync(id, cancellationToken);
            var bookInstanceResponse = _mapper.Map<BookInstanceResponse>(bookInstance);
            return Ok(bookInstanceResponse);
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Book Instance with Id {id} found");
        }
    }
    
    /// <summary>
    /// Удалить экземпляр книги с id
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _bookInstanceService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Book Instance with Id {id} found");
        }
    }
}