using AutoMapper;
using Moq;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Application.Tests.WriteOffFactService.Model;

public class WriteOffFactServiceTestsModel
{
    public required Implementations.WriteOffFactService Service { get; set; }
    public required Mock<IWriteOffFactRepository> WriteOffFactRepositoryMock { get; set; }
    public required Mock<IBookInstanceRepository> BookInstanceRepositoryMock { get; set; }
    public required Mock<IWriteOffReasonRepository> WriteOffReasonRepositoryMock { get; set; }
    public required Mock<IWriteOffFactUnitOfWork> WriteOffFactUnitOfWorkMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<WriteOffFact> WriteOffFacts { get; set; }
    public required WriteOffFact WriteOffFact { get; set; }
    public required WriteOffFactDto WriteOffFactDto { get; set; }
    public required RegisterWriteOffFactDto RegisterWriteOffFactDto { get; set; }
    public required ReceiptFact ReceiptFact { get; set; }
}