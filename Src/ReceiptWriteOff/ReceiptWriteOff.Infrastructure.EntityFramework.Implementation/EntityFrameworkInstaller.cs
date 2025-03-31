using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ReceiptWriteOff.Infrastructure.EntityFramework
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
                options.UseNpgsql(connectionString);
            });
            return services;
        }
    }
}