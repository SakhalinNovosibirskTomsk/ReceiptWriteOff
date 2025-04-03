using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Contracts.WriteOffReason;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;

namespace ReceiptWriteOff.Tests.WriteOffReasonController.Model;

public static class WriteOffReasonControllerTestsModelFactory
{
    public static WriteOffReasonControllerTestsModel Create(
        int writeOffReasonsCount = 0,
        bool writeOffReasonExists = true)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var writeOffReasons = fixture.CreateMany<WriteOffReasonDto>(writeOffReasonsCount).ToList();
        var writeOffReason = fixture.Freeze<WriteOffReasonDto>();
        var createOrEditWriteOffReasonRequest = fixture.Freeze<CreateOrEditWriteOffReasonRequest>();

        var serviceMock = fixture.Freeze<Mock<IWriteOffReasonService>>();
        serviceMock.Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(writeOffReasons);
        serviceMock.Setup(service =>
                service.CreateAsync(It.IsAny<CreateOrEditWriteOffReasonDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(writeOffReason);

        var entityNotFoundException = fixture.Freeze<EntityNotFoundException>();

        if (writeOffReasonExists)
        {
            serviceMock.Setup(service => service.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(writeOffReason);
        }
        else
        {
            serviceMock.Setup(service => service.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
            serviceMock.Setup(service => service.EditAsync(It.IsAny<int>(), It.IsAny<CreateOrEditWriteOffReasonDto>(),It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
            serviceMock.Setup(service => service.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
        }

        var writeOffReasonResponse = fixture.Freeze<WriteOffReasonResponse>();

        var mapperMock = fixture.Freeze<Mock<IMapper>>();
        mapperMock.Setup(m => m.Map<WriteOffReasonResponse>(It.IsAny<WriteOffReasonDto>()))
            .Returns(writeOffReasonResponse);

        var controller = fixture.Build<Controllers.WriteOffReasonController>()
            .OmitAutoProperties()
            .Create();

        return new WriteOffReasonControllerTestsModel
        {
            Controller = controller,
            ServiceMock = serviceMock,
            MapperMock = mapperMock,
            WriteOffReasons = writeOffReasons,
            WriteOffReason = writeOffReason,
            WriteOffReasonResponse = writeOffReasonResponse,
            CreateOrEditWriteOffReasonRequest = createOrEditWriteOffReasonRequest
        };
    }
}