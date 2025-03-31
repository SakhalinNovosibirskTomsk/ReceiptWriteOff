namespace ReceiptWriteOff.Application.Contracts.WriteOffReason;

public class WriteOffReasonDto : EntityDto
{
    public required string Description { get; set; }
}