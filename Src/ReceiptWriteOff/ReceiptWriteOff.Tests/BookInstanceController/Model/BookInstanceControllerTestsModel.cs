using AutoMapper;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Contracts.BookInstance;

namespace ReceiptWriteOff.Tests.BookInstanceController.Model;

public class BookInstanceControllerTestsModel
{
    public required Controllers.BookInstanceController Controller { get; set; }
    public required Mock<IBookInstanceService> ServiceMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<BookInstanceShortDto> BookInstances { get; set; }
    public required BookInstanceDto BookInstance { get; set; }
    public required BookInstanceResponse BookInstanceResponse { get; set; }
    public required BookInstanceShortResponse BookInstanceShortResponse { get; set; }
}