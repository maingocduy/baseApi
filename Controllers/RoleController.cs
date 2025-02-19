using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BaseApi.AttributeExtend;
using BaseApi.Databases.TM;
using BaseApi.Models.DataInfo;
using BaseApi.Models.Request;
using BaseApi.Models.Response;
using BaseApi.Service;

namespace BaseApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    [SwaggerTag("Role Controller")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;
        private readonly DBContext _context;
        public RoleController(DBContext context, ILogger<RoleController> logger, IRoleService roleService)
        {
            _roleService = roleService;
            _context = context;
            _logger = logger;
        }
        [HttpPost("category-role")]
        [DbpCert]
        [SwaggerResponse(statusCode: 200, type: typeof(BaseResponseMessageItem<InfoCatalogDTO>), description: "CategoryRole Response")]
        public async Task<IActionResult> CategoryRole([FromBody] BaseKeywordRequest request)
        {
            return ProcessRequest((token) =>
            {
                var response = _roleService.GetCategoryRole(request);

                return Ok(response);

            }, _context);
        }
    }
}
