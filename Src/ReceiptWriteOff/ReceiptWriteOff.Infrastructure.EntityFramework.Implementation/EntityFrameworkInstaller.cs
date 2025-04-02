using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Implementation
{
    public static class EntityFrameworkServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseContext(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseNpgsql(
                    connectionString,
                    builder => builder.MigrationsAssembly("ReceiptWriteOff.Infrastructure.EntityFramework.Migration"));
            });
            services.AddScoped<IDatabaseContext>(provider => provider.GetService<DatabaseContext>()!);
            
            return services;
        }
    }
}