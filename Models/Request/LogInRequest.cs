using System.ComponentModel.DataAnnotations;
using BaseApi.Models.BaseRequest;

namespace BaseApi.Models.Request
{
    public class LogInRequest : DpsParamBase
    {
       
        public string UserName { get; set; }
        public string Password { get; set; }

        public int Type { get; set; }

/*        public string? FCMToken { get; set; }*/
    }
}
