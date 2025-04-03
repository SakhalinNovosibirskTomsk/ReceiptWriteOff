using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Contracts.WriteOffFact;
using ReceiptWriteOff.Tests.WriteOffFactController.Model;

namespace ReceiptWriteOff.Tests.WriteOffFactController;

public class GetAsyncTests
{
    [Fact]
    public async Task GetAsync_ReturnsWriteOffFactById()
    {
        // Arrange
        int id = 1;
        var model = WriteOffFactControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.GetAsync(id, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result.Result;
        var writeOffFactResponse = okResult.Value as WriteOffFactResponse;
        writeOffFactResponse.Should().BeEquivalentTo(model.WriteOffFactResponse);
        model.ServiceMock.Verify(
            service => service.GetAsync(id, CancellationToken.None),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffFactResponse>(It.IsAny<WriteOffFactDto>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAsync_ReturnsNotFound_WhenWriteOffFactNotFound()
    {
        // Arrange
        int id = 1;
        var model = WriteOffFactControllerTestsModelFactory.Create(writeOffFactExists: false);

        // Act
        var result = await model.Controller.GetAsync(id, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result.Result;
        notFoundResult.Value.Should().Be($"No Write Off Fact with Id {id} found");
        model.ServiceMock.Verify(
            service => service.GetAsync(id, CancellationToken.None),
            Times.Once);
    }
}