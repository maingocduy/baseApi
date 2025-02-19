using AutoMapper;
using BaseApi.Databases.TM;
using BaseApi.Extensions;
using BaseApi.Models.DataInfo;
using BaseApi.Models.Request;
using BaseApi.Models.Response;
using BaseApi.Repository;

namespace BaseApi.Service
{
    public interface IRoleService
    {
        BaseResponseMessageItem<InfoCatalogDTO> GetCategoryRole(BaseKeywordRequest request);
    }
    [ScopedService]
    public class RoleService : BaseService, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IRoleRepository roleRepository) : base(dbContext, mapper, configuration)
        {
            _roleRepository = roleRepository;
        }
        public BaseResponseMessageItem<InfoCatalogDTO> GetCategoryRole(BaseKeywordRequest request)
        {
            var response = new BaseResponseMessageItem<InfoCatalogDTO>();
            var role = _roleRepository.GetListRole(request);
            if (role != null)
            {
                var lstCategoryRole = _mapper.Map<List<InfoCatalogDTO>>(role);
                response.Data = lstCategoryRole;
                return response;
            }
            return response;
        }
    }
}
