using TaskMonitor.Models.Request;

namespace TaskMonitor.Models.Request
{
    public class UuidPageRequest : BaseKeywordPageRequest
    {
        public string? Uuid { get; set; }
    }
}