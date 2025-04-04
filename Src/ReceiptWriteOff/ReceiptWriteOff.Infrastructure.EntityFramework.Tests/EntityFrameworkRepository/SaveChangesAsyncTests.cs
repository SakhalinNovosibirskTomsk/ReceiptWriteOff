using Moq;
using ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository;

public class SaveChangesAsyncTests
{
    [Fact]
    public async Task SaveChangesAsync_Always_ShouldCallDatabaseSaveChanges()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, false);

        // Act
        await model.Repository.SaveChangesAsync(CancellationToken.None);

        // Assert
        model.DatabaseContextMock.Verify(dc => dc.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}