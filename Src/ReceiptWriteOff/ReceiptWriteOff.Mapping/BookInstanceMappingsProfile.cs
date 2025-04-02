using AutoMapper;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Contracts.BookInstance;

namespace ReceiptWriteOff.Mapping;

public class BookInstanceMappingsProfile : Profile
{
    public BookInstanceMappingsProfile()
    {
        CreateMap<BookInstanceDto, BookInstanceResponse>();
        CreateMap<BookInstanceShortDto, BookInstanceShortResponse>();
    }
}