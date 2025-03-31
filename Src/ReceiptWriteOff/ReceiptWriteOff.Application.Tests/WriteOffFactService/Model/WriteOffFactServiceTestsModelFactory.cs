using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Application.Tests.WriteOffFactService.Model;

public static class WriteOffFactServiceTestsModelFactory
{
    public static WriteOffFactServiceTestsModel Create(int writeOffFactsCount = 0)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var writeOffFacts = fixture.CreateMany<WriteOffFact>(writeOffFactsCount).ToList();
        var writeOffFact = fixture.Freeze<WriteOffFact>();

        var repositoryMock = fixture.Freeze<Mock<IWriteOffFactRepository>>();
        repositoryMock.Setup(repo => repo.GetAllAsync(CancellationToken.None, false))
            .ReturnsAsync(writeOffFacts);
        repositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(writeOffFact);

        var writeOffFactDto = fixture.Freeze<WriteOffFactDto>();
        var registerWriteOffFactDto = fixture.Freeze<RegisterWriteOffFactDto>();

        var mapperMock = fixture.Freeze<Mock<IMapper>>();
        mapperMock.Setup(m => m.Map<WriteOffFactDto>(It.IsAny<WriteOffFact>()))
            .Returns(writeOffFactDto);
        mapperMock.Setup(m => m.Map<WriteOffFact>(registerWriteOffFactDto))
            .Returns(writeOffFact);

        var service = fixture.Freeze<Implementations.WriteOffFactService>();

        return new WriteOffFactServiceTestsModel
        {
            Service = service,
            RepositoryMock = repositoryMock,
            MapperMock = mapperMock,
            WriteOffFacts = writeOffFacts,
            WriteOffFact = writeOffFact,
            WriteOffFactDto = writeOffFactDto,
            RegisterWriteOffFactDto = registerWriteOffFactDto
        };
    }
}