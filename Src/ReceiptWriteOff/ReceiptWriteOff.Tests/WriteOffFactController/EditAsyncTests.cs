using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Tests.WriteOffFactController.Model;

namespace ReceiptWriteOff.Tests.WriteOffFactController;

public class EditAsyncTests
{
    [Fact]
    public async Task EditAsync_EditsWriteOffFact()
    {
        // Arrange
        int id = 1;
        var model = WriteOffFactControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.EditAsync(id, model.RegisterWriteOffFactRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkResult>();
        model.ServiceMock.Verify(
            service => service.EditAsync(id, It.IsAny<RegisterWriteOffFactDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<RegisterWriteOffFactDto>(model.RegisterWriteOffFactRequest),
            Times.Once);
    }

    [Fact]
    public async Task EditAsync_ReturnsNotFound_WhenWriteOffFactNotFound()
    {
        // Arrange
        int id = 1;
        var model = WriteOffFactControllerTestsModelFactory.Create(writeOffFactExists: false);

        // Act
        var result = await model.Controller.EditAsync(id, model.RegisterWriteOffFactRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
        notFoundResult.Value.Should().Be($"No Write Off Fact with Id {id} found");
        model.ServiceMock.Verify(
            service => service.EditAsync(id, It.IsAny<RegisterWriteOffFactDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}