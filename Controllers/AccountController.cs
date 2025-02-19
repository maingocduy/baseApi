using BaseApi.AttributeExtend;
using BaseApi.Models.Request;
using BaseApi.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BaseApi.Enums;

using Microsoft.EntityFrameworkCore;
using BaseApi.Utils;
using BaseApi.Models.DataInfo;
using Microsoft.VisualStudio.Services.CircuitBreaker;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.ComponentModel;
using Newtonsoft.Json;
using BaseApi.Extensions;
using BaseApi.Configuaration;
using OfficeOpenXml;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualStudio.Services.Account;
using System.Security.Principal;
using static System.Net.WebRequestMethods;

using Microsoft.VisualStudio.Services.Common;
using BaseApi.Controllers;
using BaseApi.Databases.TM;
using BaseApi.Service;
using BaseApi.Models.BaseRequest;


namespace BaseApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    [SwaggerTag("Account Controller")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;
        private readonly ILogger<AccountController> _logger;
        private readonly DBContext _context;

        public AccountController(DBContext context, ILogger<AccountController> logger, IAccountService accountService, IAuthService authService)
        {
            _accountService = accountService;
            _context = context;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("send-otp")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "SendOtpEmail Response")]
        public async Task<IActionResult> SendOtpEmail([FromBody] SendOtpRequest request)
        {
            try
            {

                var response = _accountService.SendOtpEmail(request);
                return Ok(response);
            }
            catch (ErrorException ex)
            {
                var response = new BaseResponse();
                response.error.SetErrorCode(ex.Code);


                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse();
                //_logger.LogError(ex.Message, ex);
                response.error.SetErrorCode(ErrorCode.SYSTEM_ERROR, ex.Message);

                return StatusCode(500, response);
            }
        }

        [HttpPost("enter-otp")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "EnterOtp Response")]
        public async Task<IActionResult> EnterOtp([FromBody] EnterOtpRequest request)
        {
            try
            {
                var response = _accountService.EnterOtp(request);
                return Ok(response);
            }
            catch (ErrorException ex)
            {
                var response = new BaseResponse();
                response.error.SetErrorCode(ex.Code);


                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse();
                //_logger.LogError(ex.Message, ex);
                response.error.SetErrorCode(ErrorCode.SYSTEM_ERROR, ex.Message);

                return StatusCode(500, response);
            }
        }

        [HttpPost("change-pass-forget")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "ChangePassForget Response")]
        public async Task<IActionResult> ChangePasswordForget([FromBody] ChangeForgetPass request)
        {
            var response = new BaseResponse();
            try
            {
                var account = _accountService.ChangePasswordForget(request);
                if (account.Status != 0)
                {
                    _authService.LogOutAllSession(account.Uuid);
                }
                response.error.SetErrorCode(ErrorCode.SUCCESS);
                return Ok(response);
            }
            catch (ErrorException ex)
            {

                response.error.SetErrorCode(ex.Code);


                return Ok(response);
            }
            catch (Exception ex)
            {

                //_logger.LogError(ex.Message, ex);
                response.error.SetErrorCode(ErrorCode.SYSTEM_ERROR, ex.Message);

                return StatusCode(500, response);
            }
        }
        [HttpPost("change-pass")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "ChangePass Response")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = new BaseResponse();

                // Call your service to change the password
                _accountService.ChangePassWord(request);

                // Set success response
                response.error.SetErrorCode(ErrorCode.SUCCESS);
                return Ok(response);

            }, _context);
        }

        [HttpPost("update-account")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "UpdateAccount Response")]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = new BaseResponse();

                // Call your service to change the password
                _accountService.UpdateAccount(request);

                // Set success response
                response.error.SetErrorCode(ErrorCode.SUCCESS);
                return Ok(response);

            }, _context);
        }
        [HttpPost("register")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "register Response")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = new BaseResponse();


                _accountService.Register(request);


                response.error.SetErrorCode(ErrorCode.SUCCESS);
                return Ok(response);

            }, _context);
        }

        [HttpPost("update-status")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "UpdateStatus Response")]
        public async Task<IActionResult> UpdateStatus([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = new BaseResponse();


                _accountService.UpdateStatus(request.Uuid);


                response.error.SetErrorCode(ErrorCode.SUCCESS);
                return Ok(response);

            }, _context);
        }

        [HttpPost("update-special")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "UpdateSpecial Response")]
        public async Task<IActionResult> UpdateSpecial([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = new BaseResponse();


                _accountService.UpdateSpecial(request.Uuid);


                response.error.SetErrorCode(ErrorCode.SUCCESS);
                return Ok(response);

            }, _context);
        }


        [HttpPost("lock_acc")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "LockAcc Response")]
        public async Task<IActionResult> LockAcc([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = new BaseResponse();


                _accountService.LockAcc(request);


                response.error.SetErrorCode(ErrorCode.SUCCESS);
                return Ok(response);

            }, _context);
        }
        [HttpPost("get-page-list-account")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessagePage<AccountDTO>), description: "GetPageListAccount Response")]
        public async Task<IActionResult> GetPageListAccount([FromBody] FindAccountPageRequest request)
        {
            return ProcessRequest((accountUuid) =>
            {
                var response = _accountService.GetPageListAccount(request);
                return Ok(response);

            }, _context);
        }

        [HttpPost("detail-account")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<DetailAccountDTO>), description: "DetailAccount Response")]
        public async Task<IActionResult> DetailAccount([FromBody] UuidRequest request)
        {
            return ProcessRequest((accountUuid) =>
            {
                var response = _accountService.GetDetailAccount(request.Uuid);

                return Ok(response);

            }, _context);
        }

        [HttpPost("detail-loginner")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<DetailAccountDTO>), description: "DetailLoginer Response")]
        public async Task<IActionResult> DetailLoginner([FromBody] DpsParamBase request)
        {
            return ProcessRequest((token) =>
            {
                var response = _accountService.GetDetailAccount(token.AccountUuid);

                return Ok(response);

            }, _context);
        }

        [HttpPost("category-account")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessageItem<CategoryAccountDTO>), description: "CategoryAccount Response")]
        public async Task<IActionResult> CategoryAccount([FromBody] GetCategoryAccountRequest request)
        {
            return ProcessRequest((accountUuid) =>
            {
                var response = _accountService.GetCategoryAccount(request);

                return Ok(response);

            }, _context);
        }
    }
}
