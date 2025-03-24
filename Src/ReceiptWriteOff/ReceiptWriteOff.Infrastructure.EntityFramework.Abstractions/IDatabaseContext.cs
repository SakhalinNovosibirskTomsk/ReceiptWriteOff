using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

public interface IDatabaseContext
{
    DdSetDecorator<TEntity> Set<TEntity>() where TEntity : class;
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}