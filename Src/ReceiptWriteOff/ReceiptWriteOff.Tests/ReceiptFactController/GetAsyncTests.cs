using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Contracts.ReceiptFact;
using ReceiptWriteOff.Tests.ReceiptFactController.Model;

namespace ReceiptWriteOff.Tests.ReceiptFactController;

public class GetAsyncTests
{
    [Fact]
    public async Task GetAsync_ReturnsReceiptFactById()
    {
        // Arrange
        int id = 1;
        var model = ReceiptFactControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.GetAsync(id, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result.Result;
        var receiptFactResponse = okResult.Value as ReceiptFactResponse;
        receiptFactResponse.Should().BeEquivalentTo(model.ReceiptFactResponse);
        model.ServiceMock.Verify(
            service => service.GetAsync(id, CancellationToken.None),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<ReceiptFactResponse>(It.IsAny<ReceiptFactDto>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAsync_ReturnsNotFound_WhenReceiptFactNotFound()
    {
        // Arrange
        int id = 1;
        var model = ReceiptFactControllerTestsModelFactory.Create(receiptFactExists: false);

        // Act
        var result = await model.Controller.GetAsync(id, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result.Result;
        notFoundResult.Value.Should().Be($"No Receipt Fact with Id {id} found");
        model.ServiceMock.Verify(
            service => service.GetAsync(id, CancellationToken.None),
            Times.Once);
    }
}