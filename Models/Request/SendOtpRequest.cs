using System.ComponentModel.DataAnnotations;
using BaseApi.Models.BaseRequest;

namespace BaseApi.Models.Request
{
    public class SendOtpRequest : DpsParamBase
    {
        [Required(ErrorMessage = "Email không được để trống!")]
        public string Email { get; set; }
    }
    public class EnterOtpRequest : SendOtpRequest
    {
        [Required(ErrorMessage = "Otp không được để trống!")]
        public string Otp { get; set; }
    }
    public class ChangeForgetPass : EnterOtpRequest
    {
        [Required(ErrorMessage = "Mật khẩu mới không được để trống!")]
        public string NewPass { get; set; }
    }
}
