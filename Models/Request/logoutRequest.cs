using BaseApi.Models.BaseRequest;

namespace BaseApi.Models.Request
{
    public class logoutRequest: DpsParamBase
    {
        public string token { get; set; }
    }
}
