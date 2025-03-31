using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Application.Tests.WriteOffReasonService.Model;

public class WriteOffReasonServiceTestsModel
{
    public required Implementations.WriteOffReasonService Service { get; set; }
    public required Mock<IWriteOffReasonRepository> RepositoryMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<WriteOffReason> WriteOffReasons { get; set; }
    public required WriteOffReason WriteOffReason { get; set; }
    public required WriteOffReasonDto WriteOffReasonDto { get; set; }
    public required CreateOrEditWriteOffReasonDto CreateOrEditWriteOffReasonDto { get; set; }
}