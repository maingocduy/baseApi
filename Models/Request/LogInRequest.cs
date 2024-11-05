using System.ComponentModel.DataAnnotations;
using TaskMonitor.Models.BaseRequest;

namespace TaskMonitor.Models.Request
{
    public class LogInRequest : DpsParamBase
    {
       
        public string UserName { get; set; }
        public string Password { get; set; }

        public int Type { get; set; }

/*        public string? FCMToken { get; set; }*/
    }
}
