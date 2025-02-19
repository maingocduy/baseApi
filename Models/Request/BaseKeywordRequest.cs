using BaseApi.Models.BaseRequest;

namespace BaseApi.Models.Request
{
    public class BaseKeywordRequest : DpsParamBase
    {
        public string? Keyword { get; set; }
    }
}
