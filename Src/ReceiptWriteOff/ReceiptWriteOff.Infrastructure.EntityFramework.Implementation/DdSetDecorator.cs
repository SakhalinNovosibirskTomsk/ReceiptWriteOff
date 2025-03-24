using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework;

public class DdSetDecorator<TEntity>(DbSet<TEntity> _dbSet) : DbSet<TEntity>, IDbSet<TEntity>
    where TEntity : class
{
    public override IEntityType EntityType => _dbSet.EntityType;
    public IQueryable<TEntity> AsNoTracking() => _dbSet.AsNoTracking();
    //public IQueryable<TEntity> GetQueryable(bool asNoTracking = false)
    //    => asNoTracking ? AsNoTracking() : _dbSet;
}