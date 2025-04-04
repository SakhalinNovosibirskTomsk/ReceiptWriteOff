using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
// ReSharper disable InconsistentNaming

namespace ReceiptWriteOff.Application.Implementations;

public class BookService(IBookRepository _bookRepository, IMapper _mapper) : IBookService
{
    public async Task<IEnumerable<BookDto>> GetAllAsync(bool isArchived, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync(isArchived, cancellationToken);
        return books.Select(_mapper.Map<BookDto>);
    }

    public async Task<BookDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetAsync(id, cancellationToken);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<IEnumerable<BookInstanceShortDto>> GetBookInstancesAsync(int bookId, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetAsync(bookId, cancellationToken);
        return book.BookInstances.Select(_mapper.Map<BookInstanceShortDto>);
    }

    public async Task<BookDto> CreateAsync(CreateOrEditBookDto createOrEditBookDto, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Book>(createOrEditBookDto);
        await _bookRepository.AddAsync(book, cancellationToken);
        await _bookRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<BookDto>(book);
    }

    public async Task EditAsync(int id, CreateOrEditBookDto createOrEditBookDto, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetAsync(id, cancellationToken);
        _mapper.Map(createOrEditBookDto, book);
        await _bookRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteToArchiveAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetAsync(id, cancellationToken);
        book.IsArchived = true;
        await _bookRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreFromArchiveAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetAsync(id, cancellationToken);
        book.IsArchived = false;
        await _bookRepository.SaveChangesAsync(cancellationToken);
    }
}