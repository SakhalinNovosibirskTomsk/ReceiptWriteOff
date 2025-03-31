using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReceiptWriteOff.Models.BookInstance;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Tests.BookInstanceController.Model;

namespace ReceiptWriteOff.Tests.BookInstanceController;

public class GetAllAsyncTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllBookInstances()
    {
        // Arrange
        int bookInstancesCount = 3;
        var model = BookInstanceControllerTestsModelFactory.Create(bookInstancesCount);

        // Act
        var result = await model.Controller.GetAllAsync(CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        
        var okObjectResult = (OkObjectResult)result.Result;
        okObjectResult.Value.Should().BeOfType<List<BookInstanceShortResponse>>();
        
        var bookInstances = (List<BookInstanceShortResponse>)okObjectResult.Value;
        bookInstances.Should().HaveCount(bookInstancesCount);
        
        model.ServiceMock.Verify(
            service => service.GetAllAsync(CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<BookInstanceShortResponse>(It.IsAny<BookInstanceShortDto>()),
            Times.Exactly(bookInstancesCount));
    }
}