namespace Models.Request
{
    public class CreateAccountRequest
    {
        public string AccountType { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? Balance { get; set; }
        public string Provider { get; set; }
    }
}
