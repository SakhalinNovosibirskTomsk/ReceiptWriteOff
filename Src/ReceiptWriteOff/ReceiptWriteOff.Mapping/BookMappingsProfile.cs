using AutoMapper;

namespace ReceiptWriteOff.Mapping;

public class BookMappingsProfile : Profile
{
    public BookMappingsProfile()
    {
        // CreateMap<BookDto, BookDto>();
        // CreateMap<CreateOrEditBookDto, Book>()
        //     .ForMember(book => book.Id, map => map.Ignore())
        //     .ForMember(book => book.BookInstances, map => map.MapFrom(
        //         dto => new List<BookInstance>()));
    }
}