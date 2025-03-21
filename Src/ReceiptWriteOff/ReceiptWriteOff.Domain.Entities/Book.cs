namespace ReceiptWriteOff.Domain.Entities;

public class Book : Entity
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    
    public required ICollection<BookInstance> BookInstances { get; set; }
}