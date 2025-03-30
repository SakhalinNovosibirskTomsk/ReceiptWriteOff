using FluentAssertions;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Contracts.ReceiptFact;
using ReceiptWriteOff.Services.Tests.ReceiptFactService.Model;

namespace ReceiptWriteOff.Services.Tests.ReceiptFactService;

public class GetAllAsyncTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllReceiptFacts()
    {
        // Arrange
        int receiptFactsCount = 3;
        var model = ReceiptFactServiceTestsModelFactory.Create(receiptFactsCount);

        // Act
        var result = await model.Service.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().HaveCount(receiptFactsCount);
        model.RepositoryMock.Verify(
            repo => repo.GetAllAsync(CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<ReceiptFactShortDto>(It.IsAny<ReceiptFact>()),
            Times.Exactly(receiptFactsCount));
    }
}