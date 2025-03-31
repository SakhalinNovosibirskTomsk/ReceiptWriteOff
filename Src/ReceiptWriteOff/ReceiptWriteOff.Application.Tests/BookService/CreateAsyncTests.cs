using FluentAssertions;
using Moq;
using ReceiptWriteOff.Application.Tests.BookService.Model;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Tests.BookService;

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