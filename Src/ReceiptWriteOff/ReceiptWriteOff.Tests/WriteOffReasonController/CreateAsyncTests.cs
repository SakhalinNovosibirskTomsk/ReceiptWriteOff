using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Tests.WriteOffReasonController.Model;

namespace ReceiptWriteOff.Tests.WriteOffReasonController;

public class CreateAsyncTests
{
    [Fact]
    public async Task CreateAsync_CreatesWriteOffReason()
    {
        // Arrange
        var model = WriteOffReasonControllerTestsModelFactory.Create();

        // Act
        var result =
            await model.Controller.CreateAsync(model.CreateOrEditWriteOffReasonRequest, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdAtActionResult = (CreatedAtActionResult)result.Result;
        createdAtActionResult.Value.Should().BeEquivalentTo(model.WriteOffReasonResponse);
        model.ServiceMock.Verify(
            service => service.CreateAsync(It.IsAny<CreateOrEditWriteOffReasonDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<CreateOrEditWriteOffReasonDto>(model.CreateOrEditWriteOffReasonRequest),
            Times.Once);
    }
}