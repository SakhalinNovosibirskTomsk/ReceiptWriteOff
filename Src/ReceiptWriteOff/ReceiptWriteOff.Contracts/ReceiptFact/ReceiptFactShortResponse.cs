namespace ReceiptWriteOff.Contracts.ReceiptFact;

public class ReceiptFactShortResponse : EntityResponse
{
    public int BookInstanceId { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
}