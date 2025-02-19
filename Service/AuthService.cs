using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Account;
using BaseApi.Configuaration;
using BaseApi.Databases.TM;
using static Google.Apis.Requests.BatchRequest;
using BaseApi.Models.DataInfo;
using BaseApi.Models.Response;
using BaseApi.Utils;
using BaseApi.Models.Request;
using BaseApi.Enums;
using BaseApi.Repository;

namespace BaseApi.Service
{
    public interface IAuthService
    {
        BaseResponseMessage<LogInResp> login(LogInRequest request);

        BaseResponse LogOut(string token);

        bool LogOutAllSession(string accountUuid);
    }
    public class AuthService : BaseService, IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IAccountRepository accountRepository, ISessionRepository sessionRepository, IRoleRepository roleRepository) : base(dbContext, mapper, configuration)
        {
            _accountRepository = accountRepository;
            _sessionRepository = sessionRepository;
            _roleRepository = roleRepository;
        }

        private LogInResp CheckInfor(LogInRequest request)
        {
            var loginResp = new LogInResp();
            var acc = _accountRepository.GetByUsername(request.UserName);

            if (acc == null)
            {
                throw new ErrorException(ErrorCode.ACCOUNT_NOTFOUND);
            }

            // Define a dictionary for valid role codes based on request type
            var roleMap = new Dictionary<int, string>
        {
            { 1, "R1" },
            { 2, "R2" },
            { 3, "R3" }
        };

            // Check if request type is valid and role code matches
            if (!roleMap.TryGetValue(request.Type, out var requiredRoleCode) || acc.RoleUu.Code != requiredRoleCode)
            {
                throw new ErrorException(ErrorCode.FORBIDDEN_LOGIN);
            }

            if (acc.Status == 2 )
            {
                throw new ErrorException(ErrorCode.ACCOUNT_LOCKED);
            }

            if (acc.Status == 0)
            {
                throw new ErrorException(ErrorCode.ACCOUNT_DELETED);
            }

            if (acc.Password != request.Password)
            {
                throw new ErrorException(ErrorCode.INVALID_PASS);
            }

            // Populate response
            loginResp.Uuid = acc.Uuid;
            loginResp.UserName = acc.UserName;
            loginResp.Fullname = acc.UserUu.Fullname;
            loginResp.UserUuid = acc.UserUuid;
            loginResp.RolesUuid = acc.RoleUuid;

            return loginResp;
        }


        public BaseResponseMessage<LogInResp> login(LogInRequest request)
        {
            var response = new BaseResponseMessage<LogInResp>();



            request.UserName = request.UserName.Trim().ToLower();


            var LogInRes = CheckInfor(request);


            var _token = TokenManager.getTokenInfoByUser(request.UserName);

            if (_token != null)
            {
                TokenManager.removeToken(_token.Token);
            }


            _token = new TokenInfo()
            {
                Token = Guid.NewGuid().ToString(),
                UserName = LogInRes.UserName,
                AccountUuid = LogInRes.Uuid,
                RoleName = _roleRepository.GetRoleByuuid(LogInRes.RolesUuid).Name,
                UserUuid = LogInRes.UserUuid,
            };
            _token.ResetExpired();
            LogInRes.Token = _token.Token;
            response.Data = LogInRes;

            TokenManager.addToken(_token);
            TokenManager.clearToken();


            var oldSessions = _sessionRepository.GetListSessionByAccountUuid(LogInRes.Uuid);
            if (oldSessions != null && oldSessions.Count > 0)
            {
                foreach (var session in oldSessions)
                {
                    session.Status = 1;
                    _sessionRepository.UpdateItem(session);
                }
            }


            var newSession = new Sessions()
            {
                Uuid = _token.Token,
                AccountUuid = _token.AccountUuid,
                TimeLogin = DateTime.Now,
                Status = 0
            };

            _sessionRepository.CreateItem(newSession);

            return response;


        }
        public bool LogOutAllSession(string accountUuid)
        {

            return _sessionRepository.LogOutAllSession(accountUuid);
        }

        public Sessions GetTokenInfo(string token)
        {

            var sesion = _sessionRepository.GetSessionByUuid(token);


            return sesion;
        }

        public BaseResponse LogOut(string token)
        {
            var response = new BaseResponse();
            var tokenInfo = TokenManager.getTokenInfoByToken(token);
            if (tokenInfo != null)
            {

                TokenManager.removeToken(token);


                var oldSessions = _sessionRepository.GetListSessionByAccountUuid(tokenInfo.AccountUuid);

                if (oldSessions != null && oldSessions.Count > 0)
                {
                    foreach (var session in oldSessions)
                    {
                        session.Status = 1;
                        session.TimeLogout = DateTime.Now;
                    }


                    _sessionRepository.UpdateItem(oldSessions);
                    return response;
                }
                else
                {
                    throw new ErrorException(ErrorCode.TOKEN_INVALID);
                }
            }
            else
            {
                throw new ErrorException(ErrorCode.TOKEN_INVALID);
            }
        }
    }
}
