namespace ReceiptWriteOff.Contracts.WriteOffReason;
    
    public class WriteOffReasonResponse : EntityResponse
    {
        public required string Description { get; set; }
    }