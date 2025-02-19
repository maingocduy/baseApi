using BaseApi.Models.BaseRequest;
using System.ComponentModel.DataAnnotations;

namespace BaseApi.Models.Request
{
    public class UuidRequest : DpsParamBase
    {
       
        public string? Uuid { get; set; }
    }
}
