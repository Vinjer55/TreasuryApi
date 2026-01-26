namespace Models.Request
{
    public class ConversionRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Amount { get; set; }
    }
}
