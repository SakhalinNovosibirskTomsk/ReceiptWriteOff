using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;

public class QueryableExtensionsWrapper<TEntity> : IQueryableExtensionsWrapper<TEntity>
{
    public async Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default) 
        => await source.ToListAsync(cancellationToken);

    public IQueryable<TEntity1> Where<TEntity1>(IQueryable<TEntity1> source, Expression<Func<TEntity1, bool>> predicate)
        => source.Where(predicate);
}