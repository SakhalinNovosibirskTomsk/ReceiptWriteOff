using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Contracts.Book;
using ReceiptWriteOff.Tests.BookController.Model;

namespace ReceiptWriteOff.Tests.BookController;

public class GetAsyncTests
{
    [Fact]
    public async Task GetAsync_ReturnsBookById()
    {
        // Arrange
        int id = 1;
        var model = BookControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.GetAsync(id, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result.Result;
        var bookResponse = okResult.Value as BookResponse;
        bookResponse.Should().BeEquivalentTo(model.BookResponse);
        model.ServiceMock.Verify(
            service => service.GetAsync(id, CancellationToken.None),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<BookResponse>(It.IsAny<BookDto>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAsync_ReturnsNotFound_WhenBookNotFound()
    {
        // Arrange
        int id = 1;
        var model = BookControllerTestsModelFactory.Create(bookExists: false);

        // Act
        var result = await model.Controller.GetAsync(id, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result.Result;
        notFoundResult.Value.Should().Be($"No Book with Id {id} found");
        model.ServiceMock.Verify(
            service => service.GetAsync(id, CancellationToken.None),
            Times.Once);
    }
}