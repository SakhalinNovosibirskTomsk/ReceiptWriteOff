using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Contracts.WriteOffReason;
using ReceiptWriteOff.Tests.WriteOffReasonController.Model;

namespace ReceiptWriteOff.Tests.WriteOffReasonController;

public class GetAllAsyncTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllWriteOffReasons()
    {
        // Arrange
        int writeOffReasonsCount = 3;
        var model = WriteOffReasonControllerTestsModelFactory.Create(writeOffReasonsCount);

        // Act
        var result = await model.Controller.GetAllAsync(CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();

        var okObjectResult = (OkObjectResult)result.Result;
        okObjectResult.Value.Should().BeOfType<List<WriteOffReasonResponse>>();

        var writeOffReasons = (List<WriteOffReasonResponse>)okObjectResult.Value;
        writeOffReasons.Should().HaveCount(writeOffReasonsCount);

        model.ServiceMock.Verify(
            service => service.GetAllAsync(CancellationToken.None),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffReasonResponse>(It.IsAny<WriteOffReasonDto>()),
            Times.Exactly(writeOffReasonsCount));
    }
}