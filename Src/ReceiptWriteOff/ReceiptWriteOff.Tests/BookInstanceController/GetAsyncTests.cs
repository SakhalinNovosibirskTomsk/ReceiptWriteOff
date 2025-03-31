using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Models.BookInstance;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Tests.BookInstanceController.Model;

namespace ReceiptWriteOff.Tests.BookInstanceController;

public class GetAsyncTests
{
    [Fact]
    public async Task GetAsync_ReturnsBookInstanceById()
    {
        // Arrange
        int id = 1;
        var model = BookInstanceControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.GetAsync(id, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result.Result;
        var bookInstanceResponse = okResult.Value as BookInstanceResponse;
        bookInstanceResponse.Should().BeEquivalentTo(model.BookInstanceResponse);
        model.ServiceMock.Verify(
            service => service.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<BookInstanceResponse>(It.IsAny<BookInstanceDto>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAsync_ReturnsNotFound_WhenBookInstanceNotFound()
    {
        // Arrange
        int id = 1;
        var model = BookInstanceControllerTestsModelFactory.Create(bookInstanceExists: false);

        // Act
        var result = await model.Controller.GetAsync(id, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result.Result;
        notFoundResult.Value.Should().Be($"No Book Instance with Id {id} found");
        model.ServiceMock.Verify(
            service => service.GetAsync(id, CancellationToken.None), 
            Times.Once);
    }
}