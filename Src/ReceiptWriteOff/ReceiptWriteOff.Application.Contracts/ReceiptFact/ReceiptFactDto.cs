using ReceiptWriteOff.Application.Contracts.BookInstance;

namespace ReceiptWriteOff.Application.Contracts.ReceiptFact;

public class ReceiptFactDto : EntityDto
{
    public required BookInstanceShortDto BookInstance { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
}