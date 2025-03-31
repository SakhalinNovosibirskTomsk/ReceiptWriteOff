using Microsoft.Extensions.DependencyInjection;
using ReceiptWriteOff.Domain.Entities;
using ReceiptWriteOff.Infrastructure.EntityFramework;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Implementation;

public static class RepositoriesServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IQueryableExtensionsWrapper<BookInstance>, QueryableExtensionsWrapper<BookInstance>>()
            .AddTransient<IQueryableExtensionsWrapper<Book>, QueryableExtensionsWrapper<Book>>()
            .AddTransient<IQueryableExtensionsWrapper<ReceiptFact>, QueryableExtensionsWrapper<ReceiptFact>>()
            .AddTransient<IQueryableExtensionsWrapper<WriteOffFact>, QueryableExtensionsWrapper<WriteOffFact>>()
            .AddTransient<IQueryableExtensionsWrapper<WriteOffReason>, QueryableExtensionsWrapper<WriteOffReason>>()
        
            .AddTransient<IBookInstanceRepository, BookInstanceRepository>()
            .AddTransient<IBookUnitOfWork, BookUnitOfWork>()
            .AddTransient<IReceiptFactRepository, ReceiptFactRepository>()
            .AddTransient<IWriteOffFactRepository, WriteOffFactRepository>()
            .AddTransient<IWriteOffReasonRepository, WriteOffReasonRepository>();

        return services;
    }
}