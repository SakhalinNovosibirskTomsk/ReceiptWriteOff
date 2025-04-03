namespace ReceiptWriteOff.Contracts.WriteOffReason;
    
    public class WriteOffReasonResponse : EntityResponse
    {
        public required string Reason { get; set; }
    }