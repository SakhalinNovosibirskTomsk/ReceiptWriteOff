using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Contracts.Book;
using ReceiptWriteOff.Contracts.BookInstance;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;
// ReSharper disable InconsistentNaming

namespace ReceiptWriteOff.Controllers;

[ApiController]
[Route("api/v1/books")]
public class BookController(IBookService _bookService, IMapper _mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BookResponse>>> GetAllAsync(
        CancellationToken cancellationToken, 
        bool isArchived = false)
    {
        var books = (await _bookService.GetAllAsync(isArchived, cancellationToken))
            .Select(_mapper.Map<BookResponse>).ToList();

        return Ok(books);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookResponse>> GetAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var book = await _bookService.GetAsync(id, cancellationToken);
            var bookResponse = _mapper.Map<BookResponse>(book);
            return Ok(bookResponse);
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Book with Id {id} found");
        }
    }
    
    [HttpGet("{id}/book-instances")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BookInstanceShortResponse>>> GetBookInstancesAsync(int id, CancellationToken cancellationToken)
    {
        var bookInstances = (await _bookService.GetBookInstancesAsync(id, cancellationToken))
            .Select(_mapper.Map<BookInstanceShortResponse>).ToList();

        return Ok(bookInstances);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<BookResponse>> CreateAsync(CreateOrEditBookRequest request, CancellationToken cancellationToken)
    {
        var bookDto = _mapper.Map<CreateOrEditBookDto>(request);
        var createdBookDto = await _bookService.CreateAsync(bookDto, cancellationToken);

        var actionName = nameof(GetAsync);
        return CreatedAtAction(actionName, new { id = createdBookDto.Id, cancellationToken }, createdBookDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditAsync(int id, [FromBody] CreateOrEditBookRequest request, CancellationToken cancellationToken)
    {
        var bookDto = _mapper.Map<CreateOrEditBookDto>(request);
        await _bookService.EditAsync(id, bookDto, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteToArchiveAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _bookService.DeleteToArchiveAsync(id, cancellationToken);
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Book with Id {id} found");
        }
    }

    [HttpPost("archived/{id:int}/restore")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RestoreFromArchiveAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _bookService.RestoreFromArchiveAsync(id, cancellationToken);
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound($"No Book with Id {id} found");
        }
    }
}