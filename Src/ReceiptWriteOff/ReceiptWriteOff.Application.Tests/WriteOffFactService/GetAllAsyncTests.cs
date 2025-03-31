using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Application.Tests.WriteOffFactService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.WriteOffFactService;

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