using FluentAssertions;
using Moq;
using ReceiptWriteOff.Infrastructure.EntityFramework.Exceptions;
using ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository;

public class GetAsyncTests
{
    [Fact]
    public async Task GetAsync_WhenEntityFound_ShouldCallFindAsyncOnce()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, false);

        // Act
        await model.Repository.GetAsync(1, CancellationToken.None);

        // Assert
        model.EntitySetMock.Verify(
            es => es.FindAsync(It.IsAny<object?[]?>(), CancellationToken.None),
            Times.Once());
    }
    
    [Fact]
    public async Task GetAsync_WhenEntityNotFound_ShouldThrowException()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, true);

        // Act
        var func = async () => await model.Repository.GetAsync(1, CancellationToken.None);

        // Assert
        await func.Should().ThrowAsync<EntityNotFoundException>();
    }
    
    [Fact]
    public async Task GetAsync_WhenEntityIsFound_ShouldNotThrowException()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, false);

        // Act
        var func = async () => await model.Repository.GetAsync(1, CancellationToken.None);

        // Assert
        await func.Should().NotThrowAsync<EntityNotFoundException>();
    }
}