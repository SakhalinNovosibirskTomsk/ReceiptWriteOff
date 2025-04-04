using FluentAssertions;
using Moq;
using ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository;

public class GetAllAsyncTests
{
    [Fact]
    public async Task GetAllAsync_WhenAsNoTrackingIsFalse_ShouldCallMethodsCorrectly()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(3, false);
        var asNoTracking = false;
        
        // Act
        var result = await model.Repository.GetAllAsync(CancellationToken.None, asNoTracking);
        
        // Assert
        model.EntitySetMock.Verify(es => es.AsNoTracking(), Times.Never());
        model.QueryableExtensionsMock.Verify(
            qe => qe.ToListAsync(model.EntitySetMock.Object, CancellationToken.None),
            Times.Once());
        result.Should().BeSameAs(model.EntitiesRange);
    }
    
    [Fact]
    public async Task GetAllAsync_WhenAsNoTrackingIsTrue_ShouldCallMethodsCorrectly()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(3, false);
        var asNoTracking = true;
        
        // Act
        var result = await model.Repository.GetAllAsync(CancellationToken.None, asNoTracking);
        
        // Assert
        model.EntitySetMock.Verify(es => es.AsNoTracking(), Times.Once());
        model.QueryableExtensionsMock.Verify(
            qe => qe.ToListAsync(model.QueryableMock.Object, CancellationToken.None),
            Times.Once());
        result.Should().BeSameAs(model.EntitiesRange);
    }
}