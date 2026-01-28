namespace Models.Response
{
    public class FiatToFiatResponse
    {
        public decimal ConvertedAmount { get; set; }
        public string ToCurrency { get; set; }
        public decimal NewBankBalance { get; set; }
    }
}
