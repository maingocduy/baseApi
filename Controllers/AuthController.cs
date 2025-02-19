using BaseApi.AttributeExtend;
using BaseApi.Models.Request;
using BaseApi.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BaseApi.Enums;
using Microsoft.EntityFrameworkCore;
using BaseApi.Utils;
using BaseApi.Models.BaseRequest;
using BaseApi.Controllers;
using BaseApi.Databases.TM;
using BaseApi.Models.DataInfo;
using BaseApi.Service;
using BaseApi.Configuaration;


namespace BaseApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    [SwaggerTag("Auth Controller")]
    public class AuthController : BaseController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly DBContext _context;
        private readonly IAuthService _authService;
        public AuthController(DBContext context, ILogger<AuthController> logger, IAuthService authService)
        {

            _context = context;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("login")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(LogInResp), description: "LogIn Response")]
        public async Task<IActionResult> LogIn(LogInRequest request)
        {
            var response = new BaseResponseMessage<LogInResp>();
            

            try
            {
                response = _authService.login(request);
                return Ok(response);
            }
            catch (ErrorException ex)
            {
                response.error.SetErrorCode(ex.Code);
                _logger.LogError(ex.Message);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.error.SetErrorCode(ErrorCode.SYSTEM_ERROR);
                _logger.LogError(ex.Message);

                return StatusCode(500,response);
            }
        }
        //[HttpPost("test")]
        
        //public async Task<IActionResult> test(LogInRequest request)
        //{
        //    var response = new BaseResponseMessage<LogInResp>();


        //    try
        //    {
        //        DateTime now = DateTime.Now;
        //        TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

        //        Console.WriteLine($"Giờ hiện tại: {now}");
        //        Console.WriteLine($"Múi giờ: {localTimeZone.DisplayName}");
        //        return Ok();
        //    }
        //    catch (ErrorException ex)
        //    {
        //        response.error.SetErrorCode(ex.Code);
        //        _logger.LogError(ex.Message);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.error.SetErrorCode(ErrorCode.SYSTEM_ERROR);
        //        _logger.LogError(ex.Message);

        //        return StatusCode(500, response);
        //    }
        //}

        [HttpPost("logout")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "LogOut Response")]
        public async Task<IActionResult> LogOut(DpsParamBase request)
        {
            return ProcessRequest((token) =>
            {
                var response = new BaseResponse();


                _authService.LogOut(getTokenInfo(_context).Token);


                response.error.SetErrorCode(ErrorCode.SUCCESS);
                return Ok(response);

            }, _context);
        }
    }
}
