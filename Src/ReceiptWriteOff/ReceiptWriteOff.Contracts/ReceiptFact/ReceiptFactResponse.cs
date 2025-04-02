using ReceiptWriteOff.Contracts.BookInstance;

namespace ReceiptWriteOff.Contracts.ReceiptFact;

public class ReceiptFactResponse : EntityResponse
{
    public required BookInstanceShortResponse BookInstance { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
}