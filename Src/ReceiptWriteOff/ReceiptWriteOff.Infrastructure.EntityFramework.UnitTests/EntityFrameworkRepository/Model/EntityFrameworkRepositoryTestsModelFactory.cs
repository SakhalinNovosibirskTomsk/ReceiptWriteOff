using AutoFixture;
using AutoFixture.AutoMoq;
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
        
        var queryableMock = fixture.Freeze<Mock<IQueryable<IEntity<int>>>>();
        
        IEntity<int>? foundEntity = findAsyncReturnsNull ? null : fixture.Freeze<IEntity<int>>();
        
        var entitySetMock = SetupEntitySetMock(fixture, cancellationToken, queryableMock);
        
        var entitiesRange = SetupEntitiesRange(entitiesCount, fixture, entitySetMock, cancellationToken, foundEntity);

        SetupQueriableExtensionsMock(fixture, entitiesRange, cancellationToken);
        
        SetupDatabaseMock(fixture, entitySetMock);
        
        var repository = fixture.Freeze<EntityFrameworkRepository<IEntity<int>, int>>();
        
        var model = new EntityFrameworkRepositoryTestsModel
        {
            Repository = repository,
            EntitiesRange = entitiesRange,
            EntitySetMock = entitySetMock,
            FoundEntity = foundEntity,
            QueryableMock = queryableMock,
            QueryableExtensionsMock = fixture.Freeze<Mock<IQueryableExtensionsWrapper<IEntity<int>>>>()
        };
        
        return model;
    }

    private static Mock<IQueryableExtensionsWrapper<IEntity<int>>> SetupQueriableExtensionsMock(
        IFixture fixture,
        List<IEntity<int>> entitiesRange,
        CancellationToken cancellationToken)
    {
        var queryableExtensionsWrapperMock = fixture.Freeze<Mock<IQueryableExtensionsWrapper<IEntity<int>>>>();
        queryableExtensionsWrapperMock.Setup(qe => qe.ToListAsync(
            It.IsAny<IQueryable<IEntity<int>>>(),
            cancellationToken))
            .ReturnsAsync(entitiesRange);

        return queryableExtensionsWrapperMock;
    }

    private static void SetupDatabaseMock(IFixture fixture, Mock<IDbSet<IEntity<int>>> entitySetMock)
    {
        var databaseContextMock = fixture.Freeze<Mock<IDatabaseContext>>();
        databaseContextMock.Setup(dc => dc.GetDbSet<IEntity<int>>())
            .Returns(entitySetMock.Object);
    }

    private static List<IEntity<int>> SetupEntitiesRange(int entitiesCount,
        IFixture fixture,
        Mock<IDbSet<IEntity<int>>> entitySetMock,
        CancellationToken cancellationToken,
        IEntity<int>? foundEntity)
    {
        var entitiesRange = fixture.CreateMany<IEntity<int>>(entitiesCount).ToList();
        foreach (var entity in entitiesRange)
        {
            object?[]? keyValues = [entity.Id];
            entitySetMock.Setup(es => es.FindAsync(keyValues, cancellationToken))
                .ReturnsAsync(foundEntity);
        }

        return entitiesRange;
    }

    private static Mock<IDbSet<IEntity<int>>> SetupEntitySetMock(IFixture fixture,
        CancellationToken cancellationToken,
        Mock<IQueryable<IEntity<int>>> asNoTrackingQueryableMock)
    {
        var entitySetMock = fixture.Freeze<Mock<IDbSet<IEntity<int>>>>();
        entitySetMock.Setup(es => es.AddAsync(
                It.IsAny<IEntity<int>>(), 
                cancellationToken))!
            .ReturnsAsync(null as EntityEntry<IEntity<int>>);
        entitySetMock.Setup(es => es.AsNoTracking())
            .Returns(asNoTrackingQueryableMock.Object);
        return entitySetMock;
    }
}