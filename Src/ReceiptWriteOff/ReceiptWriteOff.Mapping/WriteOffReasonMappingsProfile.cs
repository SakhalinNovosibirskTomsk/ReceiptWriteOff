using AutoMapper;

namespace ReceiptWriteOff.Mapping;

public class WriteOffReasonMappingsProfile : Profile
{
    public WriteOffReasonMappingsProfile()
    {
        // CreateMap<WriteOffReason, WriteOffReasonDto>();
        // CreateMap<CreateOrEditWriteOffReasonDto, WriteOffReason>()
        //     .ForMember(writeOffReason => writeOffReason.Id, map => map.Ignore())
        //     .ForMember(writeOffReason => writeOffReason.WriteOffFacts, map => map.MapFrom(
        //         dto => new List<WriteOffFactDto>()));
    }
}