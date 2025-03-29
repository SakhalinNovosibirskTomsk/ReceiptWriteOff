using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Services.Contracts.Book;
using ReceiptWriteOff.Services.Contracts.BookInstance;
using ReceiptWriteOff.Services.Implementations;

namespace ReceiptWriteOff.Services.Tests.BookServiceTests.Model;

public static class BookServiceTestsModelFactory
{
    public static BookServiceTestsModel Create(int booksCount = 0)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var books = fixture.CreateMany<Book>(booksCount).ToList();
        var book = fixture.Freeze<Book>();

        var bookRepositoryMock = fixture.Freeze<Mock<IBookRepository>>();
        bookRepositoryMock.Setup(repo => repo.GetAllAsync(CancellationToken.None, false))
            .ReturnsAsync(books);
        bookRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(book);
        bookRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Book>(), CancellationToken.None))
            .Returns(Task.CompletedTask);
        bookRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>(), CancellationToken.None))
            .Returns(Task.CompletedTask);
        bookRepositoryMock.Setup(repo => repo.SaveChangesAsync(CancellationToken.None))
            .Returns(Task.CompletedTask);

        var bookArchiveRepositoryMock = fixture.Freeze<Mock<IBookRepository>>();
        bookArchiveRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Book>(), CancellationToken.None))
            .Returns(Task.CompletedTask);
        bookArchiveRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>(), CancellationToken.None))
            .Returns(Task.CompletedTask);
        bookArchiveRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(book);
        bookArchiveRepositoryMock.Setup(repo => repo.SaveChangesAsync(CancellationToken.None))
            .Returns(Task.CompletedTask);

        var bookDto = new BookDto
        {
            Title = "test_title",
            Author = "test_author"
        };
        var bookInstanceDto = new BookInstanceDto
        {
            Book = bookDto
        };
        var createOrEditBookDto = new CreateOrEditBookDto { Title = "New Title", Author = "New Author" };

        var mapperMock = fixture.Freeze<Mock<IMapper>>();
        mapperMock.Setup(m => m.Map<BookDto>(It.IsAny<Book>()))
            .Returns(bookDto);
        mapperMock.Setup(m => m.Map<BookInstanceDto>(It.IsAny<BookInstance>()))
            .Returns(bookInstanceDto);
        mapperMock.Setup(m => m.Map(createOrEditBookDto, book))
            .Returns(book);
        mapperMock.Setup(m => m.Map<Book>(createOrEditBookDto))
            .Returns(book);

        var bookUnitOfWorkMock = fixture.Freeze<Mock<IBookUnitOfWork>>();
        bookUnitOfWorkMock.Setup(uow => uow.BookRepository).Returns(bookRepositoryMock.Object);
        bookUnitOfWorkMock.Setup(uow => uow.BookArchiveRepository).Returns(bookArchiveRepositoryMock.Object);


        var service = new BookService(bookUnitOfWorkMock.Object, mapperMock.Object);

        return new BookServiceTestsModel
        {
            Service = service,
            BookRepositoryMock = bookRepositoryMock,
            BookArchiveRepositoryMock = bookArchiveRepositoryMock,
            MapperMock = mapperMock,
            Books = books,
            Book = book,
            BookDto = bookDto,
            BookInstanceDto = bookInstanceDto,
            CreateOrEditBookDto = createOrEditBookDto
        };
    }
}