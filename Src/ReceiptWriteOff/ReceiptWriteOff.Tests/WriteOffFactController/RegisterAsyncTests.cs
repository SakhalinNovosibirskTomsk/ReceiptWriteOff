using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Tests.WriteOffFactController.Model;

namespace ReceiptWriteOff.Tests.WriteOffFactController;

public class RegisterAsyncTests
{
    [Fact]
    public async Task RegisterAsync_CreatesWriteOffFact()
    {
        // Arrange
        var model = WriteOffFactControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.RegisterAsync(model.RegisterWriteOffFactRequest, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdAtActionResult = (CreatedAtActionResult)result.Result;
        createdAtActionResult.Value.Should().BeEquivalentTo(model.WriteOffFactResponse);
        model.ServiceMock.Verify(
            service => service.RegisterAsync(It.IsAny<RegisterWriteOffFactDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<RegisterWriteOffFactDto>(model.RegisterWriteOffFactRequest),
            Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_ReturnsConflict_WhenAlreadyExists()
    {
        // Arrange
        var model = WriteOffFactControllerTestsModelFactory.Create(alreadyExistsOnRegister: true);

        // Act
        var result = await model.Controller.RegisterAsync(model.RegisterWriteOffFactRequest, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<ConflictObjectResult>();
        var conflictResult = (ConflictObjectResult)result.Result;
        conflictResult.Value.Should().Be("WriteOffFact already exists");
    }

    [Fact]
    public async Task RegisterAsync_ReturnsNotFound_WhenEntityNotFound()
    {
        // Arrange
        var model = WriteOffFactControllerTestsModelFactory.Create(entityNotFoundOnRegister: true);

        // Act
        var result = await model.Controller.RegisterAsync(model.RegisterWriteOffFactRequest, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result.Result;
        notFoundResult.Value.Should().Be("Entity not found");
    }

    [Fact]
    public async Task RegisterAsync_ReturnsBadRequest_WhenDateIsInvalid()
    {
        // Arrange
        var model = WriteOffFactControllerTestsModelFactory.Create(invalidDateOnRegister: true);

        // Act
        var result = await model.Controller.RegisterAsync(model.RegisterWriteOffFactRequest, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = (BadRequestObjectResult)result.Result;
        badRequestResult.Value.Should().Be("Invalid date");
    }
}