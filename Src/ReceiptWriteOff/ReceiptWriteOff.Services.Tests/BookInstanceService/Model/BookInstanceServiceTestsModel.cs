using AutoMapper;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Services.Contracts.BookInstance;
using ReceiptWriteOff.Services.Implementations;

namespace ReceiptWriteOff.Services.Tests.BookInstanceService.Model;

public class BookInstanceServiceTestsModel
{
    public required Implementations.BookInstanceService Service { get; set; }
    public required Mock<IBookInstanceRepository> RepositoryMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<BookInstance> BookInstances { get; set; }
    public required BookInstance BookInstance { get; set; }
    public required BookInstanceShortDto BookInstanceShortDto { get; set; }
    public required BookInstanceDto BookInstanceDto { get; set; }
}