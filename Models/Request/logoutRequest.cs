using TaskMonitor.Models.BaseRequest;

namespace TaskMonitor.Models.Request
{
    public class logoutRequest: DpsParamBase
    {
        public string token { get; set; }
    }
}
