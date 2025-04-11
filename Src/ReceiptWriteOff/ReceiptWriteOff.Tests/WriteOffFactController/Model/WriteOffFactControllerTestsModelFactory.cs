using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Application.Implementations.Exceptions;
using ReceiptWriteOff.Contracts.WriteOffFact;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;

namespace ReceiptWriteOff.Tests.WriteOffFactController.Model;

public static class WriteOffFactControllerTestsModelFactory
{
    public static WriteOffFactControllerTestsModel Create(
        int writeOffFactsCount = 0,
        bool writeOffFactExists = true,
        bool alreadyExistsOnRegister = false,
        bool entityNotFoundOnRegister = false,
        bool invalidDateOnRegister = false)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var writeOffFacts = fixture.CreateMany<WriteOffFactShortResponse>(writeOffFactsCount).ToList();
        var writeOffFactResponse = fixture.Freeze<WriteOffFactResponse>();
        var registerWriteOffFactRequest = fixture.Freeze<RegisterWriteOffFactRequest>();

        var serviceMock = fixture.Freeze<Mock<IWriteOffFactService>>();
        serviceMock.Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(fixture.CreateMany<WriteOffFactShortDto>());

        if (alreadyExistsOnRegister)
        {
            serviceMock.Setup(service =>
                    service.RegisterAsync(It.IsAny<RegisterWriteOffFactDto>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new AlreadyExistsException("WriteOffFact already exists"));
        }
        else if (entityNotFoundOnRegister)
        {
            serviceMock.Setup(service =>
                    service.RegisterAsync(It.IsAny<RegisterWriteOffFactDto>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new EntityNotFoundException("Entity not found"));
        }
        else if (invalidDateOnRegister)
        {
            serviceMock.Setup(service =>
                    service.RegisterAsync(It.IsAny<RegisterWriteOffFactDto>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DateException("Invalid date"));
        }
        else
        {
            serviceMock.Setup(service =>
                    service.RegisterAsync(It.IsAny<RegisterWriteOffFactDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(fixture.Create<WriteOffFactDto>());
        }

        var entityNotFoundException = fixture.Freeze<EntityNotFoundException>();

        if (writeOffFactExists)
        {
            serviceMock.Setup(service => service.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(fixture.Create<WriteOffFactDto>());
        }
        else
        {
            serviceMock.Setup(service => service.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
            serviceMock.Setup(service => service.EditAsync(It.IsAny<int>(), It.IsAny<RegisterWriteOffFactDto>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
            serviceMock.Setup(service => service.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
            serviceMock.Setup(service =>
                    service.RegisterAsync(It.IsAny<RegisterWriteOffFactDto>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
        }

        var mapperMock = fixture.Freeze<Mock<IMapper>>();
        mapperMock.Setup(m => m.Map<WriteOffFactResponse>(It.IsAny<WriteOffFactDto>()))
            .Returns(writeOffFactResponse);

        var controller = fixture.Build<Controllers.WriteOffFactController>()
            .OmitAutoProperties()
            .Create();

        return new WriteOffFactControllerTestsModel
        {
            Controller = controller,
            ServiceMock = serviceMock,
            MapperMock = mapperMock,
            WriteOffFacts = writeOffFacts,
            WriteOffFactResponse = writeOffFactResponse,
            RegisterWriteOffFactRequest = registerWriteOffFactRequest
        };
    }
}