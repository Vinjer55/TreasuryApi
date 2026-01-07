namespace Models.Response
{
    public class CorpUsers
    {
        public int Id { get; set; }
        public int? CorporationId { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
    }
}
