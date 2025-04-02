using AutoMapper;
using ReceiptWriteOff.Application.Contracts.ReceiptFact;
using ReceiptWriteOff.Contracts.ReceiptFact;

namespace ReceiptWriteOff.Mapping;

public class ReceiptFactMappingsProfile : Profile
{
    public ReceiptFactMappingsProfile()
    {
        CreateMap<ReceiptFactShortDto, ReceiptFactShortResponse>();
        CreateMap<ReceiptFactDto, ReceiptFactResponse>();
        CreateMap<RegisterReceiptFactRequest, RegisterReceiptFactDto>();
    }
}