namespace BaseApi.Models.Request
{
    public class UpsertUserRequest : UuidRequest
    {
        public string FullName { get; set; }

        public string? Email { get; set; }
        public sbyte Gender { get; set; }

        public string? Phone { get; set; }

        public DateOnly? Birthday { get; set; }

        public string? Address { get; set; }

        public string? Matp { get; set; }

        public string? Maqh { get; set; }

        public string? Xaid { get; set; }

        public string? Note { get; set; }

    }
}
