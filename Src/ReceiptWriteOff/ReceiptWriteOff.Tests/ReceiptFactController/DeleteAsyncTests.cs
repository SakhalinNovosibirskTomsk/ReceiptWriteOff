using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Tests.ReceiptFactController.Model;

namespace ReceiptWriteOff.Tests.ReceiptFactController;

public class DeleteAsyncTests
{
    [Fact]
    public async Task DeleteAsync_DeletesReceiptFact()
    {
        // Arrange
        int id = 1;
        var model = ReceiptFactControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.DeleteAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        model.ServiceMock.Verify(
            service => service.DeleteAsync(id, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenReceiptFactNotFound()
    {
        // Arrange
        int id = 1;
        var model = ReceiptFactControllerTestsModelFactory.Create(receiptFactExists: false);

        // Act
        var result = await model.Controller.DeleteAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
        notFoundResult.Value.Should().Be($"No Receipt Fact with Id {id} found");
        model.ServiceMock.Verify(
            service => service.DeleteAsync(id, CancellationToken.None),
            Times.Once);
    }
}