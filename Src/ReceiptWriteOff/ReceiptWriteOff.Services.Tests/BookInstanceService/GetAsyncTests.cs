using FluentAssertions;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework.Exceptions;
using ReceiptWriteOff.Services.Contracts.BookInstance;
using ReceiptWriteOff.Services.Tests.BookInstanceService.Model;

namespace ReceiptWriteOff.Services.Tests.BookInstanceService;

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