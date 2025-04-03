using AutoMapper;
using ReceiptWriteOff.Application.Contracts.WriteOffFact;
using ReceiptWriteOff.Contracts.WriteOffFact;

namespace ReceiptWriteOff.Mapping;

public class WriteOffFactMappingsProfile : Profile
{
    public WriteOffFactMappingsProfile()
    {
        CreateMap<WriteOffFactShortDto, WriteOffFactShortResponse>();
        CreateMap<WriteOffFactDto, WriteOffFactResponse>();
        CreateMap<RegisterWriteOffFactRequest, RegisterWriteOffFactDto>();
    }
}