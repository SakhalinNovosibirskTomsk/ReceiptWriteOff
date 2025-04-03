using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Contracts.Book;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;

namespace ReceiptWriteOff.Tests.BookController.Model;

public static class BookControllerTestsModelFactory
{
    public static BookControllerTestsModel Create(
        int booksCount = 0,
        bool bookExists = true)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var books = fixture.CreateMany<BookDto>(booksCount).ToList();
        var book = fixture.Freeze<BookDto>();
        var createOrEditBookRequest = fixture.Freeze<CreateOrEditBookRequest>();
        var bookInstanceShortDtos = fixture.CreateMany<BookInstanceShortDto>(booksCount).ToList();

        var serviceMock = fixture.Freeze<Mock<IBookService>>();
        serviceMock.Setup(service => service.GetAllAsync(It.IsAny<bool>(),It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);
        serviceMock.Setup(service =>
                service.CreateAsync(It.IsAny<CreateOrEditBookDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);
        serviceMock.Setup(service => service.GetBookInstancesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookInstanceShortDtos);

        var entityNotFoundException = fixture.Freeze<EntityNotFoundException>();

        if (bookExists)
        {
            serviceMock.Setup(service => service.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);
        }
        else
        {
            serviceMock.Setup(service => service.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
            serviceMock.Setup(service => service.DeleteToArchiveAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
            serviceMock.Setup(service => service.RestoreFromArchiveAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
            serviceMock.Setup(service => service.EditAsync(It.IsAny<int>(), It.IsAny<CreateOrEditBookDto>(),It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
        }

        var bookResponse = fixture.Freeze<BookResponse>();

        var mapperMock = fixture.Freeze<Mock<IMapper>>();
        mapperMock.Setup(m => m.Map<BookResponse>(It.IsAny<BookDto>()))
            .Returns(bookResponse);

        var controller = fixture.Build<Controllers.BookController>()
            .OmitAutoProperties()
            .Create();

        return new BookControllerTestsModel
        {
            Controller = controller,
            ServiceMock = serviceMock,
            MapperMock = mapperMock,
            Books = books,
            Book = book,
            BookResponse = bookResponse,
            CreateOrEditBookRequest = createOrEditBookRequest
        };
    }
}