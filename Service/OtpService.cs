using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BaseApi.Configuaration;
using BaseApi.Databases.TM;
using BaseApi.Enums;
using BaseApi.Repository;
using BaseApi.Service;

namespace BaseApi.Service
{
    public interface IOtpService
    {
        string GenerateAndStoreOtp(string email, string accUuid);
        bool ValidateOtp(string email, string otp);
        bool IsOtpExpired(string? email, string otp);

        Otp GetOtp(string otp);

        void changeState(string otp);

        bool IsUsed(string? email, string otp);
    }

    public class OtpService : BaseService,IOtpService
    {
     
        private readonly ILogger<OtpService> _logger;
        private static readonly Dictionary<string, (string Otp, DateTime Expiry)> OtpStorage = new Dictionary<string, (string, DateTime)>();
        private static readonly string FixedOtp = "123456"; // Mã OTP cố định
        private const int OtpExpiryMinutes = 1; // Thời gian hết hạn của mã OTP
        private readonly IOtpRepository _otpRepository;

        public OtpService(DBContext dbContext, IMapper mapper, IConfiguration configuration, ILogger<OtpService> logger ,IOtpRepository otpRepository) : base(dbContext, mapper, configuration)
        {
            _logger = logger;
            _otpRepository = otpRepository;
        }
        public Otp GetOtp (string otp)
        {
            return _otpRepository.GetOtp(otp);  
        }
        public string GenerateOtp()
        {
            Random random = new Random();
            string otp = string.Empty;

            for (int i = 0; i < 6; i++)
            {
                otp += random.Next(0, 10);
            }

            return otp;
        }
        public string GenerateAndStoreOtp(string email,string accUuid)
        {
            var newOtp = new Otp
            {
                Otp1 = GenerateOtp(),
                Expired = DateTime.Now.AddMinutes(3),
                AccUuid = accUuid
            };
            _otpRepository.CreateItem(newOtp);
            return newOtp.Otp1;
        }

        public bool ValidateOtp(string email, string otp)
        {
            var checkOtp = _otpRepository.GetOtp(otp);
            if(checkOtp == null)
            {
                return false;
            }
            return otp == checkOtp.Otp1;
        }
        public void changeState(string otp)
        {
            var checkOtp = _otpRepository.GetOtp(otp);
            if (checkOtp == null || checkOtp.State != 0)
            {
                throw new ErrorException(ErrorCode.INVALID_OTP);
            }
            if (IsOtpExpired(null,otp))
            {
                throw new ErrorException(ErrorCode.EXPIRED_OTP);
            }
            checkOtp.State = 1;
            _otpRepository.UpdateItem(checkOtp);
        }
        public bool IsOtpExpired(string? email, string otp)
        {
           var oldOtp = _otpRepository.GetOtp(otp);
            if(oldOtp == null)
            {
                throw new ErrorException(ErrorCode.INVALID_OTP);
            }
            if (DateTime.Now < oldOtp.Expired)
            {
                return false;
            }
            return true;
        }

        public bool IsUsed(string? email, string otp)
        {
            var checkOtp = _otpRepository.GetOtp(otp);
            if (otp == null)
            {
                throw new ErrorException(ErrorCode.INVALID_OTP);
            }
            if(checkOtp == null)
            {
                throw new ErrorException(ErrorCode.INVALID_OTP);
            }
            if (checkOtp.State == 1)
            {
                return true;
            }
            return false;
        }
    }
}
