using Moq;
using ReceiptWriteOff.Application.Tests.WriteOffFactService.Model;

namespace ReceiptWriteOff.Application.Tests.WriteOffFactService;

public class DeleteAsyncTests
{
    [Fact]
    public async Task DeleteAsync_DeletesWriteOffFactById()
    {
        // Arrange
        int id = 1;
        var model = WriteOffFactServiceTestsModelFactory.Create();

        // Act
        await model.Service.DeleteAsync(id, CancellationToken.None);

        // Assert
        model.WriteOffFactUnitOfWorkMock.Verify(uow => uow.WriteOffFactRepository, Times.Once);
        model.WriteOffFactRepositoryMock.Verify(
            repo => repo.DeleteAsync(id, CancellationToken.None),
            Times.Once);
        model.WriteOffFactRepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None),
            Times.Once);
    }
}