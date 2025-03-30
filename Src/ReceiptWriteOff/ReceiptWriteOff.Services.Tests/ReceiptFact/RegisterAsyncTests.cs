using FluentAssertions;
using Moq;
using ReceiptWriteOff.Services.Tests.ReceiptFactService.Model;

namespace ReceiptWriteOff.Services.Tests.ReceiptFactService;

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
        model.RepositoryMock.Verify(
            repo => repo.AddAsync(model.ReceiptFact, CancellationToken.None), 
            Times.Once);
        model.RepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<ReceiptFact>(model.RegisterReceiptFactDto),
            Times.Once);
    }
}