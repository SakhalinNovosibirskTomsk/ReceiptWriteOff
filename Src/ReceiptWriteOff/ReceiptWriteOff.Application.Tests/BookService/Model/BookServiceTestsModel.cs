using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Application.Implementations;

namespace ReceiptWriteOff.Application.Tests.BookService.Model;

public class BookServiceTestsModel
{
    public required Implementations.BookService Service { get; set; }
    public required Mock<IBookRepository> BookRepositoryMock { get; set; }
    public required Mock<IBookRepository> BookArchiveRepositoryMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<Book> Books { get; set; }
    public required Book Book { get; set; }
    public required BookDto BookDto { get; set; }
    public required BookInstanceDto BookInstanceDto { get; set; }
    public required CreateOrEditBookDto CreateOrEditBookDto { get; set; }
    public required Mock<IQueryable<Book>> BookQueryableMock { get; set; }
}