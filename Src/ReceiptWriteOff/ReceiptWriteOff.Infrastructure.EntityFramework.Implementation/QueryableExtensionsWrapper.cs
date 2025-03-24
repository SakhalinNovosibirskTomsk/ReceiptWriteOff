using Microsoft.EntityFrameworkCore;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework;

public class QueryableExtensionsWrapper<TEntity> : IQueryableExtensionsWrapper<TEntity>
{
    public async Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default) 
        => await source.ToListAsync(cancellationToken);
}