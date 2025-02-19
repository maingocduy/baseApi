using BaseApi.Models.BaseRequest;

namespace BaseApi.Models.Request
{
    public class UpdateAccountRequest : UuidRequest
    {
        public string RoleUuid { get; set; }
    }
}
