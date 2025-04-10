using ReceiptWriteOff.Domain.Entities.Abstractions;

namespace ReceiptWriteOff.Domain.Entities;

public class Book : Entity
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public bool IsArchived { get; set; }
    
    public virtual required ICollection<BookInstance> BookInstances { get; set; }
}