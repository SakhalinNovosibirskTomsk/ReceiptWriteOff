namespace ReceiptWriteOff.Contracts.Book;

public class CreateOrEditBookRequest
{
    public required string Title { get; set; }
    public required string Author { get; set; }
}