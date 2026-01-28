namespace Models.Request
{
    public class FiatToFiatRequest
    {
        public int FromBankId { get; set; }
        public string FromAccount { get; set; }
        public int ToBankId { get; set; }
        public string ToAccount { get; set; }
        public decimal Amount { get; set; }
    }
}
