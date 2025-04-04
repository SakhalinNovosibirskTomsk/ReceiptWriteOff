namespace ReceiptWriteOff.Contracts.WriteOffFact;
    
    public class WriteOffFactShortResponse : EntityResponse
    {
        public int BookInstanceId { get; set; }
        public DateTime? Date { get; set; }
        public int UserId { get; set; }
        public int WriteOffReasonId { get; set; }
    }