using AutoMapper;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Services.Contracts.Book;

namespace ReceiptWriteOff.Services.Mapping;

public class BookMappingsProfile : Profile
{
    public BookMappingsProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateOrEditBookDto, Book>()
            .ForMember(book => book.Id, map => map.Ignore())
            .ForMember(book => book.BookInstances, map => map.MapFrom(
                dto => new List<BookInstance>()));
    }
}