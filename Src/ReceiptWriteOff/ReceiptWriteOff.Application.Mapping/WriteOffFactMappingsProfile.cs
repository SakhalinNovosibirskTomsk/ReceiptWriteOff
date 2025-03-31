using AutoMapper;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Mapping;

public class WriteOffFactMappingsProfile : Profile
{
    public WriteOffFactMappingsProfile()
    {
        CreateMap<WriteOffFact, WriteOffFactDto>();
        CreateMap<WriteOffFact, WriteOffFactShortDto>();
        CreateMap<RegisterWriteOffFactDto, WriteOffFact>()
            .ForMember(receiptFact => receiptFact.Id, map => map.Ignore())
            .ForMember(receiptFact => receiptFact.BookInstance, map => map.Ignore())
            .ForMember(receiptFact => receiptFact.BookInstanceId, map => map.Ignore())
            .ForMember(receiptFact => receiptFact.WriteOffReason, map => map.Ignore())
            .ForMember(receiptFact => receiptFact.WriteOffReasonId, map => map.Ignore());
    }
}