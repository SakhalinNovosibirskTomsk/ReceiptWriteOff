using Moq;
using ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository;

public class AddAsyncTests
{
    [Fact]
    public async Task AddAsync_ShouldCallAddAsyncOnce()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, false);

        // Act
        await model.Repository.AddAsync(model.FoundEntity!, CancellationToken.None);

        // Assert
        model.EntitySetMock.Verify(
            es => es.AddAsync(model.FoundEntity!, CancellationToken.None),
            Times.Once());
    }
}