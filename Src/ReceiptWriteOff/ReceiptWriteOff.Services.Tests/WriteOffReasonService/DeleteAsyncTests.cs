using Moq;
using ReceiptWriteOff.Services.Tests.WriteOffReasonService.Model;

namespace ReceiptWriteOff.Services.Tests.WriteOffReasonService;

public class DeleteAsyncTests
{
    [Fact]
    public async Task DeleteAsync_DeletesWriteOffReasonById()
    {
        // Arrange
        int id = 1;
        var model = WriteOffReasonServiceTestsModelFactory.Create();

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