using FluentAssertions;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Contracts.Book;
using ReceiptWriteOff.Services.Tests.BookServiceTests.Model;

namespace ReceiptWriteOff.Services.Tests.BookServiceTests;

public class GetAllAsyncTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllBooks()
    {
        // Arrange
        int booksCount = 3;
        var model = BookServiceTestsModelFactory.Create(booksCount);

        // Act
        var result = await model.Service.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().HaveCount(booksCount);
        model.BookRepositoryMock.Verify(
            repo => repo.GetAllAsync(CancellationToken.None, false), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<BookDto>(It.IsAny<Book>()),
            Times.Exactly(booksCount));
    }
}