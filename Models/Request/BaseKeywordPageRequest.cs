using BaseApi.Models.BaseRequest;

namespace BaseApi.Models.Request
{
    public class BaseKeywordPageRequest:DpsPagingParamBase
    {
        public string? Keyword { get; set; }

        public sbyte? Status { get; set; }
    }
}
