using System.Linq.Expressions;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Tests.BookRepository.Model;

public static class BookRepositoryTestsModelFactory
{
    public static BookRepositoryTestsModel Create(int booksCount = 0)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var books = fixture.CreateMany<Book>(booksCount).ToList();
        
        var queryableMock = fixture.Freeze<Mock<IQueryable<Book>>>();

        var databaseContextMock = fixture.Freeze<Mock<IDatabaseContext>>();
        var queryableExtensionsWrapperMock = fixture.Freeze<Mock<IQueryableExtensionsWrapper<Book>>>();
        
        queryableExtensionsWrapperMock
            .Setup(wrapper => wrapper.Where(It.IsAny<IQueryable<Book>>(), It.IsAny<Expression<Func<Book, bool>>>()))
            .Returns(queryableMock.Object);

        queryableExtensionsWrapperMock
            .Setup(wrapper => wrapper.ToListAsync(It.IsAny<IQueryable<Book>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);

        var repository = new Implementation.BookRepository(databaseContextMock.Object, queryableExtensionsWrapperMock.Object);

        return new BookRepositoryTestsModel
        {
            Repository = repository,
            DatabaseContextMock = databaseContextMock,
            QueryableExtensionsWrapperMock = queryableExtensionsWrapperMock,
            Books = books,
            QueryableMock = queryableMock
        };
    }
}