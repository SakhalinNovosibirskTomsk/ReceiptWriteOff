using FluentAssertions;
using Moq;
using ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository;

public class GetAllAsync
{
    [Fact]
    public async Task GetAllAsync_WhenAsNoTrackingIsFalse_ShouldReturnAllEntities()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(3, false);
        var asNoTracking = false;
        
        // Act
        var result = await model.Repository.GetAllAsync(CancellationToken.None, asNoTracking);
        
        // Assert
        model.EntitySetMock.Verify(es => es.AsNoTracking(), Times.Never());
        model.EntitySetMock.Verify(es => es.AsNoTracking(), Times.Never());
        result.Should().BeSameAs(model.EntitySetMock.Object.ToList());
    }
    
    [Fact]
    public async Task GetAllAsync_WhenAsNoTrackingIsTrue_ShouldReturnAllEntities()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(3, false);
        var asNoTracking = true;
        
        // Act
        var result = await model.Repository.GetAllAsync(CancellationToken.None, asNoTracking);
        
        // Assert
        model.EntitySetMock.Verify(es => es.AsNoTracking(), Times.Never());
        result.Should().BeSameAs(model.EntitySetMock.Object.ToList());
    }
}