using Moq;
using ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository;

public class GetAsyncTests
{
    [Fact]
    public async Task GetAsync_ShouldCallFindAsyncOnce()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, false);
        int primaryKey = new();

        // Act
        await model.Repository.GetAsync(primaryKey, CancellationToken.None);

        // Assert
        model.EntitySetMock.Verify(
            es => es.FindAsync(It.IsAny<object?[]?>(), CancellationToken.None),
            Times.Once());
    }
}