namespace ReceiptWriteOff.Services.Contracts.WriteOffFact;

public class RegisterWriteOffFactDto
{
    public int BookInstanceId { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
    public int WriteOffReasonId { get; set; }
}