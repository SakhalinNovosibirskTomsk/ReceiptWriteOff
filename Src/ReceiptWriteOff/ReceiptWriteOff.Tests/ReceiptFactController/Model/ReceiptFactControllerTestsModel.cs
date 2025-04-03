using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Contracts.ReceiptFact;

namespace ReceiptWriteOff.Tests.ReceiptFactController.Model;

public class ReceiptFactControllerTestsModel
{
    public required Controllers.ReceiptFactController Controller { get; set; }
    public required Mock<IReceiptFactService> ServiceMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<ReceiptFactShortDto> ReceiptFacts { get; set; }
    public required ReceiptFactDto ReceiptFact { get; set; }
    public required ReceiptFactResponse ReceiptFactResponse { get; set; }
    public required RegisterReceiptFactRequest RegisterReceiptFactRequest { get; set; }
}