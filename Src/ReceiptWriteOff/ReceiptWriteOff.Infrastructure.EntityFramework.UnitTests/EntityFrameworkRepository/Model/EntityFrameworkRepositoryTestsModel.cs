using Moq;
using ReceiptWriteOff.Domain.Entities.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

public class EntityFrameworkRepositoryTestsModel
{
    public required EntityFrameworkRepository<IEntity<PrimaryKeyStub>, PrimaryKeyStub> Repository { get; set; }
    public required Mock<IDbSet<IEntity<PrimaryKeyStub>>> EntitySetMock { get; set; }
    public required List<IEntity<PrimaryKeyStub>> EntitiesRange  { get; set; }
    public IEntity<PrimaryKeyStub>? FoundEntity { get; set; }
    public required Mock<IQueryable<IEntity<PrimaryKeyStub>>> AsNoTrackingQueryableMock { get; set; }
}