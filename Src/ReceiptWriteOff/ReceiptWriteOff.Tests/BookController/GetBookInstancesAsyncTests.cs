using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Contracts.Book;
using ReceiptWriteOff.Contracts.BookInstance;
using ReceiptWriteOff.Tests.BookController.Model;

namespace ReceiptWriteOff.Tests.BookController;

public class GetBookInstancesAsyncTests
{
    [Fact]
    public async Task GetBookInstancesAsync_ReturnsBookInstancesByBookId()
    {
        // Arrange
        int id = 1;
        int instancesCount = 3;
        var model = BookControllerTestsModelFactory.Create(instancesCount);

        // Act
        var result = await model.Controller.GetBookInstancesAsync(id, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();

        var okObjectResult = (OkObjectResult)result.Result;
        okObjectResult.Value.Should().BeOfType<List<BookInstanceShortResponse>>();

        var instances = (List<BookInstanceShortResponse>)okObjectResult.Value;
        instances.Should().HaveCount(instancesCount);

        model.ServiceMock.Verify(
            service => service.GetBookInstancesAsync(id, CancellationToken.None),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<BookInstanceShortResponse>(It.IsAny<BookInstanceShortDto>()),
            Times.Exactly(instancesCount));
    }
}