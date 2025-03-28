using AutoMapper;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Contracts.WriteOffFact;

namespace ReceiptWriteOff.Services.Mapping;

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