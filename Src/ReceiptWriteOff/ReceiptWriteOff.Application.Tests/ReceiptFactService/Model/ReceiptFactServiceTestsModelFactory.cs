using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Application.Tests.ReceiptFactService.Model;

public static class ReceiptFactServiceTestsModelFactory
{
    public static ReceiptFactServiceTestsModel Create(int receiptFactsCount = 0)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var receiptFacts = fixture.CreateMany<ReceiptFact>(receiptFactsCount).ToList();
        var receiptFact = fixture.Freeze<ReceiptFact>();

        var repositoryMock = fixture.Freeze<Mock<IReceiptFactRepository>>();
        repositoryMock.Setup(repo => repo.GetAllAsync(CancellationToken.None, false))
            .ReturnsAsync(receiptFacts);
        repositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(receiptFact);

        var receiptFactShortDto = fixture.Freeze<ReceiptFactShortDto>();
        var receiptFactDto = fixture.Freeze<ReceiptFactDto>();
        var registerReceiptFactDto = fixture.Freeze<RegisterReceiptFactDto>();
        
        var mapperMock = fixture.Freeze<Mock<IMapper>>();
        mapperMock.Setup(m => m.Map<ReceiptFactShortDto>(It.IsAny<ReceiptFact>()))
            .Returns(receiptFactShortDto);
        mapperMock.Setup(m => m.Map<ReceiptFactDto>(It.IsAny<ReceiptFact>()))
            .Returns(receiptFactDto);
        mapperMock.Setup(m => m.Map<ReceiptFact>(registerReceiptFactDto))
            .Returns(receiptFact);

        var service = fixture.Freeze<Implementations.ReceiptFactService>();
        
        return new ReceiptFactServiceTestsModel
        {
            Service = service,
            RepositoryMock = repositoryMock,
            MapperMock = mapperMock,
            ReceiptFacts = receiptFacts,
            ReceiptFact = receiptFact,
            ReceiptFactShortDto = receiptFactShortDto,
            ReceiptFactDto = receiptFactDto,
            RegisterReceiptFactDto = registerReceiptFactDto
        };
    }
}