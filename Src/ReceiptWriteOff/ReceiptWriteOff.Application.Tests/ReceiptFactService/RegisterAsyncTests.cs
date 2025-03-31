using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Tests.ReceiptFactService.Model;

namespace ReceiptWriteOff.Application.Tests.ReceiptFactService;

public class RegisterAsyncTests
{
    [Fact]
    public async Task RegisterAsync_CreatesNewReceiptFact()
    {
        // Arrange
        var model = ReceiptFactServiceTestsModelFactory.Create();

        // Act
        var result = await model.Service.RegisterAsync(model.RegisterReceiptFactDto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model.ReceiptFactDto);
        model.RepositoryMock.Verify<Task>(
            repo => repo.AddAsync(model.ReceiptFact, CancellationToken.None), 
            Times.Once);
        model.RepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<Domain.Entities.ReceiptFact>(model.RegisterReceiptFactDto),
            Times.Once);
    }
}