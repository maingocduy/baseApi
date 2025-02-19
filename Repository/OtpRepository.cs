using BaseApi.Databases.TM;
using BaseApi.Extensions;

namespace BaseApi.Repository
{
    public interface IOtpRepository : IBaseRepository
    {
        Otp GetOtp(string otp);
    }

    [ScopedService]
    public class OtpRepository : BaseRepository, IOtpRepository
    {
        public OtpRepository(DBContext dbContext) : base(dbContext)
        {
        }
        public Otp GetOtp(string otp)
        {
            return _dbContext.Otp.FirstOrDefault(x=>x.Otp1 == otp);
        }
    }
}
