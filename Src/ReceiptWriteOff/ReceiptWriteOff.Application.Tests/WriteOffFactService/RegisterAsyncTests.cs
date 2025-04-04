using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Application.Tests.WriteOffFactService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.WriteOffFactService;

public class RegisterAsyncTests
{
    [Fact]
    public async Task RegisterAsync_CreatesNewWriteOffFact()
    {
        // Arrange
        var model = WriteOffFactServiceTestsModelFactory.Create();

        // Act
        var result = await model.Service.RegisterAsync(model.RegisterWriteOffFactDto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model.WriteOffFactDto);
        model.WriteOffFactUnitOfWorkMock.Verify(uow => uow.BookInstanceRepository, Times.Once);
        model.WriteOffFactUnitOfWorkMock.Verify(uow => uow.WriteOffFactRepository, Times.Once);
        model.WriteOffFactUnitOfWorkMock.Verify(uow => uow.WriteOffReasonRepository, Times.Once);
        model.BookInstanceRepositoryMock.Verify(
            repo => repo.GetAsync(model.RegisterWriteOffFactDto.BookInstanceId, CancellationToken.None), 
            Times.Once);
        model.WriteOffReasonRepositoryMock.Verify(
            repo => repo.GetAsync(model.RegisterWriteOffFactDto.WriteOffReasonId, CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffFact>(model.RegisterWriteOffFactDto),
            Times.Once);
        model.WriteOffFactRepositoryMock.Verify(
            repo => repo.AddAsync(model.WriteOffFact, CancellationToken.None), 
            Times.Once);
        model.WriteOffFactRepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<WriteOffFactDto>(model.WriteOffFact),
            Times.Once);
    }
}