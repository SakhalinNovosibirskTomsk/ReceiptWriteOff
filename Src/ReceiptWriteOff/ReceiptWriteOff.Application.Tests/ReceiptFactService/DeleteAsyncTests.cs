using Moq;
using ReceiptWriteOff.Application.Tests.ReceiptFactService.Model;

namespace ReceiptWriteOff.Application.Tests.ReceiptFactService;

public class DeleteAsyncTests
{
    [Fact]
    public async Task DeleteAsync_DeletesReceiptFactById()
    {
        // Arrange
        int id = 1;
        var model = ReceiptFactServiceTestsModelFactory.Create();

        // Act
        await model.Service.DeleteAsync(id, CancellationToken.None);

        // Assert
        model.RepositoryMock.Verify(
            repo => repo.DeleteAsync(id, CancellationToken.None),
            Times.Once);
        model.RepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None),
            Times.Once);
    }
}