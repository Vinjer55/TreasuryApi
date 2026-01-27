namespace Models.Request
{
    public class CryptoToCryptoRequest
    {
        public int FromWalletId { get; set; }
        public string FromAsset { get; set; }
        public int ToWalletId { get; set; }
        public string ToAsset { get; set; }
        public decimal Amount { get; set; }
    }
}
