using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Tests.WriteOffReasonService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.WriteOffReasonService;

public class CreateAsyncTests
{
    [Fact]
    public async Task CreateAsync_CreatesNewWriteOffReason()
    {
        // Arrange
        var model = WriteOffReasonServiceTestsModelFactory.Create();

        // Act
        var result = await model.Service.CreateAsync(model.CreateOrEditWriteOffReasonDto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model.WriteOffReasonDto);
        model.RepositoryMock.Verify(
            repo => repo.AddAsync(model.WriteOffReason, CancellationToken.None), 
            Times.Once);
        model.RepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffReason>(model.CreateOrEditWriteOffReasonDto),
            Times.Once);
    }
}