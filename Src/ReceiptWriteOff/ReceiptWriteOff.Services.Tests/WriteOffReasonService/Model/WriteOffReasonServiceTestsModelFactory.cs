using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Services.Contracts.WriteOffReason;

namespace ReceiptWriteOff.Services.Tests.WriteOffReasonService.Model;

public static class WriteOffReasonServiceTestsModelFactory
{
    public static WriteOffReasonServiceTestsModel Create(int writeOffReasonsCount = 0)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var writeOffReasons = fixture.CreateMany<WriteOffReason>(writeOffReasonsCount).ToList();
        var writeOffReason = fixture.Freeze<WriteOffReason>();

        var repositoryMock = fixture.Freeze<Mock<IWriteOffReasonRepository>>();
        repositoryMock.Setup(repo => repo.GetAllAsync(CancellationToken.None, false))
            .ReturnsAsync(writeOffReasons);
        repositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(writeOffReason);

        var writeOffReasonDto = fixture.Freeze<WriteOffReasonDto>();
        var createOrEditWriteOffReasonDto = fixture.Freeze<CreateOrEditWriteOffReasonDto>();

        var mapperMock = fixture.Freeze<Mock<IMapper>>();
        mapperMock.Setup(m => m.Map<WriteOffReasonDto>(It.IsAny<WriteOffReason>()))
            .Returns(writeOffReasonDto);
        mapperMock.Setup(m => m.Map(createOrEditWriteOffReasonDto, writeOffReason))
            .Returns(writeOffReason);
        mapperMock.Setup(m => m.Map<WriteOffReason>(createOrEditWriteOffReasonDto))
            .Returns(writeOffReason);

        var service = fixture.Freeze<Implementations.WriteOffReasonService>();
        
        return new WriteOffReasonServiceTestsModel
        {
            Service = service,
            RepositoryMock = repositoryMock,
            MapperMock = mapperMock,
            WriteOffReasons = writeOffReasons,
            WriteOffReason = writeOffReason,
            WriteOffReasonDto = writeOffReasonDto,
            CreateOrEditWriteOffReasonDto = createOrEditWriteOffReasonDto
        };
    }
}