using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Application.Tests.WriteOffReasonService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.WriteOffReasonService;

public class GetAsyncTests
{
    [Fact]
    public async Task GetAsync_ReturnsWriteOffReasonById()
    {
        // Arrange
        int id = 1;
        var model = WriteOffReasonServiceTestsModelFactory.Create();

        // Act
        var result = await model.Service.GetAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model.WriteOffReasonDto);
        model.RepositoryMock.Verify(
            repo => repo.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffReasonDto>(It.IsAny<WriteOffReason>()),
            Times.Once);
    }
}