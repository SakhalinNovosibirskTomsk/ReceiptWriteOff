using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using ReceiptWriteOff.Infrastructure.EntityFramework.Exceptions;
using ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository;

public class UpdateAsyncTests
{
    [Fact]
    public async Task UpdateAsync_WhenEntityNotFound_ShouldThrowException()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, true);

        // Act
        var func = async () => await model.Repository.UpdateAsync(1, CancellationToken.None);

        // Assert
        await func.Should().ThrowAsync<EntityNotFoundException>();
    }
    
    [Fact]
    public async Task UpdateAsync_WhenEntityIsFound_ShouldNotThrowException()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, false);

        // Act
        var func = async () => await model.Repository.UpdateAsync(1, CancellationToken.None);

        // Assert
        await func.Should().NotThrowAsync<EntityNotFoundException>();
        model.DatabaseContextMock.Verify(dc => dc.GetEntry(model.FoundEntity!), Times.Once);
        model.EntityEntryMock.VerifySet(ee => ee.State = EntityState.Modified, Times.Once);
    }
}