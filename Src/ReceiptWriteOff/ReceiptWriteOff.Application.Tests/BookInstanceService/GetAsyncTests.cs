using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Application.Tests.BookInstanceService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.BookInstanceService;

public class GetAsyncTests
{
    [Fact]
    public async Task GetAsync_ReturnsBookInstanceById()
    {
        // Arrange
        int id = 1;
        var model = BookInstanceServiceTestsModelFactory.Create();

        // Act
        var result = await model.Service.GetAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model.BookInstanceDto);
        model.RepositoryMock.Verify(
            repo => repo.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<BookInstanceDto>(It.IsAny<BookInstance>()),
            Times.Once);
    }
}