using Moq;
using ReceiptWriteOff.Application.Tests.WriteOffFactService.Model;

namespace ReceiptWriteOff.Application.Tests.WriteOffFactService;

public class EditAsyncTests
{
    [Fact]
    public async Task EditAsync_UpdatesWriteOffFact()
    {
        // Arrange
        int id = 1;
        var model = WriteOffFactServiceTestsModelFactory.Create();

        // Act
        await model.Service.EditAsync(id, model.RegisterWriteOffFactDto, CancellationToken.None);

        // Assert
        model.WriteOffFactUnitOfWorkMock.Verify(uow => uow.WriteOffFactRepository, Times.Once);
        model.WriteOffFactRepositoryMock.Verify(
            repo => repo.GetAsync(id, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map(model.RegisterWriteOffFactDto, model.WriteOffFact),
            Times.Once);
        model.WriteOffFactRepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None), 
            Times.Once);
    }
}