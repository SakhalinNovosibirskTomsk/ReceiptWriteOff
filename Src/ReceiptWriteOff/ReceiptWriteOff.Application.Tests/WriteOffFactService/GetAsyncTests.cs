using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Application.Tests.WriteOffFactService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.WriteOffFactService;

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
        model.WriteOffFactUnitOfWorkMock.Verify(uow => uow.WriteOffFactRepository, Times.Once);
        model.WriteOffFactRepositoryMock.Verify(
            repo => repo.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffFactDto>(It.IsAny<WriteOffFact>()),
            Times.Once);
    }
}