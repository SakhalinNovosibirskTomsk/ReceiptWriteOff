using AutoMapper;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Services.Contracts.Book;
using ReceiptWriteOff.Services.Contracts.BookInstance;
using ReceiptWriteOff.Services.Implementations;

namespace ReceiptWriteOff.Services.Tests.BookServiceTests.Model;

public class BookServiceTestsModel
{
    public required BookService Service { get; set; }
    public required Mock<IBookRepository> BookRepositoryMock { get; set; }
    public required Mock<IBookRepository> BookArchiveRepositoryMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<Book> Books { get; set; }
    public required Book Book { get; set; }
    public required BookDto BookDto { get; set; }
    public required BookInstanceDto BookInstanceDto { get; set; }
    public required CreateOrEditBookDto CreateOrEditBookDto { get; set; }
}