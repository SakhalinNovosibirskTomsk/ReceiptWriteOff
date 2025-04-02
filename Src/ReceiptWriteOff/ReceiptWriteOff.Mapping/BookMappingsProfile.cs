using AutoMapper;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Contracts.Book;

namespace ReceiptWriteOff.Mapping;

public class BookMappingsProfile : Profile
{
    public BookMappingsProfile()
    {
        CreateMap<BookDto, BookResponse>();
        CreateMap<CreateOrEditBookRequest, CreateOrEditBookDto>();
    }
}