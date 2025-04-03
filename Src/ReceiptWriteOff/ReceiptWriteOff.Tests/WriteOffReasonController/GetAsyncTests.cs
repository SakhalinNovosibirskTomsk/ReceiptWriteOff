using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Contracts.WriteOffReason;
using ReceiptWriteOff.Tests.WriteOffReasonController.Model;

namespace ReceiptWriteOff.Tests.WriteOffReasonController;

public class GetAsyncTests
{
    [Fact]
    public async Task GetAsync_ReturnsWriteOffReasonById()
    {
        // Arrange
        int id = 1;
        var model = WriteOffReasonControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.GetAsync(id, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result.Result;
        var writeOffReasonResponse = okResult.Value as WriteOffReasonResponse;
        writeOffReasonResponse.Should().BeEquivalentTo(model.WriteOffReasonResponse);
        model.ServiceMock.Verify(
            service => service.GetAsync(id, CancellationToken.None),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffReasonResponse>(It.IsAny<WriteOffReasonDto>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAsync_ReturnsNotFound_WhenWriteOffReasonNotFound()
    {
        // Arrange
        int id = 1;
        var model = WriteOffReasonControllerTestsModelFactory.Create(writeOffReasonExists: false);

        // Act
        var result = await model.Controller.GetAsync(id, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result.Result;
        notFoundResult.Value.Should().Be($"No Write-Off Reason with Id {id} found");
        model.ServiceMock.Verify(
            service => service.GetAsync(id, CancellationToken.None),
            Times.Once);
    }
}