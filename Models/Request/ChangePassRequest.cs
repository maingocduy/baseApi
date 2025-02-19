using BaseApi.Models.BaseRequest;

namespace BaseApi.Models.Request
{
    public class ChangePassRequest : UuidRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
