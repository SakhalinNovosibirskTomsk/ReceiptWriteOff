using AutoMapper;
using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Mapping;

public class BookInstanceMappingsProfile : Profile
{
    public BookInstanceMappingsProfile()
    {
        CreateMap<BookInstance, BookInstanceDto>();
        CreateMap<BookInstance, BookInstanceShortDto>();
    }
}