using BaseApi.Models.Request;

namespace BaseApi.Models.Request
{
    public class GetCategoryAccountRequest : BaseKeywordRequest
    {
        public List<sbyte>? Status { get; set; }
        public string? RoleUuid { get; set; }
    }
}
