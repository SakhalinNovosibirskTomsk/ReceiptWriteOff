namespace ReceiptWriteOff.Contracts.Book;

public class BookResponse : EntityResponse
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public bool IsArchived { get; set; }
}