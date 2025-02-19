using BaseApi.Models.Request;

namespace BaseApi.Models.Request
{
    public class UuidPageRequest : BaseKeywordPageRequest
    {
        public string? Uuid { get; set; }
    }
}