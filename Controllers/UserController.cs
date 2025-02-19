using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BaseApi.AttributeExtend;
using BaseApi.Configuaration;
using BaseApi.Databases.TM;
using BaseApi.Enums;
using BaseApi.Models.DataInfo;
using BaseApi.Models.Request;
using BaseApi.Models.Response;
using BaseApi.Service;

namespace BaseApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    [SwaggerTag("User Controller")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly DBContext _context;
        public UserController(DBContext context, ILogger<AccountController> logger, IUserService userService)
        {
            _userService = userService;
            _context = context;
            _logger = logger;
        }

        [HttpPost("get-page-list-user")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessagePage<UserDTO>), description: "GetPageListUser Response")]
        public async Task<IActionResult> GetPageListUser([FromBody] FindUserPageRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _userService.GetPageListUser(request, token);

                return Ok(response);

            }, _context);
        }

        [HttpPost("detail-user")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessage<DetailUserDTO>), description: "DetailUser Response")]
        public async Task<IActionResult> DetailUser([FromBody] UuidRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _userService.GetDetailUser(request.Uuid);

                return Ok(response);

            }, _context);
        }

        [HttpPost("category-user")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessageItem<CategoryUserDTO>), description: "CategoryUser Response")]
        public async Task<IActionResult> CategoryUser([FromBody] GetCategoryUserRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _userService.GetCategoryUser(request);

                return Ok(response);

            }, _context);
        }
        [HttpPost("update-status")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "Update Status Response")]
        public async Task<IActionResult> UpdateStatus([FromBody] UuidRequest request)
        {
           return ProcessRequest((token) =>
           {
               var response = new BaseResponse();


               _userService.UpdateStatus(request.Uuid);


               response.error.SetErrorCode(ErrorCode.SUCCESS);
               return Ok(response);

           }, _context);
        }

        [HttpPost("upsert-user")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponse), description: "Upsert User Response")]
        public async Task<IActionResult> UpsertUser([FromBody] UpsertUserRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = new BaseResponse();


                _userService.UpsertUser(request);


                response.error.SetErrorCode(ErrorCode.SUCCESS);
                return Ok(response);

            }, _context);
        }
    }
}
