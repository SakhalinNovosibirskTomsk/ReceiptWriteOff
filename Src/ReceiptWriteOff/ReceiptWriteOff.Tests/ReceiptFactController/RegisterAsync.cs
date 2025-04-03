using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Contracts.ReceiptFact;
using ReceiptWriteOff.Tests.ReceiptFactController.Model;

namespace ReceiptWriteOff.Tests.ReceiptFactController;

public class RegisterAsyncTests
{
    [Fact]
    public async Task RegisterAsync_RegistersReceiptFact()
    {
        // Arrange
        var model = ReceiptFactControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.RegisterAsync(model.RegisterReceiptFactRequest, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdAtActionResult = (CreatedAtActionResult)result.Result;
        createdAtActionResult.Value.Should().BeEquivalentTo(model.ReceiptFactResponse);
        model.ServiceMock.Verify(
            service => service.RegisterAsync(It.IsAny<RegisterReceiptFactDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<RegisterReceiptFactDto>(model.RegisterReceiptFactRequest),
            Times.Once);
    }
}