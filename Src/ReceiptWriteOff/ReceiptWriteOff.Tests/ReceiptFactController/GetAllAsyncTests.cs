using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Contracts.ReceiptFact;
using ReceiptWriteOff.Tests.ReceiptFactController.Model;

namespace ReceiptWriteOff.Tests.ReceiptFactController;

public class GetAllAsyncTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllReceiptFacts()
    {
        // Arrange
        int receiptFactsCount = 3;
        var model = ReceiptFactControllerTestsModelFactory.Create(receiptFactsCount);

        // Act
        var result = await model.Controller.GetAllAsync(CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();

        var okObjectResult = (OkObjectResult)result.Result;
        okObjectResult.Value.Should().BeOfType<List<ReceiptFactShortResponse>>();

        var receiptFacts = (List<ReceiptFactShortResponse>)okObjectResult.Value;
        receiptFacts.Should().HaveCount(receiptFactsCount);

        model.ServiceMock.Verify(
            service => service.GetAllAsync(CancellationToken.None),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<ReceiptFactShortResponse>(It.IsAny<ReceiptFactShortDto>()),
            Times.Exactly(receiptFactsCount));
    }
}