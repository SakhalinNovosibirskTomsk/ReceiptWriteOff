using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Application.Tests.BookInstanceService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.BookInstanceService;

public class GetAllAsyncTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllBookInstances()
    {
        // Arrange
        int bookInstancesCount = 3;
        var model = BookInstanceServiceTestsModelFactory.Create(bookInstancesCount);

        // Act
        var result = await model.Service.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().HaveCount(bookInstancesCount);
        model.RepositoryMock.Verify(
            repo => repo.GetAllAsync(CancellationToken.None, false), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<BookInstanceShortDto>(It.IsAny<BookInstance>()),
            Times.Exactly(bookInstancesCount));
    }
}