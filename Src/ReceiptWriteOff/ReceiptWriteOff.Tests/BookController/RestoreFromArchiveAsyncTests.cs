using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Tests.BookController.Model;

namespace ReceiptWriteOff.Tests.BookController;

public class RestoreFromArchiveAsyncTests
{
    [Fact]
    public async Task RestoreFromArchiveAsync_RestoresBook()
    {
        // Arrange
        int id = 1;
        var model = BookControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.RestoreFromArchiveAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        model.ServiceMock.Verify(
            service => service.RestoreFromArchiveAsync(id, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task RestoreFromArchiveAsync_ReturnsNotFound_WhenBookNotFound()
    {
        // Arrange
        int id = 1;
        var model = BookControllerTestsModelFactory.Create(bookExists: false);

        // Act
        var result = await model.Controller.RestoreFromArchiveAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
        notFoundResult.Value.Should().Be($"No Book with Id {id} found");
        model.ServiceMock.Verify(
            service => service.RestoreFromArchiveAsync(id, CancellationToken.None),
            Times.Once);
    }
}