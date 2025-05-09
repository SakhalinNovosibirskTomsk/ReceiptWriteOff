using Microsoft.Extensions.DependencyInjection;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public static class RepositoriesServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IQueryableExtensionsWrapper<BookInstance>, QueryableExtensionsWrapper<BookInstance>>()
            .AddScoped<IQueryableExtensionsWrapper<Book>, QueryableExtensionsWrapper<Book>>()
            .AddScoped<IQueryableExtensionsWrapper<ReceiptFact>, QueryableExtensionsWrapper<ReceiptFact>>()
            .AddScoped<IQueryableExtensionsWrapper<WriteOffFact>, QueryableExtensionsWrapper<WriteOffFact>>()
            .AddScoped<IQueryableExtensionsWrapper<WriteOffReason>, QueryableExtensionsWrapper<WriteOffReason>>()
        
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IBookInstanceRepository, BookInstanceRepository>()
            .AddScoped<IReceiptFactRepository, ReceiptFactRepository>()
            .AddScoped<IWriteOffFactUnitOfWork, WriteOffFactUnitOfWork>()
            .AddScoped<IWriteOffReasonRepository, WriteOffReasonRepository>();

        return services;
    }
}