namespace Models.Request
{
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public required string Password { get; set; }
    }
}
