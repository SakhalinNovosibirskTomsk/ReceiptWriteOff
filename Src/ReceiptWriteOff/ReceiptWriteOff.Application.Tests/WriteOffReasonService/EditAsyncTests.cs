using Moq;
using ReceiptWriteOff.Application.Tests.WriteOffReasonService.Model;

namespace ReceiptWriteOff.Application.Tests.WriteOffReasonService;

public class EditAsyncTests
{
    [Fact]
    public async Task EditAsync_UpdatesWriteOffReason()
    {
        // Arrange
        int id = 1;
        var model = WriteOffReasonServiceTestsModelFactory.Create();

        // Act
        await model.Service.EditAsync(id, model.CreateOrEditWriteOffReasonDto, CancellationToken.None);

        // Assert
        model.RepositoryMock.Verify(
            repo => repo.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map(model.CreateOrEditWriteOffReasonDto, model.WriteOffReason),
            Times.Once);
    }
}