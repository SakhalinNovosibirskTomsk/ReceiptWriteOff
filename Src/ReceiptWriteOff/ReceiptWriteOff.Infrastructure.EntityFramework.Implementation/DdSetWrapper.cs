using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;

// ReSharper disable once InconsistentNaming
public class DdSetWrapper<TEntity>(DbSet<TEntity> _dbSet) : IDbSet<TEntity>
    where TEntity : class
{
    public ValueTask<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken) => _dbSet.FindAsync(keyValues, cancellationToken);
    public ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default) => _dbSet.AddAsync(entity, cancellationToken);
    public IQueryable<TEntity> AsNoTracking() => _dbSet.AsNoTracking();
    public EntityEntry<TEntity> Remove(TEntity entity) => _dbSet.Remove(entity);
    public IEnumerator<TEntity> GetEnumerator() => ((IEnumerable<TEntity>)_dbSet).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public Type ElementType => ((IQueryable)_dbSet).ElementType;
    public Expression Expression => ((IQueryable)_dbSet).Expression;
    public IQueryProvider Provider => ((IQueryable)_dbSet).Provider;
    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default) => ((IAsyncEnumerable<TEntity>)_dbSet).GetAsyncEnumerator(cancellationToken);
}