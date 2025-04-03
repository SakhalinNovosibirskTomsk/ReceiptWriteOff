using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Contracts.WriteOffFact;
using ReceiptWriteOff.Tests.WriteOffFactController.Model;

namespace ReceiptWriteOff.Tests.WriteOffFactController;

public class GetAllAsyncTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllWriteOffFacts()
    {
        // Arrange
        int writeOffFactsCount = 3;
        var model = WriteOffFactControllerTestsModelFactory.Create(writeOffFactsCount);

        // Act
        var result = await model.Controller.GetAllAsync(CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();

        var okObjectResult = (OkObjectResult)result.Result;
        okObjectResult.Value.Should().BeOfType<List<WriteOffFactShortResponse>>();

        var writeOffFacts = (List<WriteOffFactShortResponse>)okObjectResult.Value;
        writeOffFacts.Should().HaveCount(writeOffFactsCount);

        model.ServiceMock.Verify(
            service => service.GetAllAsync(CancellationToken.None),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffFactShortResponse>(It.IsAny<WriteOffFactShortDto>()),
            Times.Exactly(writeOffFactsCount));
    }
}