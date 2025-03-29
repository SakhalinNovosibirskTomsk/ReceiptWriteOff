using ReceiptWriteOff.Domain.Entities.Abstractions;

namespace ReceiptWriteOff.Infrastructure.Repositories.Abstractions
{
    public interface IRepository<TEntity, TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey> 
        where TPrimaryKey : struct
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> GetAsync(TPrimaryKey id, CancellationToken cancellationToken);
        IQueryable<TEntity> GetAll(bool asNoTracking = false);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);
        Task UpdateAsync(TPrimaryKey id, CancellationToken cancellationToken);
        void Update(TEntity entity);
        Task DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken);
        void Delete(TEntity entity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}