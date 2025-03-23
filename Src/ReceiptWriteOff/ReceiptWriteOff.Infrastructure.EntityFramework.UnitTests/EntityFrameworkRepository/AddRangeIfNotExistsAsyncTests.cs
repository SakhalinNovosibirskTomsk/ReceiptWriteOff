using Moq;
using ReceiptWriteOff.Domain.Entities.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository;

public class AddRangeIfNotExistsAsyncTests
{
    [Fact]
    public async Task AddRangeIfNotExistsAsync_EntitiesRangeIsEmpty_ShouldDoNothing()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create();

        // Act
        await model.Repository.AddRangeIfNotExistsAsync(model.EntitiesRange, CancellationToken.None);

        // Assert
        model.EntitySetMock.Verify(x => x.FindAsync(), Times.Never());
        model.EntitySetMock.Verify(
            x => x.AddAsync(It.IsAny<IEntity<PrimaryKeyStub>>(), CancellationToken.None),
            Times.Never());
    }
}