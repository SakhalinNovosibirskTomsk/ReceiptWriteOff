using FluentAssertions;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Tests.WriteOffFactService.Model;

namespace ReceiptWriteOff.Services.Tests.WriteOffFactService;

public class RegisterAsyncTests
{
    [Fact]
    public async Task RegisterAsync_CreatesNewWriteOffFact()
    {
        // Arrange
        var model = WriteOffFactServiceTestsModelFactory.Create();

        // Act
        var result = await model.Service.RegisterAsync(model.RegisterWriteOffFactDto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model.WriteOffFactDto);
        model.RepositoryMock.Verify(
            repo => repo.AddAsync(model.WriteOffFact, CancellationToken.None), 
            Times.Once);
        model.RepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffFact>(model.RegisterWriteOffFactDto),
            Times.Once);
    }
}