using AutoMapper;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Mapping;

public class ReceiptFactMappingsProfile : Profile
{
    public ReceiptFactMappingsProfile()
    {
        CreateMap<ReceiptFact, ReceiptFactDto>();
        CreateMap<ReceiptFact, ReceiptFactShortDto>();
        CreateMap<RegisterReceiptFactDto, ReceiptFact>()
            .ForMember(receiptFact => receiptFact.Id, map => map.Ignore())
            .ForMember(receiptFact => receiptFact.BookInstance, map => map.Ignore())
            .ForMember(receiptFact => receiptFact.BookInstanceId, map => map.Ignore());
        CreateMap<RegisterReceiptFactDto, BookInstance>()
            .ForMember(receiptFact => receiptFact.Id, map => map.Ignore())
            .ForMember(receiptFact => receiptFact.ReceiptFact, map => map.Ignore())
            .ForMember(receiptFact => receiptFact.WriteOffFact, map => map.Ignore())
            .ForMember(receiptFact => receiptFact.Book, map => map.Ignore());
    }
}