using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Contracts.Book;
using ReceiptWriteOff.Tests.BookController.Model;

namespace ReceiptWriteOff.Tests.BookController;

public class GetAllAsyncTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllBooks()
    {
        // Arrange
        int booksCount = 3;
        var model = BookControllerTestsModelFactory.Create(booksCount);

        // Act
        var result = await model.Controller.GetAllAsync(CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();

        var okObjectResult = (OkObjectResult)result.Result;
        okObjectResult.Value.Should().BeOfType<List<BookResponse>>();

        var books = (List<BookResponse>)okObjectResult.Value;
        books.Should().HaveCount(booksCount);

        model.ServiceMock.Verify(
            service => service.GetAllAsync(CancellationToken.None),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<BookResponse>(It.IsAny<BookDto>()),
            Times.Exactly(booksCount));
    }
}