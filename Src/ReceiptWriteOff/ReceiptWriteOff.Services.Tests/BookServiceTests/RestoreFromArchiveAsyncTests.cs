using Moq;
using ReceiptWriteOff.Services.Tests.BookServiceTests.Model;

namespace ReceiptWriteOff.Services.Tests.BookServiceTests;

public class RestoreFromArchiveAsyncTests
{
    [Fact]
    public async Task RestoreFromArchiveAsync_RestoresBookFromArchive()
    {
        // Arrange
        int id = 1;
        var model = BookServiceTestsModelFactory.Create();

        // Act
        await model.Service.RestoreFromArchiveAsync(id, CancellationToken.None);

        // Assert
        model.BookArchiveRepositoryMock.Verify(
            repo => repo.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.BookArchiveRepositoryMock.Verify(
            repo => repo.DeleteAsync(id, CancellationToken.None), 
            Times.Once);
        model.BookRepositoryMock.Verify(
            repo => repo.AddAsync(model.Book, CancellationToken.None), 
            Times.Once);
        model.BookRepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None), 
            Times.Once);
    }
}