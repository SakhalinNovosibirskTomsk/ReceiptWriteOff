using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Tests.BookRepository.Model;

public class BookRepositoryTestsModel
{
    public required Implementation.BookRepository Repository { get; set; }
    public required Mock<IDatabaseContext> DatabaseContextMock { get; set; }
    public required Mock<IQueryableExtensionsWrapper<Book>> QueryableExtensionsWrapperMock { get; set; }
    public required List<Book> Books { get; set; }
    public required Mock<IQueryable<Book>> QueryableMock { get; set; }
}