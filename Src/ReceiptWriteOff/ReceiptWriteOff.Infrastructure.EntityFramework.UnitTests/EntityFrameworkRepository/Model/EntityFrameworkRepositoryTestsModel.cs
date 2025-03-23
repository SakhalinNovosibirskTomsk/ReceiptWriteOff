using Microsoft.EntityFrameworkCore;
using Moq;
using ReceiptWriteOff.Domain.Entities.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

public class EntityFrameworkRepositoryTestsModel
{
    public required EntityFrameworkRepository<IEntity<PrimaryKeyStub>, PrimaryKeyStub> Repository { get; set; }
    public required Mock<DbSet<IEntity<PrimaryKeyStub>>> EntitySetMock { get; set; }
    public required List<IEntity<PrimaryKeyStub>> EntitiesRange  { get; set; }
}