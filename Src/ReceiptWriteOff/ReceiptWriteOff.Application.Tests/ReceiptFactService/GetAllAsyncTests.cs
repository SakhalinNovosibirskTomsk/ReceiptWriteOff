using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Application.Tests.ReceiptFactService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.ReceiptFactService;

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
            repo => repo.GetAllAsync(CancellationToken.None, false), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<ReceiptFactShortDto>(It.IsAny<ReceiptFact>()),
            Times.Exactly(receiptFactsCount));
    }
}