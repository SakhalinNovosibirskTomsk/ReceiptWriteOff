using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

public interface IDbSet<TEntity> : IQueryable<TEntity> where TEntity : class
{
    ValueTask<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken);
    ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    IQueryable<TEntity> AsNoTracking();
    EntityEntry<TEntity> Remove(TEntity entity);
    //IQueryable<TEntity> GetQueryable(bool asNoTracking = false);
}