using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using BaseApi.Configuaration;
using BaseApi.Databases.TM;
using BaseApi.Enums;
using BaseApi.Models.DataInfo;
using BaseApi.Models.Request;

namespace BaseApi.Repository
{
    public interface IAccountRepository : IBaseRepository
    {
        Account? GetByUuid(string uuid);
        Account? GetByUsername(string username);

        Account? GetByEmail(string email);

        Account? GetByUserUuid(string UserUuid);

        List<Account> GetListAccountByProject(string ProjectUuid);


        List<Account> GetListAccount(GetCategoryAccountRequest request);

        List<Account> GetPageListAccount(FindAccountPageRequest request);

        int Count(FindAccountPageRequest request);
        string GetUserUuid(string uuid);

        List<Account>? GetAccountAdmin();
    }
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(DBContext dbContext) : base(dbContext)
        {
        }

        private IQueryable<Account> GetAll(FindAccountPageRequest request)
        {
            return _dbContext.Account.Include(x=>x.UserUu).Include(x => x.RoleUu).Where(x=>x.RoleUuid == request.RoleUuid|| string.IsNullOrEmpty(request.RoleUuid)).Where(x => request.Status == null || request.Status.Contains(x.Status)).Where(x => string.IsNullOrEmpty(request.Keyword)
                    || EF.Functions.Like(x.UserName , $"%{request.Keyword}%"));
        }
        public List<Account> GetPageListAccount(FindAccountPageRequest request)
        {
            return GetAll(request).ToList();
        }

        public List<Account> GetListAccount(GetCategoryAccountRequest request)
        {
            return _dbContext.Account.Where(x=>request.Status == null || request.Status.Contains(x.Status))
                .Where(x => string.IsNullOrEmpty(request.Keyword)
                    || EF.Functions.Like(x.UserName, $"%{request.Keyword}%")).ToList();
        }
        public Account? GetByUuid(string uuid)
        {
            return _dbContext.Account
                .Include(x => x.UserUu)
                .ThenInclude(x=>x.UserProjects)
                .Include(x => x.RoleUu)
                .Include(x=>x.UserUu.MaqhNavigation)
                .Include(x => x.UserUu.MatpNavigation)
                .Include(x => x.UserUu.Xa).FirstOrDefault(a => a.Uuid == uuid.ToString());
        }
        public Account? GetByUsername(string username)
        {
            return _dbContext.Account.Include(x=>x.UserUu).Where(x=>x.Status != 0).Include(x=>x.RoleUu).FirstOrDefault(a => a.UserName == username);
        }
        public int Count(FindAccountPageRequest request)
        {
            return GetAll(request).Count();
        }
        public Account? GetByEmail(string email)
        {
            return _dbContext.Account.Include(x => x.UserUu).FirstOrDefault(a => a.UserUu.Email == email);
        }
        public Account? GetByUserUuid(string UserUuid)
        {
            return _dbContext.Account.Include(x => x.UserUu).FirstOrDefault(a => a.UserUuid == UserUuid);
        }
        public string GetUserUuid(string uuid)
        {
            return _dbContext.Account.Where(a => a.Uuid == uuid).Select(a => a.UserUuid).FirstOrDefault();
        }

        public List<Account>? GetAccountAdmin()
        {
            return _dbContext.Account.Include(x => x.RoleUu).Where(x=>x.RoleUu.Name == "ADMIN").ToList();
        }

        public List<Account> GetListAccountByProject(string ProjectUuid)
        {
            return _dbContext.Project
            .Include(p => p.UserProjects)
            .ThenInclude(up => up.UserUu)
            .ThenInclude(uu => uu.Account)
            .Where(p => p.Uuid == ProjectUuid)
            .SelectMany(p => p.UserProjects.SelectMany(up => up.UserUu.Account))
            .Where(account => account.Status != 0)
            .ToList();


        }
    }
}
