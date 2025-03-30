using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

public interface IWriteOffFactRepository : IRepository<WriteOffFact, int>
{
    
}