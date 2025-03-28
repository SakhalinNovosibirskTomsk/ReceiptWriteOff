namespace ReceiptWriteOff.Services.Contracts.ReceiptFact;

public class ReceiptFactShortDto : EntityDto
{
    public int BookInstanceId { get; set; }
    public DateTime? Date { get; set; }
    public int UserId { get; set; }
}