using Microsoft.Extensions.DependencyInjection;
using ReceiptWriteOff.Application.Abstractions;

namespace ReceiptWriteOff.Application.Implementations;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>()
            .AddScoped<IBookInstanceService, BookInstanceService>()
            .AddScoped<IReceiptFactService, ReceiptFactService>()
            .AddScoped<IWriteOffFactService, WriteOffFactService>()
            .AddScoped<IWriteOffReasonService, WriteOffReasonService>();

        return services;
    }
}