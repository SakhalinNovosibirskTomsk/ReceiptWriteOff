using FluentAssertions;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Contracts.ReceiptFact;
using ReceiptWriteOff.Services.Tests.ReceiptFactService.Model;

namespace ReceiptWriteOff.Services.Tests.ReceiptFactService;

public class GetAsyncTests
{
    [Fact]
    public async Task GetAsync_ReturnsReceiptFactById()
    {
        // Arrange
        int id = 1;
        var model = ReceiptFactServiceTestsModelFactory.Create();

        // Act
        var result = await model.Service.GetAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model.ReceiptFactDto);
        model.RepositoryMock.Verify(
            repo => repo.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<ReceiptFactDto>(It.IsAny<ReceiptFact>()),
            Times.Once);
    }
}