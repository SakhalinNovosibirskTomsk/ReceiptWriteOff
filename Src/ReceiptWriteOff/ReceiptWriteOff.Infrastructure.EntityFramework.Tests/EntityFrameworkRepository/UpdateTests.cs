using Microsoft.EntityFrameworkCore;
using Moq;
using ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository;

public class UpdateTests
{
    [Fact]
    public void Update_Always_ShouldCallDatabaseEntryWithEntity()
    {
        // Arrange
        var model = EntityFrameworkRepositoryTestsModelFactory.Create(0, false);

        // Act
        model.Repository.Update(model.FoundEntity!);

        // Assert
        model.DatabaseContextMock.Verify(dc => dc.GetEntry(model.FoundEntity!), Times.Once);
        model.EntityEntryMock.VerifySet(ee => ee.State = EntityState.Modified, Times.Once);
    }
}