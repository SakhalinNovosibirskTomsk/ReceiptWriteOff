namespace ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

public interface IDatabaseContext
{
    IDbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    IEntityEntry<TEntity> GetEntry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
}