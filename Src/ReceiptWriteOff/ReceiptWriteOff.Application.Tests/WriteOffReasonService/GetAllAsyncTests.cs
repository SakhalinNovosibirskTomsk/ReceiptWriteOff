using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Application.Tests.WriteOffReasonService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.WriteOffReasonService;

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