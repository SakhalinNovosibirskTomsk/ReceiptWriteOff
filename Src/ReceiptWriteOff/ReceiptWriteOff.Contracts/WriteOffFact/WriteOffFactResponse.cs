using ReceiptWriteOff.Contracts.BookInstance;
using ReceiptWriteOff.Contracts.WriteOffReason;

namespace ReceiptWriteOff.Contracts.WriteOffFact;

public class WriteOffFactResponse : EntityResponse
{
    public required BookInstanceResponse BookInstance { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
    public required WriteOffReasonResponse WriteOffReason { get; set; }
}