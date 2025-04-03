using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Tests.WriteOffReasonController.Model;

namespace ReceiptWriteOff.Tests.WriteOffReasonController;

public class DeleteAsyncTests
{
    [Fact]
    public async Task DeleteAsync_DeletesWriteOffReason()
    {
        // Arrange
        int id = 1;
        var model = WriteOffReasonControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.DeleteAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        model.ServiceMock.Verify(
            service => service.DeleteAsync(id, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_WhenWriteOffReasonNotFound()
    {
        // Arrange
        int id = 1;
        var model = WriteOffReasonControllerTestsModelFactory.Create(writeOffReasonExists: false);

        // Act
        var result = await model.Controller.DeleteAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
        notFoundResult.Value.Should().Be($"No Write-Off Reason with Id {id} found");
        model.ServiceMock.Verify(
            service => service.DeleteAsync(id, CancellationToken.None),
            Times.Once);
    }
}