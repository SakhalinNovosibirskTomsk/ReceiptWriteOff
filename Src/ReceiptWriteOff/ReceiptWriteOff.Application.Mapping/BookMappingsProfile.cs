using AutoMapper;
using ReceiptWriteOff.Application.Contracts.Book;
using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Application.Mapping;

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