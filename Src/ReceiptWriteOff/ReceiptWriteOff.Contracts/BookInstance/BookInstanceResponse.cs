using ReceiptWriteOff.Contracts.Book;

namespace ReceiptWriteOff.Contracts.BookInstance;

public class BookInstanceResponse : EntityResponse
{
    public required BookResponse Book { get; set; }
    public int InventoryNumber { get; set; }
}