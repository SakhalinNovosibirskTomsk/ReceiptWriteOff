using System.Linq.Expressions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

public interface IQueryableExtensionsWrapper<TEntity>
{
    Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> source, CancellationToken cancellationToken = default);
    IQueryable<TEntity> Where<TEntity>(IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate);
}