using ReceiptWriteOff.Services.Contracts.Book;

namespace ReceiptWriteOff.Services.Contracts.BookInstance;

public class BookInstanceDto : EntityDto
{
    public required BookDto Book { get; set; }
    public int InventoryNumber { get; set; }
}