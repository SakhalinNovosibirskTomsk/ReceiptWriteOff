using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Infrastructure.Repositories.Implementation;
using ReceiptWriteOff.Services.Contracts.Book;
using ReceiptWriteOff.Services.Contracts.BookInstance;
using ReceiptWriteOff.Services.Implementations;

namespace ReceiptWriteOff.Services.Tests.BookInstanceService.Model;
    
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
            repositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            var bookInstanceShortDto = new BookInstanceShortDto();
            var bookInstanceDto = new BookInstanceDto
            {
                Book = new BookDto()
                {
                    Author = "test_author",
                    Title = "test_title"
                }
            };
            
            var mapperMock = fixture.Freeze<Mock<IMapper>>();
            mapperMock.Setup(m => m.Map<BookInstanceShortDto>(It.IsAny<BookInstance>()))
                .Returns(bookInstanceShortDto);
            mapperMock.Setup(m => m.Map<BookInstanceDto>(It.IsAny<BookInstance>()))
                .Returns(bookInstanceDto);
    
            var service = new Implementations.BookInstanceService(repositoryMock.Object, mapperMock.Object);
    
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