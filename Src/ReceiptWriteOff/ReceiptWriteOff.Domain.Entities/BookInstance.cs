using ReceiptWriteOff.Domain.Entities.Abstractions;

namespace ReceiptWriteOff.Domain.Entities;

public class BookInstance : Entity
{
    public int BookId { get; set; }
    public int ReceipdFactId { get; set; }
    public int InventoryNumber { get; set; }
    
    public virtual required Book Book { get; set; }
    public virtual required ReceiptFact ReceiptFact { get; set; }
    public virtual WriteOffFact? WriteOffFact { get; set; }
}