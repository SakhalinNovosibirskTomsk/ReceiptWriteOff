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
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, true);

        // Act
        await model.Repository.AddRangeIfNotExistsAsync(model.EntitiesRange, CancellationToken.None);

        // Assert
        model.EntitySetMock.Verify(
            x => x.FindAsync(It.IsAny<object?[]?>(), CancellationToken.None),
            Times.Never());
        model.EntitySetMock.Verify(
            x => x.AddAsync(It.IsAny<IEntity<PrimaryKeyStub>>(), CancellationToken.None),
            Times.Never());
    }
    
    [Fact]
    public async Task AddRangeIfNotExistsAsync_EntitiesNotFound_ShouldAddEntities()
    {
        // Arrange
        int entitiesCount = 5;
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(entitiesCount, true);

        // Act
        await model.Repository.AddRangeIfNotExistsAsync(model.EntitiesRange, CancellationToken.None);

        // Assert
        model.EntitySetMock.Verify(
            x => x.FindAsync(It.IsAny<object?[]?>(), CancellationToken.None),
            Times.Exactly(entitiesCount));
        model.EntitySetMock.Verify(
            x => x.AddAsync(
                It.IsAny<IEntity<PrimaryKeyStub>>(), CancellationToken.None),
            Times.Exactly(entitiesCount));
    }
    
    [Fact]
    public async Task AddRangeIfNotExistsAsync_EntitiesFound_ShouldNotAddEntities()
    {
        // Arrange
        int entitiesCount = 5;
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(entitiesCount, false);

        // Act
        await model.Repository.AddRangeIfNotExistsAsync(model.EntitiesRange, CancellationToken.None);

        // Assert
        model.EntitySetMock.Verify(
            x => x.FindAsync(It.IsAny<object?[]?>(), CancellationToken.None), 
            Times.Exactly(entitiesCount));
        model.EntitySetMock.Verify(
            x => x.AddAsync(It.IsAny<IEntity<PrimaryKeyStub>>(), CancellationToken.None),
            Times.Never());
    }
}