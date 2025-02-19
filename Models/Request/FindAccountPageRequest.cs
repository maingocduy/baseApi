namespace BaseApi.Models.Request
{
    public class FindAccountPageRequest : BaseKeywordPageRequest
    {
        public List<sbyte>? Status { get; set; }
        public string? RoleUuid { get; set; }

    }
}
