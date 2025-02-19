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
using BaseApi.Extensions;
using System;

using User = BaseApi.Databases.TM.User;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.TeamFoundation.Common;


namespace BaseApi.Service
{
    public interface IUserService
    {
        User UpdateStatus(string uuid);
        User UpsertUser(UpsertUserRequest request);

        BaseResponseMessage<DetailUserDTO> GetDetailUser(string uuid);

        BaseResponseMessageItem<CategoryUserDTO> GetCategoryUser(GetCategoryUserRequest request);
        BaseResponseMessagePage<UserDTO> GetPageListUser(FindUserPageRequest request, TokenInfo token);


    }

    [ScopedService]
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IUserRepository userRepository) : base(dbContext, mapper, configuration)
        {
            _userRepository = userRepository;
        }

        public BaseResponseMessageItem<CategoryUserDTO> GetCategoryUser(GetCategoryUserRequest request)
        {
            var response = new BaseResponseMessageItem<CategoryUserDTO>();
            var user = _userRepository.GetListUser(request);
            if (user != null)
            {
                var lstCategoryUser = _mapper.Map<List<CategoryUserDTO>>(user);
                response.Data = lstCategoryUser;
                return response;
            }
            return response;
        }
        public BaseResponseMessagePage<UserDTO> GetPageListUser(FindUserPageRequest request, TokenInfo token)
        {
            var response = new BaseResponseMessagePage<UserDTO>();
            var lstUser = _userRepository.GetPageListUser(request, token);
            var count = _userRepository.Count(request, token);
            if (lstUser != null && count > 0)
            {
                var result = lstUser.OrderByDescending(x => x.Id)
                                    .TakePage(request.Page, request.PageSize);

                var lstUserDTO = _mapper.Map<List<UserDTO>>(result);

                response.Data.Items = lstUserDTO;
                response.Data.Pagination = new Paginations()
                {
                    TotalPage = result.TotalPages,
                    TotalCount = result.TotalCount,
                };
            }
            return response;
        }
        public BaseResponseMessage<DetailUserDTO> GetDetailUser(string uuid)
        {
            var response = new BaseResponseMessage<DetailUserDTO>();
            var user = _userRepository.GetByUuid(uuid);

            if (user == null)
            {
                throw new ErrorException(ErrorCode.USER_NOTFOUND);
            }
            var detailUserDto = _mapper.Map<DetailUserDTO>(user);
            detailUserDto.Address = string.Join(", ", new[]
                            {
                                detailUserDto.Address?.Trim(),
                                detailUserDto.Xa?.Name?.Trim(),
                                detailUserDto.QH?.Name?.Trim(),
                                detailUserDto.TP?.Name?.Trim()
                            }.Where(part => !string.IsNullOrEmpty(part)));
            response.Data = detailUserDto;
            return response;
        }
        public User UpdateStatus(string uuid)
        {
            return ExecuteInTransaction(() =>
            {
                var user = _userRepository.GetByUuid(uuid);
                if (user == null)
                {
                    throw new ErrorException(ErrorCode.USER_NOTFOUND);
                }
                if (user.UserProjects.Any(x=>x.Status != 0))
                {
                    throw new ErrorException(ErrorCode.CANT_DELETE_USER);
                }
                if (user.Account.Any(x => x.Status != 0))
                {
                    user.Account.FirstOrDefault().Status = 0;
                }
                user.Status = (sbyte)(user.Status == 1 ? 0 : 1);

                return _userRepository.UpdateItem(user);
            });
        }


        public User UpsertUser(UpsertUserRequest request)
        {
            return ExecuteInTransaction(() =>
            {
                var userByEmail = _userRepository.GetByEmail(request.Email);
                var userByPhone = _userRepository.GetByPhone(request.Phone);
                if (string.IsNullOrEmpty(request.Uuid))
                {
                    if(userByEmail != null && userByEmail.Any(u => u.Status != 0))
                    {
                        throw new ErrorException(ErrorCode.CANT_CREATE_USER);
                    }
                    if(userByPhone != null && userByPhone.Any(u => u.Status != 0))
                    {
                        throw new ErrorException(ErrorCode.CANT_CREATE_USER);
                    }
                    // Create new user
                    var newUser = new User()
                    {
                        Email = request.Email,
                        Birthday = request.Birthday == null ? null : request.Birthday,
                        Address = request.Address,
                        Fullname = request.FullName,
                        Phone = string.IsNullOrEmpty(request.Phone) ? null : request.Phone,
                        Gender = request.Gender,
                        Maqh = string.IsNullOrEmpty(request.Maqh) ? null : request.Maqh,
                        Matp = string.IsNullOrEmpty(request.Matp) ? null : request.Matp,
                       Xaid = string.IsNullOrEmpty(request.Xaid) ? null : request.Xaid,
                        Note = request.Note
                    };

                    var create = _userRepository.CreateItem(newUser);
                    create.Code = "U" + create.Id;

                    return _userRepository.UpdateItem(create);
                }
                else
                {
                    // Update existing user
                    var user = _userRepository.GetByUuid(request.Uuid);

                    if (user == null)
                    {
                        throw new ErrorException(ErrorCode.USER_NOTFOUND);
                    }
                    if (userByEmail != null && userByEmail.Any(u => u.Status != 0) && user.Email != request.Email)
                    {
                        throw new ErrorException(ErrorCode.CANT_CREATE_USER);
                    }
                    if (userByPhone != null && userByPhone.Any(u => u.Status != 0) && user.Phone != request.Phone)
                    {
                        throw new ErrorException(ErrorCode.CANT_CREATE_USER);
                    }
                    user.Birthday = request.Birthday == null ? null : request.Birthday;
                    user.Address = request.Address;
                    user.Fullname = request.FullName;
                    user.Phone = string.IsNullOrEmpty(request.Phone) ? null : request.Phone;
                    user.Gender = request.Gender;
                    user.Maqh = string.IsNullOrEmpty(request.Maqh) ? null : request.Maqh;
                    user.Email = request.Email;
                    user.Matp = string.IsNullOrEmpty(request.Matp) ? null : request.Matp;
                    user.Xaid = string.IsNullOrEmpty(request.Xaid) ? null : request.Xaid;
                    user.Note = request.Note;
                    return _userRepository.UpdateItem(user);
                }
            });
        }

    }
}
