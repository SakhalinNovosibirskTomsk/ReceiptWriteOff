using Moq;
using ReceiptWriteOff.Services.Tests.BookServiceTests.Model;

namespace ReceiptWriteOff.Services.Tests.BookServiceTests;

public class DeleteToArchiveAsyncTests
{
    [Fact]
    public async Task DeleteToArchiveAsync_DeletesBookAndAddsToArchive()
    {
        // Arrange
        int id = 1;
        var model = BookServiceTestsModelFactory.Create();

        // Act
        await model.Service.DeleteToArchiveAsync(id, CancellationToken.None);

        // Assert
        model.BookRepositoryMock.Verify(
            repo => repo.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.BookRepositoryMock.Verify(
            repo => repo.DeleteAsync(id, CancellationToken.None), 
            Times.Once);
        model.BookArchiveRepositoryMock.Verify(
            repo => repo.AddAsync(model.Book, CancellationToken.None), 
            Times.Once);
        model.BookArchiveRepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None), 
            Times.Once);
    }
}