using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.WebApi;
using System.Security.Cryptography;
using System.Text;
using BaseApi.Configuaration;
using BaseApi.Databases.TM;
using BaseApi.Enums;
using BaseApi.Extensions;
using BaseApi.Models.DataInfo;
using BaseApi.Models.Request;
using BaseApi.Models.Response;
using BaseApi.Repository;
using BaseApi.Utils;
using BaseApi.Service;
using static Google.Apis.Requests.BatchRequest;

namespace BaseApi.Service
{
    public interface IAccountService
    {

        BaseResponseMessageItem<CategoryAccountDTO> GetCategoryAccount(GetCategoryAccountRequest request);

        BaseResponseMessagePage<AccountDTO> GetPageListAccount(FindAccountPageRequest request);

        BaseResponseMessage<DetailAccountDTO> GetDetailAccount(string uuid);
        BaseResponse SendOtpEmail(SendOtpRequest request);
        BaseResponse EnterOtp(EnterOtpRequest request);
        Account ChangePasswordForget(ChangeForgetPass request);

        Account ChangePassWord(ChangePassRequest request);
        Account Register(RegisterAccountRequest request);

        Account UpdateAccount(UpdateAccountRequest request);

        BaseResponse UpdateStatus(string uuid);

        BaseResponse LockAcc(UuidRequest request);

        BaseResponse UpdateSpecial(string uuid);
    }
    public class AccountService : BaseService, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOtpService _otpService;
        private readonly ISessionRepository _sessionRepository;
        private readonly IEmailService _emailService;

