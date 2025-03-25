using Microsoft.EntityFrameworkCore;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions;

public interface IEntityEntry<TEntity> where TEntity : class
{
    EntityState State { get; set; }
}