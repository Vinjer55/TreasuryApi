namespace Models.Response
{
    public class FiatToCryptoResponse
    {
        public decimal ConvertedAmount { get; set; }
        public string Currency { get; set; }
        public decimal NewWalletBalance { get; set; }
        public int WalletId { get; set; }
    }
}
