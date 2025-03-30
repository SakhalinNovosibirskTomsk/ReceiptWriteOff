using Moq;
using ReceiptWriteOff.Services.Tests.ReceiptFactService.Model;

namespace ReceiptWriteOff.Services.Tests.ReceiptFactService;

public class EditAsyncTests
{
    [Fact]
    public async Task EditAsync_UpdatesReceiptFact()
    {
        // Arrange
        int id = 1;
        var model = ReceiptFactServiceTestsModelFactory.Create();

        // Act
        await model.Service.EditAsync(id, model.RegisterReceiptFactDto, CancellationToken.None);

        // Assert
        model.RepositoryMock.Verify(
            repo => repo.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map(model.RegisterReceiptFactDto, model.ReceiptFact),
            Times.Once);
    }
}