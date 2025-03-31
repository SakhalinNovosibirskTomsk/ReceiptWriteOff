using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Application.Tests.WriteOffReasonService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.WriteOffReasonService;

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