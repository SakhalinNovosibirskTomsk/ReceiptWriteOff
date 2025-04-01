using ReceiptWriteOff.Domain.Entities.Abstractions;

namespace ReceiptWriteOff.Domain.Entities;

public class WriteOffReason : Entity
{
    public required string Description { get; set; }
    
    public virtual required ICollection<WriteOffFact> WriteOffFacts { get; set; }
}