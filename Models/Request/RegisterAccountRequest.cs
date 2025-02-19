
using System.ComponentModel.DataAnnotations;
using BaseApi.Models.BaseRequest;

namespace BaseApi.Models.Request
{
    public class RegisterAccountRequest : DpsParamBase
    {
       public string Username {  get; set; }

        public string Password { get; set; }
        public string UserUuid { get; set; }
        public string RoleUuid { get; set; } 

        public sbyte Special {  get; set; } = 0;
    }
}
