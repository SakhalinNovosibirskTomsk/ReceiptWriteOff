namespace ReceiptWriteOff.Application.Contracts.Book;

public class CreateOrEditBookDto
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public bool IsArchived { get; set; }
}