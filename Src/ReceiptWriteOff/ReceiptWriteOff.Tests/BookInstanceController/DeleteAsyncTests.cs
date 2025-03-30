using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Tests.BookInstanceController.Model;

namespace ReceiptWriteOff.Tests.BookInstanceController;

public class DeleteAsyncTests
{
    [Fact]
    public async Task DeleteAsync_DeletesBookInstanceById()
    {
        // Arrange
        int id = 1;
        var model = BookInstanceControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.DeleteAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        model.ServiceMock.Verify(
            service => service.DeleteAsync(id, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenBookInstanceNotFound()
    {
        // Arrange
        int id = 1;
        var model = BookInstanceControllerTestsModelFactory.Create(bookInstanceExists: false);

        // Act
        var result = await model.Controller.DeleteAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
        notFoundResult.Value.Should().Be($"No Book Instance with Id {id} found");
        model.ServiceMock.Verify(
            service => service.DeleteAsync(id, CancellationToken.None), 
            Times.Once);
    }
}