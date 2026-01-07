namespace Models.DBModel
{
    public class AppUser
    {
        public int Id { get; set; }
        public int? CorporationId { get; set; }
        public int? AccountId { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
        public bool Verified { get; set; }   // Default 0
        public bool IsActive { get; set; } // Default 1
    }
}
