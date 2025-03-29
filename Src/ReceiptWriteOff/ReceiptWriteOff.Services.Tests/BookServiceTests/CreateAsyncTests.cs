using FluentAssertions;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Tests.BookServiceTests.Model;

namespace ReceiptWriteOff.Services.Tests.BookServiceTests;

public class CreateAsyncTests
{
    [Fact]
    public async Task CreateAsync_CreatesNewBook()
    {
        // Arrange
        var model = BookServiceTestsModelFactory.Create();

        // Act
        var result = await model.Service.CreateAsync(model.CreateOrEditBookDto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(model.BookDto);
        model.BookRepositoryMock.Verify(
            repo => repo.AddAsync(model.Book, CancellationToken.None), 
            Times.Once);
        model.BookRepositoryMock.Verify(
            repo => repo.SaveChangesAsync(CancellationToken.None), 
            Times.Once);
        model.MapperMock.Verify(
            mapper => mapper.Map<Book>(model.CreateOrEditBookDto),
            Times.Once);
    }
}