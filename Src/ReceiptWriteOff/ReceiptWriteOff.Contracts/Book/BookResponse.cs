namespace ReceiptWriteOff.Models.Book;

public class BookResponse
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public bool IsArchived { get; set; }
}