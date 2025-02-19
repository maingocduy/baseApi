using Microsoft.EntityFrameworkCore;

using BaseApi.Databases.TM;
using BaseApi.Models.Request;

namespace BaseApi.Repository
{
    public interface ISessionRepository :IBaseRepository
    {
        bool LogOutAllSession(string username);
        List<Sessions?> GetListSessionByAccountUuid(string accountUuid);

        Sessions? GetSessionByUuid(string token);
    }
    public class SessionRepository : BaseRepository,ISessionRepository
    {
        public SessionRepository(DBContext dbContext) : base(dbContext)
        {
        }


        public bool LogOutAllSession(string username)
        {
            var session = _dbContext.Sessions.Include(x=>x.AccountUu).Where(x => x.AccountUu.UserName == username && x.Status == 0).ToList();

            if (session != null && session.Count() > 0)
            {
                foreach (var item in session)
                {
                    if (item.Status == 0)
                    {
                        item.TimeLogout = DateTime.Now;
                        item.Status = 1;
                    }

                }
                _dbContext.SaveChanges();
            }
            return true;
        }

        public List<Sessions?> GetListSessionByAccountUuid(string accountUuid)
        {
            return _dbContext.Sessions.Where(x=>x.Status == 0).Where(x => x.AccountUuid == accountUuid).ToList();
        }
        public Sessions? GetSessionByUuid(string token)
        {
            return _dbContext.Sessions.Where(x => x.Uuid == token).SingleOrDefault();
        }
    }
}
