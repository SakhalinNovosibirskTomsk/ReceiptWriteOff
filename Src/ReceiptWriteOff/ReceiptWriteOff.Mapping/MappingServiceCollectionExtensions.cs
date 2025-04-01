using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ReceiptWriteOff.Mapping;

public static class MappingServiceCollectionExtensions
{
    public static void AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(ConfigureMapper);
    }

    private static void ConfigureMapper(IMapperConfigurationExpression config)
    {
        config.AllowNullCollections = true;
        config.AddGlobalIgnore("Item");
                
        config.AddProfile<BookInstanceMappingsProfile>();
        config.AddProfile<BookMappingsProfile>();
        config.AddProfile<ReceiptFactMappingsProfile>();
        config.AddProfile<WriteOffFactMappingsProfile>();
        config.AddProfile<WriteOffReasonMappingsProfile>();
            
        config.AddProfile<ReceiptWriteOff.Application.Mapping.BookInstanceMappingsProfile>();
        config.AddProfile<ReceiptWriteOff.Application.Mapping.BookMappingsProfile>();
        config.AddProfile<ReceiptWriteOff.Application.Mapping.ReceiptFactMappingsProfile>();
        config.AddProfile<ReceiptWriteOff.Application.Mapping.WriteOffFactMappingsProfile>();
        config.AddProfile<ReceiptWriteOff.Application.Mapping.WriteOffReasonMappingsProfile>();
    }
    
}