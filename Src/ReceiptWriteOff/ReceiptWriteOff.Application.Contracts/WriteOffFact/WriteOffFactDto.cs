using ReceiptWriteOff.Application.Contracts.BookInstance;
using ReceiptWriteOff.Application.Contracts.WriteOffReason;

namespace ReceiptWriteOff.Application.Contracts.WriteOffFact;

public class WriteOffFactDto : EntityDto
{
    public required BookInstanceDto BookInstance { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
    public required WriteOffReasonDto WriteOffReason { get; set; }
}