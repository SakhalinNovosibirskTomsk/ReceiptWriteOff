using AutoMapper;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;
using ReceiptWriteOff.Contracts.WriteOffReason;

namespace ReceiptWriteOff.Mapping;

public class WriteOffReasonMappingsProfile : Profile
{
    public WriteOffReasonMappingsProfile()
    {
        CreateMap<WriteOffReasonDto, WriteOffReasonResponse>();
        CreateMap<CreateOrEditWriteOffReasonRequest, CreateOrEditWriteOffReasonDto>();
    }
}