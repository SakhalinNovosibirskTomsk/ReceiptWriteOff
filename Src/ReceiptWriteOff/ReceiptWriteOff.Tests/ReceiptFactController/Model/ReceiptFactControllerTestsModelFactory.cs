using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Contracts.ReceiptFact;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;

namespace ReceiptWriteOff.Tests.ReceiptFactController.Model;

public static class ReceiptFactControllerTestsModelFactory
{
    public static ReceiptFactControllerTestsModel Create(
        int receiptFactsCount = 0,
        bool receiptFactExists = true)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var receiptFacts = fixture.CreateMany<ReceiptFactShortDto>(receiptFactsCount).ToList();
        var receiptFact = fixture.Freeze<ReceiptFactDto>();
        var registerReceiptFactRequest = fixture.Freeze<RegisterReceiptFactRequest>();

        var serviceMock = fixture.Freeze<Mock<IReceiptFactService>>();
        serviceMock.Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(receiptFacts);
        serviceMock.Setup(service =>
                service.RegisterAsync(It.IsAny<RegisterReceiptFactDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(receiptFact);

        var entityNotFoundException = fixture.Freeze<EntityNotFoundException>();

        if (receiptFactExists)
        {
            serviceMock.Setup(service => service.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(receiptFact);
        }
        else
        {
            serviceMock.Setup(service => service.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
            serviceMock.Setup(service => service.EditAsync(It.IsAny<int>(), It.IsAny<RegisterReceiptFactDto>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
            serviceMock.Setup(service => service.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
        }

        var receiptFactResponse = fixture.Freeze<ReceiptFactResponse>();

        var mapperMock = fixture.Freeze<Mock<IMapper>>();
        mapperMock.Setup(m => m.Map<ReceiptFactResponse>(It.IsAny<ReceiptFactDto>()))
            .Returns(receiptFactResponse);

        var controller = fixture.Build<Controllers.ReceiptFactController>()
            .OmitAutoProperties()
            .Create();

        return new ReceiptFactControllerTestsModel
        {
            Controller = controller,
            ServiceMock = serviceMock,
            MapperMock = mapperMock,
            ReceiptFacts = receiptFacts,
            ReceiptFact = receiptFact,
            ReceiptFactResponse = receiptFactResponse,
            RegisterReceiptFactRequest = registerReceiptFactRequest
        };
    }
}