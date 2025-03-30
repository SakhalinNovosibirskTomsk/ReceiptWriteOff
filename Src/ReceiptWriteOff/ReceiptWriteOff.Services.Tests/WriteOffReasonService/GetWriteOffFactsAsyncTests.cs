using FluentAssertions;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Contracts.WriteOffFact;
using ReceiptWriteOff.Services.Tests.WriteOffReasonService.Model;

namespace ReceiptWriteOff.Services.Tests.WriteOffReasonService;

public class GetWriteOffFactsAsyncTests
{
    [Fact]
    public async Task GetWriteOffFactsAsync_ReturnsWriteOffFactsByWriteOffReasonId()
    {
        // Arrange
        int writeOffReasonId = 1;
        var model = WriteOffReasonServiceTestsModelFactory.Create();
        var writeOffFacts = model.WriteOffReason.WriteOffFacts;

        // Act
        var result = await model.Service.GetWriteOffFactsAsync(writeOffReasonId, CancellationToken.None);

        // Assert
        result.Should().HaveCount(writeOffFacts.Count);
        model.RepositoryMock.Verify(
            repo => repo.GetAsync(writeOffReasonId, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffFactDto>(It.IsAny<WriteOffFact>()),
            Times.Exactly(writeOffFacts.Count));
    }
}