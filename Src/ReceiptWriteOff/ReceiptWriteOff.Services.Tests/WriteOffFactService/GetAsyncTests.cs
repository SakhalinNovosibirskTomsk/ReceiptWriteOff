using FluentAssertions;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Contracts.WriteOffFact;
using ReceiptWriteOff.Services.Tests.WriteOffFactService.Model;

namespace ReceiptWriteOff.Services.Tests.WriteOffFactService;

public class GetAsyncTests
{
    [Fact]
    public async Task GetAsync_ReturnsWriteOffFactById()
    {
        // Arrange
        int id = 1;
        var model = WriteOffFactServiceTestsModelFactory.Create();

        // Act
        var result = await model.Service.GetAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model.WriteOffFactDto);
        model.RepositoryMock.Verify(
            repo => repo.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffFactDto>(It.IsAny<WriteOffFact>()),
            Times.Once);
    }
}