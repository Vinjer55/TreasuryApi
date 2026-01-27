namespace Models.Response
{
    public class CryptoToCryptoResponse
    {
        public decimal ConvertedAmount { get; set; }
        public string FromAsset { get; set; }
        public string ToAsset { get; set; }
        public decimal NewFromBalance { get; set; }
        public decimal NewToBalance { get; set; }
    }
}
