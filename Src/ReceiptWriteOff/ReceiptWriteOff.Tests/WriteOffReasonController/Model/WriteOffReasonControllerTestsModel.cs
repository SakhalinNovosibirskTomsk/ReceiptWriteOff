using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Abstractions;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Contracts.WriteOffReason;

namespace ReceiptWriteOff.Tests.WriteOffReasonController.Model;

public class WriteOffReasonControllerTestsModel
{
    public required Controllers.WriteOffReasonController Controller { get; set; }
    public required Mock<IWriteOffReasonService> ServiceMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<WriteOffReasonDto> WriteOffReasons { get; set; }
    public required WriteOffReasonDto WriteOffReason { get; set; }
    public required WriteOffReasonResponse WriteOffReasonResponse { get; set; }
    public required CreateOrEditWriteOffReasonRequest CreateOrEditWriteOffReasonRequest { get; set; }
}