namespace ReceiptWriteOff.Domain.Entities;

public class WriteOffReason : Entity
{
    public required string Description { get; set; }
    
    public required ICollection<WriteOffFact> WriteOffFacts { get; set; }
}