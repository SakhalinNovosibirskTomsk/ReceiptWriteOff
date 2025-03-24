using Microsoft.EntityFrameworkCore;
using ReceiptWriteOff.Domain.Entities.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;
using ReceiptWriteOff.Infrastructure.EntityFramework.Exceptions;
using ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework;

public class EntityFrameworkRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey> 
    where TPrimaryKey : struct
{
    private readonly IDatabaseContext _databaseContext;
    private readonly IDbSet<TEntity> _entitySet;
    
    public EntityFrameworkRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _entitySet = new DdSetDecorator<TEntity>(_databaseContext.Set<TEntity>());
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

    public async Task<TEntity?> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
    {
        return await _entitySet.FindAsync([id], cancellationToken);
    }

    public virtual IQueryable<TEntity> GetAll(bool asNoTracking = false)
    {
        return asNoTracking ? _entitySet.AsNoTracking() : _entitySet;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        return await GetAll(asNoTracking).ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(TPrimaryKey id, CancellationToken cancellationToken)
    {
        var entity = await GetAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new EntityNotFoundException($"Entity with id={id} not found.");
        }
        Update(entity);
    }

    public void Update(TEntity entity)
    {
        _databaseContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken)
    {
        var entity = await GetAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new EntityNotFoundException($"Entity with id={id} not found.");
        }
        _entitySet.Remove(entity);
    }

    public void Delete(TEntity entity)
    {
        _databaseContext.Entry(entity).State = EntityState.Deleted;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}