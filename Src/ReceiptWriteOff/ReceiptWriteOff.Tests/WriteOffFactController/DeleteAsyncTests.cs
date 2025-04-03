using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Tests.WriteOffFactController.Model;

namespace ReceiptWriteOff.Tests.WriteOffFactController;

public class DeleteAsyncTests
{
    [Fact]
    public async Task DeleteAsync_DeletesWriteOffFact()
    {
        // Arrange
        int id = 1;
        var model = WriteOffFactControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.DeleteAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        model.ServiceMock.Verify(
            service => service.DeleteAsync(id, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenWriteOffFactNotFound()
    {
        // Arrange
        int id = 1;
        var model = WriteOffFactControllerTestsModelFactory.Create(writeOffFactExists: false);

        // Act
        var result = await model.Controller.DeleteAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
        notFoundResult.Value.Should().Be($"No Write Off Fact with Id {id} found");
        model.ServiceMock.Verify(
            service => service.DeleteAsync(id, CancellationToken.None),
            Times.Once);
    }
}