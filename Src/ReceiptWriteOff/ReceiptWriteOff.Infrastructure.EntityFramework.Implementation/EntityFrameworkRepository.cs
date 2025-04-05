using Microsoft.EntityFrameworkCore;
using ReceiptWriteOff.Domain.Entities.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation.Exceptions;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;

public class EntityFrameworkRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey> 
    where TPrimaryKey : struct
{
    private readonly IDatabaseContext _databaseContext;
    protected readonly IDbSet<TEntity> _entitySet;
    protected readonly IQueryableExtensionsWrapper<TEntity> _queryableExtensionsWrapper;
    
    public EntityFrameworkRepository(
        IDatabaseContext databaseContext, 
        IQueryableExtensionsWrapper<TEntity> queryableExtensionsWrapper)
    {
        _databaseContext = databaseContext;
        _entitySet = databaseContext.GetDbSet<TEntity>();
        _queryableExtensionsWrapper = queryableExtensionsWrapper;
    }

    public async Task AddRangeIfNotExistsAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            var existingEntity = await _entitySet.FindAsync([entity.Id], cancellationToken);
            if (existingEntity == null)
            {
                await AddAsync(entity, cancellationToken);
            }
        }
    }
    
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _entitySet.AddAsync(entity, cancellationToken);
    }

    public async Task<TEntity> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
    {
        var entity = await _entitySet.FindAsync([id], cancellationToken);
        if (entity == null)
        {
            throw new EntityNotFoundException($"Entity with id={id} not found.");
        }

        return entity;
    }

    public virtual IQueryable<TEntity> GetAll(bool asNoTracking = false)
    {
        return asNoTracking ? _entitySet.AsNoTracking() : _entitySet;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var queryable = GetAll(asNoTracking);
        return await _queryableExtensionsWrapper.ToListAsync(queryable, cancellationToken);
    }

    public async Task UpdateAsync(TPrimaryKey id, CancellationToken cancellationToken)
    {
        var entity = await GetAsync(id, cancellationToken);
        Update(entity);
    }

    public void Update(TEntity entity)
    {
        _databaseContext.GetEntry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken)
    {
        var entity = await GetAsync(id, cancellationToken);
        _entitySet.Remove(entity);
    }

    public void Delete(TEntity entity)
    {
        _databaseContext.GetEntry(entity).State = EntityState.Deleted;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}