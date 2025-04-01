using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Models.BookInstance;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;

namespace ReceiptWriteOff.Tests.BookInstanceController.Model;

public static class BookInstanceControllerTestsModelFactory
{
    public static BookInstanceControllerTestsModel Create(
        int bookInstancesCount = 0,
        bool bookInstanceExists = true)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var bookInstances = fixture.CreateMany<BookInstanceShortDto>(bookInstancesCount).ToList();
        var bookInstance = fixture.Freeze<BookInstanceDto>();

        var serviceMock = fixture.Freeze<Mock<IBookInstanceService>>();
        serviceMock.Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookInstances);

        var entityNotFoundException = fixture.Freeze<EntityNotFoundException>();
        
        if (bookInstanceExists)
        {
            serviceMock.Setup(service => service.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookInstance);
        }
        else
        {
            serviceMock.Setup(service => service.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
            serviceMock.Setup(service => service.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(entityNotFoundException);
        }

        var bookInstanceResponse = fixture.Freeze<BookInstanceResponse>();
        var bookInstanceShortResponse = fixture.Freeze<BookInstanceShortResponse>();

        var mapperMock = fixture.Freeze<Mock<IMapper>>();
        mapperMock.Setup(m => m.Map<BookInstanceResponse>(It.IsAny<BookInstanceDto>()))
            .Returns(bookInstanceResponse);
        mapperMock.Setup(m => m.Map<BookInstanceShortResponse>(It.IsAny<BookInstanceShortDto>()))
            .Returns(bookInstanceShortResponse);

        var controller = fixture.Build<Controllers.BookInstanceController>()
            .OmitAutoProperties()
            .Create();

        return new BookInstanceControllerTestsModel
        {
            Controller = controller,
            ServiceMock = serviceMock,
            MapperMock = mapperMock,
            BookInstances = bookInstances,
            BookInstance = bookInstance,
            BookInstanceResponse = bookInstanceResponse,
            BookInstanceShortResponse = bookInstanceShortResponse
        };
    }
}