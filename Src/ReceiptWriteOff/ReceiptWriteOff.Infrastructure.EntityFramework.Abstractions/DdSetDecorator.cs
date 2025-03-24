using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

public class DdSetDecorator<TEntity>(DbSet<TEntity> _dbSet) : DbSet<TEntity>, IDbSet<TEntity>
    where TEntity : class
{
    public override IEntityType EntityType => _dbSet.EntityType;
    public IQueryable<TEntity> AsNoTracking() => _dbSet.AsNoTracking();
}