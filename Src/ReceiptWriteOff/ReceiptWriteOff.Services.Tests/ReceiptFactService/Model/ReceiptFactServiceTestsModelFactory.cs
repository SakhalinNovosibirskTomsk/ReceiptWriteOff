using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Services.Contracts.BookInstance;
using ReceiptWriteOff.Services.Contracts.ReceiptFact;

namespace ReceiptWriteOff.Services.Tests.ReceiptFactService.Model;

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
        var bookInstance = fixture.Freeze<BookInstanceShortDto>();

        var repositoryMock = fixture.Freeze<Mock<IReceiptFactRepository>>();
        repositoryMock.Setup(repo => repo.GetAllAsync(CancellationToken.None, false))
            .ReturnsAsync(receiptFacts);
        repositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(receiptFact);
        repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ReceiptFact>(), CancellationToken.None))
            .Returns(Task.CompletedTask);
        repositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>(), CancellationToken.None))
            .Returns(Task.CompletedTask);
        repositoryMock.Setup(repo => repo.SaveChangesAsync(CancellationToken.None))
            .Returns(Task.CompletedTask);

        var receiptFactShortDto = new ReceiptFactShortDto();
        var receiptFactDto = new ReceiptFactDto
        {
            BookInstance = bookInstance
        };
        var registerReceiptFactDto = new RegisterReceiptFactDto();

        var mapperMock = fixture.Freeze<Mock<IMapper>>();
        mapperMock.Setup(m => m.Map<ReceiptFactShortDto>(It.IsAny<ReceiptFact>()))
            .Returns(receiptFactShortDto);
        mapperMock.Setup(m => m.Map<ReceiptFactDto>(It.IsAny<ReceiptFact>()))
            .Returns(receiptFactDto);
        mapperMock.Setup(m => m.Map<ReceiptFact>(registerReceiptFactDto))
            .Returns(receiptFact);

        var service = new Implementations.ReceiptFactService(repositoryMock.Object, mapperMock.Object);

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