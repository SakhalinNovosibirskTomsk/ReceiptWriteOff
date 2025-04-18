using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Application.Tests.BookService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.BookService;

public class GetBookInstancesAsyncTests
{
    [Fact]
    public async Task GetBookInstancesAsync_ReturnsBookInstancesByBookId()
    {
        // Arrange
        int bookId = 1;
        var model = BookServiceTestsModelFactory.Create();
        var bookInstances = model.Book.BookInstances;

        // Act
        var result = await model.Service.GetBookInstancesAsync(bookId, CancellationToken.None);

        // Assert
        result.Should().HaveCount(bookInstances.Count);
        model.BookRepositoryMock.Verify(
            repo => repo.GetAsync(bookId, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<BookInstanceShortDto>(It.IsAny<BookInstance>()),
            Times.Exactly(bookInstances.Count));
    }
}