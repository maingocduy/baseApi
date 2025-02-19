using Microsoft.EntityFrameworkCore;
using BaseApi.Databases.TM;
using BaseApi.Extensions;
using BaseApi.Models.Request;

namespace BaseApi.Repository
{
    public interface IRoleRepository : IBaseRepository
    {
        List<Role> GetListRole(BaseKeywordRequest request);

        Role GetRoleByuuid(string uuid);
    }
    [ScopedService]
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(DBContext dbContext) : base(dbContext)
        {
        }
        public List<Role> GetListRole(BaseKeywordRequest request)
        {
            return _dbContext.Role.Where(x => string.IsNullOrEmpty(request.Keyword)
                    || EF.Functions.Like(x.Name + "" + x.Code, $"%{request.Keyword}%")).ToList();
        }
        public Role GetRoleByuuid(string uuid)
        {
            return _dbContext.Role.FirstOrDefault(x=>x.Uuid == uuid);
        }
    }
}
