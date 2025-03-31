using Microsoft.Extensions.DependencyInjection;
using ReceiptWriteOff.Services.Abstractions;

namespace ReceiptWriteOff.Services.Implementations;

public static class ServicesServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IBookService, BookService>()
            .AddTransient<IBookInstanceService, BookInstanceService>()
            .AddTransient<IReceiptFactService, ReceiptFactService>()
            .AddTransient<IWriteOffFactService, WriteOffFactService>()
            .AddTransient<IWriteOffReasonService, WriteOffReasonService>();

        return services;
    }
}