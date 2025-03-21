namespace ReceiptWriteOff.Domain.Entities;

public class BookInstance : Entity
{
    public int BookId { get; set; }
    public int InventoryNumber { get; set; }
    
    public required Book Book { get; set; }
    public required ReceiptFact ReceiptFact { get; set; }
    public WriteOffFact? WriteOffFact { get; set; }
}