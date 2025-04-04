using Moq;
using ReceiptWriteOff.Application.Tests.BookInstanceService.Model;

namespace ReceiptWriteOff.Application.Tests.BookInstanceService;

public class DeleteAsyncTests
{
    [Fact]
    public async Task DeleteAsync_DeletesBookInstanceById()
    {
        // Arrange
        int id = 1;
        var model = BookInstanceServiceTestsModelFactory.Create();

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