using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository;

public class GetAllTests
{
    [Fact]
    public void GetAll_AsNoTrackingIsTrue_ShouldReturnEntitySetAsNoTracking()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, false);
        bool asNoTracking = true;

        // Act
        var result = model.Repository.GetAll(asNoTracking);

        // Assert
        model.EntitySetMock.Verify(es => es.AsNoTracking(), Times.Once());
        result.Should().BeSameAs(model.AsNoTrackingQueryableMock.Object);
    }
    
    [Fact]
    public void GetAll_AsNoTrackingIsFalse_ShouldReturnEntitySet()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, false);
        bool asNoTracking = false;

        // Act
        var result = model.Repository.GetAll(asNoTracking);

        // Assert
        model.EntitySetMock.Verify(es => es.AsNoTracking(), Times.Never());
        result.Should().BeSameAs(model.EntitySetMock.Object);
    }
}