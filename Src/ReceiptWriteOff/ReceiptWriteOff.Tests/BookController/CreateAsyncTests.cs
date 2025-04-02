using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Contracts.Book;
using ReceiptWriteOff.Tests.BookController.Model;

namespace ReceiptWriteOff.Tests.BookController;

public class CreateAsyncTests
{
    [Fact]
    public async Task CreateAsync_CreatesBook()
    {
        // Arrange
        var model = BookControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.CreateAsync(model.CreateOrEditBookRequest, CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdAtActionResult = (CreatedAtActionResult)result.Result;
        createdAtActionResult.Value.Should().BeEquivalentTo(model.Book);
        model.ServiceMock.Verify(
            service => service.CreateAsync(It.IsAny<CreateOrEditBookDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<CreateOrEditBookDto>(model.CreateOrEditBookRequest),
            Times.Once);
    }
}