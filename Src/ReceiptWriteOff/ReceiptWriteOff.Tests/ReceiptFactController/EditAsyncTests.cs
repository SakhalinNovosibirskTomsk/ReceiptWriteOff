using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Contracts.ReceiptFact;
using ReceiptWriteOff.Tests.ReceiptFactController.Model;

namespace ReceiptWriteOff.Tests.ReceiptFactController;

public class EditAsyncTests
{
    [Fact]
    public async Task EditAsync_EditsReceiptFact()
    {
        // Arrange
        int id = 1;
        var model = ReceiptFactControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.EditAsync(id, model.RegisterReceiptFactRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        model.ServiceMock.Verify(
            service => service.EditAsync(id, It.IsAny<RegisterReceiptFactDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<RegisterReceiptFactDto>(model.RegisterReceiptFactRequest),
            Times.Once);
    }

    [Fact]
    public async Task EditAsync_ReturnsNotFound_WhenReceiptFactNotFound()
    {
        // Arrange
        int id = 1;
        var model = ReceiptFactControllerTestsModelFactory.Create(receiptFactExists: false);

        // Act
        var result = await model.Controller.EditAsync(id, model.RegisterReceiptFactRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
        notFoundResult.Value.Should().Be($"No Receipt Fact with Id {id} found");
        model.ServiceMock.Verify(
            service => service.EditAsync(id, It.IsAny<RegisterReceiptFactDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}