using Moq;
using ReceiptWriteOff.Services.Tests.BookService.Model;

namespace ReceiptWriteOff.Services.Tests.BookService;

public class EditAsyncTests
{
    [Fact]
    public async Task EditAsync_UpdatesBook()
    {
        // Arrange
        int id = 1;
        var model = BookServiceTestsModelFactory.Create();

        // Act
        await model.Service.EditAsync(id, model.CreateOrEditBookDto, CancellationToken.None);

        // Assert
        model.BookRepositoryMock.Verify(
            repo => repo.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map(model.CreateOrEditBookDto, model.Book),
            Times.Once);
    }
}