using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Contracts.WriteOffFact;

namespace ReceiptWriteOff.Tests.WriteOffFactController.Model;

public class WriteOffFactControllerTestsModel
{
    public required Controllers.WriteOffFactController Controller { get; set; }
    public required Mock<IWriteOffFactService> ServiceMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<WriteOffFactShortResponse> WriteOffFacts { get; set; }
    public required WriteOffFactResponse WriteOffFactResponse { get; set; }
    public required RegisterWriteOffFactRequest RegisterWriteOffFactRequest { get; set; }
}