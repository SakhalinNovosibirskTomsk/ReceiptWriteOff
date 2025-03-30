using FluentAssertions;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Contracts.WriteOffFact;
using ReceiptWriteOff.Services.Tests.WriteOffFactService.Model;

namespace ReceiptWriteOff.Services.Tests.WriteOffFactService;

public class GetAllAsyncTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllWriteOffFacts()
    {
        // Arrange
        int writeOffFactsCount = 3;
        var model = WriteOffFactServiceTestsModelFactory.Create(writeOffFactsCount);

        // Act
        var result = await model.Service.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().HaveCount(writeOffFactsCount);
        model.RepositoryMock.Verify(
            repo => repo.GetAllAsync(CancellationToken.None, false),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffFactShortDto>(It.IsAny<WriteOffFact>()),
            Times.Exactly(writeOffFactsCount));
    }
}