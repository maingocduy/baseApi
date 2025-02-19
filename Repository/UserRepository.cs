using Microsoft.AspNetCore.Identity.Data;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using BaseApi.Configuaration;
using BaseApi.Databases.TM;
using BaseApi.Enums;
using BaseApi.Extensions;
using BaseApi.Models.DataInfo;
using BaseApi.Models.Request;
using BaseApi.Utils;

namespace BaseApi.Repository
{
    public interface IUserRepository : IBaseRepository
    {
        User? GetByUuid(string uuid);
        User? GetByCode(string code);

        List<User> GetListUser(GetCategoryUserRequest request);
        List<User> GetPageListUser(FindUserPageRequest request, TokenInfo token);
        public string GetFullnameByActivityUuid(string activityUuid);

        int Count(FindUserPageRequest request, TokenInfo token);
        
        User? GetByToken(TokenInfo tokenInfo);

        User? GetReporterByActivityUuid(string activityUuid);

        List<User>? GetByPhone(string phone);

        List<User>? GetByEmail(string email);

        User? GetReporterByReport(string reportUuid);
    }

    [ScopedService]
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DBContext dbContext) : base(dbContext)
        {
        }

        private IQueryable<User> GetAll(FindUserPageRequest request,TokenInfo token)
        {
            if (token.RoleName == "MANAGER")
            {
                return _dbContext.User.Include(x => x.Account)
                    .ThenInclude(x=>x.RoleUu)
                    .Where(x=>x.Account.FirstOrDefault().RoleUu.Name == "USER")
               .Where(x => request.IsHaveAcc == null || (request.IsHaveAcc == 0 && !x.Account.Any()) ||
               (request.IsHaveAcc == 1 && x.Account.Any()))
               .Where(x => request.Status == null || request.Status == x.Status)
               .Where(x => string.IsNullOrEmpty(request.Keyword)
                   || EF.Functions.Like(x.Code, $"%{request.Keyword}%")
                   || EF.Functions.Like(x.Fullname, $"%{request.Keyword}%")
                   || EF.Functions.Like(x.Phone, $"%{request.Keyword}%")).AsQueryable();
            }
                return _dbContext.User.Include(x => x.Account)
                .Where(x => request.IsHaveAcc == null || (request.IsHaveAcc == 0 && !x.Account.Any()) ||
                (request.IsHaveAcc == 1 && x.Account.Any()))
                .Where(x=>request.Status == null || request.Status == x.Status)
                .Where(x => string.IsNullOrEmpty(request.Keyword)
                    || EF.Functions.Like(x.Code, $"%{request.Keyword}%")
                    || EF.Functions.Like(x.Fullname, $"%{request.Keyword}%") 
                    || EF.Functions.Like(x.Phone, $"%{request.Keyword}%")).AsQueryable();
        }
        public List<User> GetPageListUser(FindUserPageRequest request, TokenInfo token)
        {
            return GetAll(request, token).ToList();
        }

        public List<User> GetListUser(GetCategoryUserRequest request)
        {
            // Lọc trước theo Type nếu có
            var query = _dbContext.User
                .Include(x => x.Account)
                .ThenInclude(account => account.RoleUu)
                .Where(x => request.Status ==null || request.Status == x.Status);

            // Nếu request.Type khác null, lọc theo Type
            if (request.Type != null)
            {
                if (request.Type == 1)
                {
                    query = query.Where(x => x.Account.Any(account => account.RoleUu.Code == "R1"));
                }
                else if (request.Type == 2)
                {
                    query = query.Where(x => x.Account.Any(account => account.RoleUu.Code == "R2"));
                }
                else if (request.Type == 3)
                {
                    query = query.Where(x => x.Account.Any(account => account.RoleUu.Code == "R3"));
                }
            }

            // Lọc theo RoleUuid nếu có
            if (!string.IsNullOrEmpty(request.RoleUuid))
            {
                query = query.Where(x => x.Account.Any(account => account.RoleUuid == request.RoleUuid));
            }

            // Lọc theo từ khóa nếu có
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => EF.Functions.Like(x.Code, $"%{request.Keyword}%") ||
                                         EF.Functions.Like(x.Fullname, $"%{request.Keyword}%"));
            }

            return query.ToList();
        }




        public int Count(FindUserPageRequest request,TokenInfo tokenInfo)
        {
            return GetAll(request,tokenInfo).Count();
        }

        public User? GetByToken(TokenInfo tokenInfo)
        {
            var user = _dbContext.User
                .Include(u => u.Account).ThenInclude(x=>x.RoleUu)
                .FirstOrDefault(u => u.Account.Any(acc => acc.Uuid.Equals(tokenInfo.AccountUuid)));
            return user;
        }

        public User? GetByUuid(string uuid)
        {
            return _dbContext.User.Include(x=>x.Account).ThenInclude(x=>x.RoleUu).Include(x => x.MatpNavigation).Include(x=>x.UserProjects).Include(x => x.Xa).Include(x => x.MaqhNavigation).FirstOrDefault(a => a.Uuid == uuid);
        }

        public User? GetByCode(string code)
        {
            return _dbContext.User.Include(x => x.MatpNavigation).Include(x => x.Xa).Include(x => x.MaqhNavigation).FirstOrDefault(a => a.Code == code);
        }

        public List<User>? GetByPhone(string phone)
        {
            return _dbContext.User.Include(x => x.MatpNavigation).Include(x => x.Xa).Include(x => x.MaqhNavigation).Where(a => a.Phone == phone).ToList();
        }
        public List<User>? GetByEmail(string email)
        {
            return _dbContext.User.Include(x => x.MatpNavigation).Include(x => x.Xa).Include(x => x.MaqhNavigation).Where(x=>x.Email == email).ToList();
        }
        public string GetFullnameByActivityUuid(string activityUuid)
        {
            return _dbContext.User
                .Include(u => u.UserProjects)
                    .ThenInclude(up => up.Reports)
                        .ThenInclude(r => r.ActivityReport)
                .Where(u => u.UserProjects.Any(up => up.Reports.Any(r => r.ActivityReport.Any(ar => ar.ActivityUuid == activityUuid))))
                .Select(u => u.Fullname)
                .FirstOrDefault();
        }

        public User? GetReporterByActivityUuid(string activityUuid)
        {
            return _dbContext.User
                .Include(u => u.UserProjects)
                    .ThenInclude(up => up.Reports)
                        .ThenInclude(r => r.ActivityReport)
                .Where(u => u.UserProjects.Any(up => up.Reports.Any(r => r.ActivityReport.Any(ar => ar.ActivityUuid == activityUuid))))
                .FirstOrDefault();
        }
        public User? GetReporterByReport(string reportUuid)
        {
            return _dbContext.User
                .Include(u => u.UserProjects)
                    .ThenInclude(up => up.Reports)
                .Where(u => u.UserProjects.Any(up => up.Reports.Any(x=>x.Uuid == reportUuid)))
                .FirstOrDefault();
        }

    }
}
