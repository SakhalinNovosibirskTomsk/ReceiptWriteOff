namespace ReceiptWriteOff.Services.Contracts.Book;

public class BookDto : EntityDto
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public bool IsArchived { get; set; }
}