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
    public async Task RegisterAsync_ReturnsNotFound_WhenSomeEntityNotFound()
    {
        // Arrange
        int id = 1;
        var model = WriteOffFactControllerTestsModelFactory.Create(writeOffFactExists: false);

        // Act
        var result = await model.Controller.RegisterAsync(model.RegisterWriteOffFactRequest, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
        model.ServiceMock.Verify(
            service => service.RegisterAsync(It.IsAny<RegisterWriteOffFactDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}