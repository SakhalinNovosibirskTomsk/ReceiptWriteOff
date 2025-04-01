using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;

public class DdSetWrapper<TEntity>(DbSet<TEntity> _dbSet) : DbSet<TEntity>, IDbSet<TEntity>
    where TEntity : class
{
    public override IEntityType EntityType => _dbSet.EntityType;
    public IQueryable<TEntity> AsNoTracking() => _dbSet.AsNoTracking();
}