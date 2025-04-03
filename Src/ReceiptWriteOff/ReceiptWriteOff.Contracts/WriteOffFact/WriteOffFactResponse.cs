namespace ReceiptWriteOff.Contracts.WriteOffFact;

public class WriteOffFactResponse : EntityResponse
{
    public int BookInstanceId { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
}