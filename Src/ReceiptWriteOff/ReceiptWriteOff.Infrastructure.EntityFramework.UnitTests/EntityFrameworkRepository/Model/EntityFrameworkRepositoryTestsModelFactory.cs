using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ReceiptWriteOff.Domain.Entities.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.UnitTests.EntityFrameworkRepository.Model;

public static class EntityFrameworkRepositoryTestsModelFactory
{
    public static EntityFrameworkRepositoryTestsModel Create(int entitiesCount = 0)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var dbSetMock = fixture.Freeze<Mock<DbSet<IEntity<PrimaryKeyStub>>>>();
        var entitiesRange = new List<IEntity<PrimaryKeyStub>>();
        var databaseContextMock = fixture.Freeze<Mock<IDatabaseContext>>();
        var repository = new EntityFrameworkRepository<IEntity<PrimaryKeyStub>, PrimaryKeyStub>(databaseContextMock.Object);
        
        var model = new EntityFrameworkRepositoryTestsModel
        {
            Repository = repository,
            EntitiesRange = entitiesRange,
            EntitySetMock = dbSetMock
        };
        
        databaseContextMock.Setup(dc => dc.Set<IEntity<PrimaryKeyStub>>()).Returns(dbSetMock.Object);
        
        return model;
    }
}