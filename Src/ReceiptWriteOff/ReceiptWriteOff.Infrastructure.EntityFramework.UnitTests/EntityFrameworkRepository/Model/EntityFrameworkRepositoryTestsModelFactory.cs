using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using ReceiptWriteOff.Domain.Entities.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

public static class EntityFrameworkRepositoryTestsModelFactory
{
    public static EntityFrameworkRepositoryTestsModel Create(
        int entitiesCount,
        bool findAsyncReturnsNull)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var cancellationToken = CancellationToken.None;
        
        var asNoTrackingQueryableMock = fixture.Freeze<Mock<IQueryable<IEntity<PrimaryKeyStub>>>>();
        
        var entitySetMock = fixture.Freeze<Mock<IDbSet<IEntity<PrimaryKeyStub>>>>();
        entitySetMock.Setup(es => es.AddAsync(
                It.IsAny<IEntity<PrimaryKeyStub>>(), 
                cancellationToken))!
            .ReturnsAsync(null as EntityEntry<IEntity<PrimaryKeyStub>>);
        entitySetMock.Setup(es => es.AsNoTracking()).Returns(asNoTrackingQueryableMock.Object);
        
        var entitiesRange = fixture.CreateMany<IEntity<PrimaryKeyStub>>(entitiesCount).ToList();
        IEntity<PrimaryKeyStub>? foundEntity = findAsyncReturnsNull ? null : fixture.Freeze<IEntity<PrimaryKeyStub>>();
        foreach (var entity in entitiesRange)
        {
            object?[]? keyValues = [entity.Id];
            entitySetMock.Setup(es => es.FindAsync(keyValues, cancellationToken))
                .ReturnsAsync(foundEntity);
        }
        
        var databaseContextMock = fixture.Freeze<Mock<IDatabaseContext>>();
        databaseContextMock.Setup(dc => dc.Set<IEntity<PrimaryKeyStub>>())
            .Returns((DdSetDecorator<IEntity<PrimaryKeyStub>>)entitySetMock.Object);
        
        var repository = fixture.Freeze<EntityFrameworkRepository<IEntity<PrimaryKeyStub>, PrimaryKeyStub>>();
        
        var model = new EntityFrameworkRepositoryTestsModel
        {
            Repository = repository,
            EntitiesRange = entitiesRange,
            EntitySetMock = entitySetMock,
            FoundEntity = foundEntity,
            AsNoTrackingQueryableMock = asNoTrackingQueryableMock
        };
        
        return model;
    }
}