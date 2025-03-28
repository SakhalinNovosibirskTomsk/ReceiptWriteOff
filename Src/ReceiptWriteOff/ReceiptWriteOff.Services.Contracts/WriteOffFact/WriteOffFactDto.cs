using ReceiptWriteOff.Services.Contracts.BookInstance;
using ReceiptWriteOff.Services.Contracts.WriteOffReason;

namespace ReceiptWriteOff.Services.Contracts.WriteOffFact;

public class WriteOffFactDto : EntityDto
{
    public required BookInstanceDto BookInstance { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
    public required WriteOffReasonDto WriteOffReason { get; set; }
}