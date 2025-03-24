namespace ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

public interface IQueryableExtensionsWrapper<TEntity>
{
    Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> source, CancellationToken cancellationToken = default);
}