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
        
        var asNoTrackingQueryableMock = fixture.Freeze<Mock<IQueryable<IEntity<int>>>>();
        
        var entitySetMock = fixture.Freeze<Mock<IDbSet<IEntity<int>>>>();
        entitySetMock.Setup(es => es.AddAsync(
                It.IsAny<IEntity<int>>(), 
                cancellationToken))!
            .ReturnsAsync(null as EntityEntry<IEntity<int>>);
        entitySetMock.Setup(es => es.AsNoTracking()).Returns(asNoTrackingQueryableMock.Object);
        
        var entitiesRange = fixture.CreateMany<IEntity<int>>(entitiesCount).ToList();
        IEntity<int>? foundEntity = findAsyncReturnsNull ? null : fixture.Freeze<IEntity<int>>();
        foreach (var entity in entitiesRange)
        {
            object?[]? keyValues = [entity.Id];
            entitySetMock.Setup(es => es.FindAsync(keyValues, cancellationToken))
                .ReturnsAsync(foundEntity);
        }
        
        var databaseContextMock = fixture.Freeze<Mock<IDatabaseContext>>();
        databaseContextMock.Setup(dc => dc.GetDbSet<IEntity<int>>())
            .Returns(entitySetMock.Object);
        
        var repository = fixture.Freeze<EntityFrameworkRepository<IEntity<int>, int>>();
        
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