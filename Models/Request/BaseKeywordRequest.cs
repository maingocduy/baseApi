using TaskMonitor.Models.BaseRequest;

namespace TaskMonitor.Models.Request
{
    public class BaseKeywordRequest : DpsParamBase
    {
        public string? Keyword { get; set; }
    }
}
