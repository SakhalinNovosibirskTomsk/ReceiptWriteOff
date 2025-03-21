namespace ReceiptWriteOff.Domain.Entities;

public class ReceiptFact : Entity
{
    public int BookInstanceId { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
    
    public required BookInstance BookInstance { get; set; }
}