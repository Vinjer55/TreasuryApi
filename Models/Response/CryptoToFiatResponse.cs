namespace Models.Response
{
    public class CryptoToFiatResponse
    {
        public decimal ConvertedAmount { get; set; }
        public string Currency { get; set; }
        public decimal NewBankBalance { get; set; }
        public int BankAccountId { get; set; }
    }
}
