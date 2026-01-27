namespace Models.Request
{
    public class FiatToCryptoRequest
    {
        public int BankId { get; set; }
        public string Account { get; set; }
        public int WalletId { get; set; }
        public string Asset { get; set; }
        public decimal Amount { get; set; }
    }
}
