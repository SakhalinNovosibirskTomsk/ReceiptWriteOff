using FluentAssertions;
using Moq;
using ReceiptWriteOff.Infrastructure.EntityFramework.Exceptions;
using ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository;

public class DeleteAsyncTest
{
    [Fact]
    public async Task DeleteAsync_WhenEntityNotFound_ShouldThrowException()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, true);

        // Act
        var func = async () => await model.Repository.DeleteAsync(1, CancellationToken.None);

        // Assert
        await func.Should().ThrowAsync<EntityNotFoundException>();
        model.EntitySetMock.Verify(es => es.FindAsync(
            It.IsAny<object?[]?>(), 
            CancellationToken.None), Times.Once);
        model.EntitySetMock.Verify(es => es.Remove(model.FoundEntity!), Times.Never);
    }
    
    [Fact]
    public async Task DeleteAsync_WhenEntityIsFound_ShouldNotThrowException()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, false);

        // Act
        var func = async () => await model.Repository.DeleteAsync(1, CancellationToken.None);

        // Assert
        await func.Should().NotThrowAsync<EntityNotFoundException>();
        model.EntitySetMock.Verify(es => es.FindAsync(
            It.IsAny<object?[]?>(), 
            CancellationToken.None), Times.Once);
        model.EntitySetMock.Verify(es => es.Remove(model.FoundEntity!), Times.Once);
    }
}