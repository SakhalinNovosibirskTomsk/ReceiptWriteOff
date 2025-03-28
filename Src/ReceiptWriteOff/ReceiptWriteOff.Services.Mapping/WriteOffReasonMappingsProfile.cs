using AutoMapper;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Contracts.WriteOffFact;
using ReceiptWriteOff.Services.Contracts.WriteOffReason;

namespace ReceiptWriteOff.Services.Mapping;

public class WriteOffReasonMappingsProfile : Profile
{
    protected WriteOffReasonMappingsProfile()
    {
        CreateMap<WriteOffReason, WriteOffReasonDto>();
        CreateMap<CreateOrEditWriteOffReasonDto, WriteOffReason>()
            .ForMember(writeOffReason => writeOffReason.Id, map => map.Ignore())
            .ForMember(writeOffReason => writeOffReason.WriteOffFacts, map => map.MapFrom(
                dto => new List<WriteOffFactDto>()));
    }
}