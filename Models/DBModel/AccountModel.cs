namespace Models.DBModel
{
    public class AccountModel
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string AccountType { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
        public string Provider { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
