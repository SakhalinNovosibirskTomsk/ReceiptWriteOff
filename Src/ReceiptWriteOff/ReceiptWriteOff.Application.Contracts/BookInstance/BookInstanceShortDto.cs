namespace ReceiptWriteOff.Application.Contracts.BookInstance;

public class BookInstanceShortDto : EntityDto
{
    public int BookId { get; set; }
    public int InventoryNumber { get; set; }
}