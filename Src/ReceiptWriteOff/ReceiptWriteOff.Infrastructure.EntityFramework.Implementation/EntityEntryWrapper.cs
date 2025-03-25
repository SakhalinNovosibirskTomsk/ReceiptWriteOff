using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

namespace ReceiptWriteOff.Infrastructure.EntityFramework;

public class EntityEntryWrapper<TEntity>(EntityEntry<TEntity> _entityEntry) : IEntityEntry<TEntity>
    where TEntity : class
{
    public EntityState State
    {
        get => _entityEntry.State;
        set => _entityEntry.State = value;
    }
}