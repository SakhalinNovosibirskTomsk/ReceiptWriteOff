using Moq;
using ReceiptWriteOff.Services.Tests.WriteOffFactService.Model;

namespace ReceiptWriteOff.Services.Tests.WriteOffFactService;

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
        model.RepositoryMock.Verify(
            repo => repo.DeleteAsync(id, CancellationToken.None),
            Times.Once);
        model.RepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None),
            Times.Once);
    }
}