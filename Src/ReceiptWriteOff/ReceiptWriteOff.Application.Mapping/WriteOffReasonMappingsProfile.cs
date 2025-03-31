using AutoMapper;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Mapping;

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