using AutoMapper;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Services.Contracts.WriteOffFact;

namespace ReceiptWriteOff.Services.Tests.WriteOffFactService.Model;

public class WriteOffFactServiceTestsModel
{
    public required Implementations.WriteOffFactService Service { get; set; }
    public required Mock<IWriteOffFactRepository> RepositoryMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<WriteOffFact> WriteOffFacts { get; set; }
    public required WriteOffFact WriteOffFact { get; set; }
    public required WriteOffFactDto WriteOffFactDto { get; set; }
    public required RegisterWriteOffFactDto RegisterWriteOffFactDto { get; set; }
}