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

        // Act
        await model.Repository.GetAsync(1, CancellationToken.None);

        // Assert
        model.EntitySetMock.Verify(
            es => es.FindAsync(It.IsAny<object?[]?>(), CancellationToken.None),
            Times.Once());
    }
}