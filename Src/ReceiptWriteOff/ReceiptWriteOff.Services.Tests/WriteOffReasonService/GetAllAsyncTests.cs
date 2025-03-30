using FluentAssertions;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Contracts.WriteOffReason;
using ReceiptWriteOff.Services.Tests.WriteOffReasonService.Model;

namespace ReceiptWriteOff.Services.Tests.WriteOffReasonService;

public class GetAllAsyncTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllWriteOffReasons()
    {
        // Arrange
        int writeOffReasonsCount = 3;
        var model = WriteOffReasonServiceTestsModelFactory.Create(writeOffReasonsCount);

        // Act
        var result = await model.Service.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().HaveCount(writeOffReasonsCount);
        model.RepositoryMock.Verify(
            repo => repo.GetAllAsync(CancellationToken.None, false), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffReasonDto>(It.IsAny<WriteOffReason>()),
            Times.Exactly(writeOffReasonsCount));
    }
}