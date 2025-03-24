using Moq;
using ReceiptWriteOff.Domain.Entities.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

public class EntityFrameworkRepositoryTestsModel
{
    public required EntityFrameworkRepository<IEntity<int>, int> Repository { get; set; }
    public required Mock<IDbSet<IEntity<int>>> EntitySetMock { get; set; }
    public required List<IEntity<int>> EntitiesRange  { get; set; }
    public IEntity<int>? FoundEntity { get; set; }
    public required Mock<IQueryable<IEntity<int>>> AsNoTrackingQueryableMock { get; set; }
}