using TaskMonitor.Models.BaseRequest;
using System.ComponentModel.DataAnnotations;

namespace TaskMonitor.Models.Request
{
    public class UuidRequest : DpsParamBase
    {
       
        public string? Uuid { get; set; }
    }
}
