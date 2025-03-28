using AutoMapper;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Contracts.BookInstance;

namespace ReceiptWriteOff.Services.Mapping;

public class BookInstanceMappingsProfile : Profile
{
    public BookInstanceMappingsProfile()
    {
        CreateMap<BookInstance, BookInstanceDto>();
        CreateMap<BookInstance, BookInstanceShortDto>();
    }
}