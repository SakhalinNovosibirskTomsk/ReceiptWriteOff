using ReceiptWriteOff.Services.Contracts.BookInstance;

namespace ReceiptWriteOff.Services.Contracts.ReceiptFact;

public class ReceiptFactDto : EntityDto
{
    public required BookInstanceShortDto BookInstance { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
}