using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Tests.BookRepository.Model;

namespace ReceiptWriteOff.Infrastructure.Repositories.Tests.BookRepository;

public class BookRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsBooks()
    {
        // Arrange
        int booksCount = 3;
        var model = BookRepositoryTestsModelFactory.Create(booksCount);
        var isArchived = true;
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await model.Repository.GetAllAsync(isArchived, cancellationToken);

        // Assert
        result.Should().BeEquivalentTo(model.Books);
        model.QueryableExtensionsWrapperMock.Verify(
            wrapper => wrapper.Where(It.IsAny<IQueryable<Book>>(), It.IsAny<Expression<Func<Book, bool>>>()),
            Times.Once);
        model.QueryableExtensionsWrapperMock.Verify(
            wrapper => wrapper.ToListAsync(It.IsAny<IQueryable<Book>>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}