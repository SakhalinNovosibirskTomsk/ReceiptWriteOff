using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Contracts.Book;
using ReceiptWriteOff.Tests.BookController.Model;

namespace ReceiptWriteOff.Tests.BookController;

public class EditAsyncTests
{
    [Fact]
    public async Task EditAsync_EditsBook()
    {
        // Arrange
        int id = 1;
        var model = BookControllerTestsModelFactory.Create();

        // Act
        var result = await model.Controller.EditAsync(id, model.CreateOrEditBookRequest, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        model.ServiceMock.Verify(
            service => service.EditAsync(id, It.IsAny<CreateOrEditBookDto>(), It.IsAny<CancellationToken>()),
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<CreateOrEditBookDto>(model.CreateOrEditBookRequest),
            Times.Once);
    }
}