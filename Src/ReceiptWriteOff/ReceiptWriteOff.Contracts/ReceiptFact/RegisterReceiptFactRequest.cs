namespace ReceiptWriteOff.Contracts.ReceiptFact;

public class RegisterReceiptFactRequest
{
    public int BookId { get; set; }
    public int InventoryNumber { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
}