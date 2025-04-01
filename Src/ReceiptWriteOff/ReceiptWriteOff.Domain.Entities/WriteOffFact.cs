using ReceiptWriteOff.Domain.Entities.Abstractions;

namespace ReceiptWriteOff.Domain.Entities;

public class WriteOffFact : Entity
{
    public int BookInstanceId { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
    public int WriteOffReasonId { get; set; }
    
    public virtual required BookInstance BookInstance { get; set; }
    public virtual required WriteOffReason WriteOffReason { get; set; }
}