        public AccountService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IAccountRepository accountRepository, IUserRepository userRepository, IOtpService otpService, IEmailService emailService, ISessionRepository sessionRepository) : base(dbContext, mapper, configuration)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _otpService = otpService;
            _sessionRepository = sessionRepository;
            _emailService = emailService;

        }
        public BaseResponseMessageItem<CategoryAccountDTO> GetCategoryAccount(GetCategoryAccountRequest request)
        {
            var response = new BaseResponseMessageItem<CategoryAccountDTO>();
            var lstAccount = _accountRepository.GetListAccount(request);
            if (lstAccount != null)
            {
                var lstCategoryAccount = _mapper.Map<List<CategoryAccountDTO>>(lstAccount);
                response.Data = lstCategoryAccount;
                return response;
            }
            return response;
        }
        public BaseResponseMessagePage<AccountDTO> GetPageListAccount(FindAccountPageRequest request)
        {
            var response = new BaseResponseMessagePage<AccountDTO>();
            var lstAccount = _accountRepository.GetPageListAccount(request);
            var count = _accountRepository.Count(request);
            if (lstAccount != null && count > 0)
            {
                var result = lstAccount.OrderByDescending(x => x.Id)
                                    .TakePage(request.Page, request.PageSize);

                var lstContractorDTO = _mapper.Map<List<AccountDTO>>(result);

                response.Data.Items = lstContractorDTO;
                response.Data.Pagination = new Paginations()
                {
                    TotalPage = result.TotalPages,
                    TotalCount = result.TotalCount,
                };
            }
            return response;
        }
        public BaseResponseMessage<DetailAccountDTO> GetDetailAccount(string uuid)
        {
            var response = new BaseResponseMessage<DetailAccountDTO>();
            var account = _accountRepository.GetByUuid(uuid);

            if (account == null)
            {
                throw new ErrorException(ErrorCode.ACCOUNT_NOTFOUND);
            }
            var detailAccountDto = _mapper.Map<DetailAccountDTO>(account);

            response.Data = detailAccountDto;
            return response;
        }
        public BaseResponse SendOtpEmail(SendOtpRequest request)
        {
            var response = new BaseResponse();
            var account = _accountRepository.GetByEmail(request.Email);
            if (account == null)
            {
                throw new ErrorException(ErrorCode.ACCOUNT_NOTFOUND);
            }

            var otp = _otpService.GenerateAndStoreOtp(request.Email,account.Uuid);
            _emailService.SendOtpEmailAsync(request.Email,otp);
            return response;
        }

        public BaseResponse EnterOtp(EnterOtpRequest request)
        {
            var response = new BaseResponse();
            var isValid = _otpService.ValidateOtp(request.Email, request.Otp);
            var isExpiredOtp = _otpService.IsOtpExpired(request.Email, request.Otp);
            var isUsedOtp = _otpService.IsUsed(request.Email, request.Otp);
            if (!isValid)
            {
                throw new ErrorException(ErrorCode.INVALID_OTP);
            }

            if (isExpiredOtp)
            {
                throw new ErrorException(ErrorCode.EXPIRED_OTP);
            }
            if (isUsedOtp)
            {
                throw new ErrorException(ErrorCode.OTP_USED);
            }
            
            return response;
        }

        public Account ChangePasswordForget(ChangeForgetPass request)
        {
            return ExecuteInTransaction(() =>
            {


                var account = _accountRepository.GetByEmail(request.Email);
                if (account != null)
                {
                    var isValid = _otpService.ValidateOtp(request.Email, request.Otp);
                    var isExpiredOtp = _otpService.IsOtpExpired(request.Email, request.Otp);
                    if (!isValid)
                    {
                        throw new ErrorException(ErrorCode.INVALID_OTP);
                    }

                    if (isExpiredOtp)
                    {
                        throw new ErrorException(ErrorCode.EXPIRED_OTP);
                    }
                    account.Password = request.NewPass;
                    account = _accountRepository.UpdateItem(account);
                    _otpService.changeState(request.Otp);
                    return account;
                }
                else
                {

                    throw new ErrorException(ErrorCode.ACCOUNT_NOTFOUND);
                }


            });
        }

        public Account ChangePassWord(ChangePassRequest request)
        {
            return ExecuteInTransaction(() =>
            {


                var account = _accountRepository.GetByUuid(request.Uuid);
                if (account != null)
                {
                    if (account.Password == request.OldPassword)
                    {
                        account.Password = request.NewPassword;
                        account = _accountRepository.UpdateItem(account);

                        return account;
                    }
                    else
                    {
                        throw new ErrorException(ErrorCode.OLD_PASS_WRONG);
                    }
                }
                else
                {
                    throw new ErrorException(ErrorCode.ACCOUNT_NOTFOUND);
                }


            });
        }

        public Account Register(RegisterAccountRequest request)
        {
            return ExecuteInTransaction(() =>
            {
                var user = _userRepository.GetByUuid(request.UserUuid);
                var acc = _accountRepository.GetByUserUuid(request.UserUuid);
                var accWithUsername = _accountRepository.GetByUsername(request.Username);

                if (user == null)
                {
                    throw new ErrorException(ErrorCode.USER_NOTFOUND);
                }

                if (acc != null)
                {
                    throw new ErrorException(ErrorCode.USER_ALREADY_HAVE_ACC);
                }

                if (accWithUsername != null)
                {
                    throw new ErrorException(ErrorCode.USERNAME_ALREADY_TAKEN);
                }

                var newAcc = new Account
                {
                    UserName = request.Username,
                    UserUuid = request.UserUuid,
                    RoleUuid = request.RoleUuid,
                    Password = request.Password
                };
                user.UserName = newAcc.UserName;

                return _accountRepository.CreateItem(newAcc);
            });
        }

        public Account UpdateAccount(UpdateAccountRequest request)
        {
            return ExecuteInTransaction(() =>
            {
                var acc = _accountRepository.GetByUuid(request.Uuid);
                if(acc == null)
                {
                    throw new ErrorException(ErrorCode.ACCOUNT_NOTFOUND);
                }
               
                acc.RoleUuid =request .RoleUuid;
                return _accountRepository.UpdateItem(acc);
            });
        }

        public BaseResponse UpdateStatus(string uuid)
        {
            return ExecuteInTransaction(() =>
            {
                var respone = new BaseResponse();
                var acc = _accountRepository.GetByUuid(uuid)
                          ?? throw new ErrorException(ErrorCode.ACCOUNT_NOTFOUND);
                 if(acc.UserUu.UserProjects.Any(x=>x.Status != 0)) throw new ErrorException(ErrorCode.CANT_DELETE_ACC);
                
                acc.Status = (sbyte)(acc.Status == 0 ? 1 : 0);
                _accountRepository.UpdateItem(acc);
                if (acc.Status == 0)
                {
                    var oldSessions = _sessionRepository.GetListSessionByAccountUuid(acc.Uuid);
                    if (oldSessions != null && oldSessions.Count > 0)
                    {
                        foreach (var session in oldSessions)
                        {
                            session.Status = 1;
                            session.TimeLogout = DateTime.Now;
                        }
                        var _token = TokenManager.getTokenInfoByUser(acc.UserName);

                        if (_token != null)
                        {
                            TokenManager.removeToken(_token.Token);
                        }
                        _sessionRepository.UpdateItem(oldSessions);
                    }
                };
                
                return respone;
            });
        }

        public BaseResponse UpdateSpecial(string uuid)
        {
            return ExecuteInTransaction(() =>
            {
                var respone = new BaseResponse();
                var acc = _accountRepository.GetByUuid(uuid)
                          ?? throw new ErrorException(ErrorCode.ACCOUNT_NOTFOUND);
               

               
                _accountRepository.UpdateItem(acc);

                return respone;
            });
        }

        public BaseResponse LockAcc(UuidRequest request)
        {
            return ExecuteInTransaction(() =>
            {
                var respone = new BaseResponse();
                var acc = _accountRepository.GetByUuid(request.Uuid)
                          ?? throw new ErrorException(ErrorCode.ACCOUNT_NOTFOUND);

                acc.Status = (sbyte)(acc.Status == 2 ? 1 : 2);
                _accountRepository.UpdateItem(acc);
                if (acc.Status == 2)
                {
                    var oldSessions = _sessionRepository.GetListSessionByAccountUuid(acc.Uuid);
                    if (oldSessions != null && oldSessions.Count > 0)
                    {
                        foreach (var session in oldSessions)
                        {
                            session.Status = 1;
                            session.TimeLogout = DateTime.Now;
                        }
                        var _token = TokenManager.getTokenInfoByUser(acc.UserName);

                        if (_token != null)
                        {
                            TokenManager.removeToken(_token.Token);
                        }
                        _sessionRepository.UpdateItem(oldSessions);
                    }
                };
                return respone;
            });
        }
    }
}
