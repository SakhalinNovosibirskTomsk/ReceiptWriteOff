using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Infrastructure.Repositories.Implementation;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Application.Implementations;

namespace ReceiptWriteOff.Application.Tests.BookInstanceService.Model;
    
    public static class BookInstanceServiceTestsModelFactory
    {
        public static BookInstanceServiceTestsModel Create(int bookInstancesCount = 0)
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    
            var bookInstances = fixture.CreateMany<BookInstance>(bookInstancesCount).ToList();
            var bookInstance = fixture.Freeze<BookInstance>();
    
            var repositoryMock = fixture.Freeze<Mock<IBookInstanceRepository>>();
            repositoryMock.Setup(repo => repo.GetAllAsync(CancellationToken.None, false))
                .ReturnsAsync(bookInstances);
            repositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>(), CancellationToken.None))
                .ReturnsAsync(bookInstance);

            var bookInstanceShortDto = fixture.Freeze<BookInstanceShortDto>();
            var bookInstanceDto = fixture.Freeze<BookInstanceDto>();
            
            var mapperMock = fixture.Freeze<Mock<IMapper>>();
            mapperMock.Setup(m => m.Map<BookInstanceShortDto>(It.IsAny<BookInstance>()))
                .Returns(bookInstanceShortDto);
            mapperMock.Setup(m => m.Map<BookInstanceDto>(It.IsAny<BookInstance>()))
                .Returns(bookInstanceDto);
    
            var service = fixture.Freeze<Implementations.BookInstanceService>();
            
            return new BookInstanceServiceTestsModel
            {
                Service = service,
                RepositoryMock = repositoryMock,
                MapperMock = mapperMock,
                BookInstances = bookInstances,
                BookInstance = bookInstance,
                BookInstanceShortDto = bookInstanceShortDto,
                BookInstanceDto = bookInstanceDto
            };
        }
    }