using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Tests.BookService.Model;

namespace ReceiptWriteOff.Application.Tests.BookService;

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
        model.Book.IsArchived.Should().BeTrue();
        model.BookRepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None), 
            Times.Once);
    }
}