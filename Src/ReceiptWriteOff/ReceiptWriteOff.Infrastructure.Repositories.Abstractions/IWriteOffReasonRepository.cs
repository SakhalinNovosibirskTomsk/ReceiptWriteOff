using ReceiptWriteOff.Domain.Entities;

namespace ReceiptWriteOff.Infrastructure.Repositories.Abstractions;

public interface IWriteOffReasonRepository : IRepository<WriteOffReason, int>
{
    bool ContainsWithDescription(string description);
}