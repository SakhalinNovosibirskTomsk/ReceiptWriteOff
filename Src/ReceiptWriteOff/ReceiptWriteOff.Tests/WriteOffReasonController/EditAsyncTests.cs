using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Tests.WriteOffReasonController.Model;

namespace ReceiptWriteOff.Tests.WriteOffReasonController;

public class EditAsyncTests
{
    [Fact]
    public async Task EditAsync_EditsWriteOffReason()
    {
        // Arrange
        int id = 1;
        var model = WriteOffReasonControllerTestsModelFactory.Create();

        // Act
        var result =
            await model.Controller.EditAsync(id, model.CreateOrEditWriteOffReasonRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        model.ServiceMock.Verify(
            service => service.EditAsync(id, It.IsAny<CreateOrEditWriteOffReasonDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<CreateOrEditWriteOffReasonDto>(model.CreateOrEditWriteOffReasonRequest),
            Times.Once);
    }

    [Fact]
    public async Task EditAsync_ReturnsNotFound_WhenWriteOffReasonNotFound()
    {
        // Arrange
        int id = 1;
        var model = WriteOffReasonControllerTestsModelFactory.Create(writeOffReasonExists: false);

        // Act
        var result =
            await model.Controller.EditAsync(id, model.CreateOrEditWriteOffReasonRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
        notFoundResult.Value.Should().Be($"No Write-Off Reason with Id {id} found");
        model.ServiceMock.Verify(
            service => service.EditAsync(id, It.IsAny<CreateOrEditWriteOffReasonDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}