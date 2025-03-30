using AutoMapper;
using Moq;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;
using ReceiptWriteOff.Services.Contracts.ReceiptFact;
using ReceiptWriteOff.Services.Implementations;

namespace ReceiptWriteOff.Services.Tests.ReceiptFactService.Model;

public class ReceiptFactServiceTestsModel
{
    public required Implementations.ReceiptFactService Service { get; set; }
    public required Mock<IReceiptFactRepository> RepositoryMock { get; set; }
    public required Mock<IMapper> MapperMock { get; set; }
    public required List<ReceiptFact> ReceiptFacts { get; set; }
    public required ReceiptFact ReceiptFact { get; set; }
    public required ReceiptFactShortDto ReceiptFactShortDto { get; set; }
    public required ReceiptFactDto ReceiptFactDto { get; set; }
    public required RegisterReceiptFactDto RegisterReceiptFactDto { get; set; }
}