using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Application.Implementations.Exceptions;
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

    [Fact]
    public async Task RegisterAsync_ThrowsAlreadyExistsException_WhenFactAlreadyExists()
    {
        // Arrange
        var model = WriteOffFactServiceTestsModelFactory.Create(factExists: true);

        // Act
        var act = async () => await model.Service.RegisterAsync(model.RegisterWriteOffFactDto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AlreadyExistsException>()
            .WithMessage(
                $"Write-off fact already exists for this book instance with id={model.RegisterWriteOffFactDto.BookInstanceId}.");
    }

    [Fact]
    public async Task RegisterAsync_ThrowsDateException_WhenWriteOffDateIsEarlierThanReceiptDate()
    {
        // Arrange
        var model = WriteOffFactServiceTestsModelFactory.Create(writeOffDateIsEarlier: true);

        // Act
        var act = async () => await model.Service.RegisterAsync(model.RegisterWriteOffFactDto, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DateException>()
            .WithMessage(
                $"Write-off fact date {model.RegisterWriteOffFactDto.Date} cannot be earlier than receipt fact date {model.ReceiptFact.Date}.");
    }
}