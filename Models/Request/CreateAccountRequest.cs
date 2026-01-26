namespace Models.Request
{
    public class CreateAccountRequest
    {
        public string AccountKind { get; set; }
        public string AssetType { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? Balance { get; set; }
        public string Provider { get; set; }
    }
}
