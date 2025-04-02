using AutoMapper;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

// ReSharper disable InconsistentNaming

namespace ReceiptWriteOff.Application.Implementations;

public class BookService(IBookUnitOfWork _bookUnitOfWork, IMapper _mapper) : IBookService
{
    public async Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var books = await _bookUnitOfWork.BookRepository.GetAllAsync(cancellationToken);
        return books.Select(_mapper.Map<BookDto>);
    }

    public async Task<BookDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _bookUnitOfWork.BookRepository.GetAsync(id, cancellationToken);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<IEnumerable<BookDto>> GetAllArchivedAsync(CancellationToken cancellationToken)
    {
        var books = await _bookUnitOfWork.BookArchiveRepository.GetAllAsync(cancellationToken);
        return books.Select(_mapper.Map<BookDto>);
    }

    public async Task<BookDto> GetArchivedAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _bookUnitOfWork.BookArchiveRepository.GetAsync(id, cancellationToken);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<IEnumerable<BookInstanceShortDto>> GetBookInstancesAsync(int bookId, CancellationToken cancellationToken)
    {
        var book = await _bookUnitOfWork.BookRepository.GetAsync(bookId, cancellationToken);
        return book.BookInstances.Select(_mapper.Map<BookInstanceShortDto>);
    }

    public async Task<BookDto> CreateAsync(CreateOrEditBookDto createOrEditBookDto, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Book>(createOrEditBookDto);
        await _bookUnitOfWork.BookRepository.AddAsync(book, cancellationToken);
        await _bookUnitOfWork.BookRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<BookDto>(book);
    }

    public async Task EditAsync(int id, CreateOrEditBookDto createOrEditBookDto, CancellationToken cancellationToken)
    {
        var book = await _bookUnitOfWork.BookRepository.GetAsync(id, cancellationToken);
        _mapper.Map(createOrEditBookDto, book);
    }

    public async Task DeleteToArchiveAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _bookUnitOfWork.BookRepository.GetAsync(id, cancellationToken);
        await _bookUnitOfWork.BookRepository.DeleteAsync(id, cancellationToken);
        await _bookUnitOfWork.BookArchiveRepository.AddAsync(book, cancellationToken);
        book.IsArchived = true;
        await _bookUnitOfWork.BookArchiveRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task RestoreFromArchiveAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _bookUnitOfWork.BookArchiveRepository.GetAsync(id, cancellationToken);
        await _bookUnitOfWork.BookArchiveRepository.DeleteAsync(id, cancellationToken);
        await _bookUnitOfWork.BookRepository.AddAsync(book, cancellationToken);
        book.IsArchived = false;
        await _bookUnitOfWork.BookRepository.SaveChangesAsync(cancellationToken);
    }
}