using ReceiptWriteOff.Application.Contracts.Book;

namespace ReceiptWriteOff.Application.Contracts.BookInstance;

public class BookInstanceDto : EntityDto
{
    public required BookDto Book { get; set; }
    public int InventoryNumber { get; set; }
}