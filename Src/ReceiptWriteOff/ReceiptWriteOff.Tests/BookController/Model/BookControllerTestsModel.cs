using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Contracts.Book;

namespace ReceiptWriteOff.Tests.BookController.Model;

public class BookControllerTestsModel
{
    public required Controllers.BookController Controller { get; set; }
    public required Mock<IBookService> ServiceMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<BookDto> Books { get; set; }
    public required BookDto Book { get; set; }
    public required BookResponse BookResponse { get; set; }
    public required CreateOrEditBookRequest CreateOrEditBookRequest { get; set; }
}