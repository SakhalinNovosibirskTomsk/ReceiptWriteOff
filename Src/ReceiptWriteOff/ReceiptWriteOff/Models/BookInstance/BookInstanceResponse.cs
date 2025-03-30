using ReceiptWriteOff.Models.Book;

namespace ReceiptWriteOff.Models.BookInstance;

public class BookInstanceResponse
{
    public required BookResponse Book { get; set; }
    public int InventoryNumber { get; set; }
}