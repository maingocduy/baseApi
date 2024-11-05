using TaskMonitor.Models.BaseRequest;

namespace TaskMonitor.Models.Request
{
    public class BaseKeywordPageRequest:DpsPagingParamBase
    {
        public string? Keyword { get; set; }

        public sbyte? Status { get; set; }
    }
}
