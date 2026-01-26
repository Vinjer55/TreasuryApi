namespace Models.Request
{
    public class CryptoToFiatRequest
    {
        public int WalletId { get; set; }
        public string Asset { get; set; }
        public int BankId { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
    }
}
