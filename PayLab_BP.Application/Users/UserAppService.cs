using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using PayLab_BP.Authorization;
using PayLab_BP.Users.Dto;
using Microsoft.AspNet.Identity;
using System;
using Abp.Domain.Uow;
using System.Linq;
using PayLab_BP.MultiTenancy;
using PayLab_BP.MultiTenancy.Dto;

namespace PayLab_BP.Users
{
    /* THIS IS JUST A SAMPLE. */
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : PayLab_BPAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;
        private readonly ITenantAppService _tenantService;

        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager, ITenantAppService tenantService)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
            _tenantService = tenantService;
        }

        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        public async Task CreateUser(CreateUserInput input)
        {
            var user = input.MapTo<User>();

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            CheckErrors(await UserManager.CreateAsync(user));
        }

        public async Task ChangePassword(ChangeUserPasswordInput input)
        {
            var user = _userRepository.Get((long)AbpSession.UserId);
           
            //VALIDACION DEL PASSWORD
            user.Password = new PasswordHasher().HashPassword(input.Password);
            await _userRepository.UpdateAsync(user);

            //TODO: Implementar el RESET CODE y la notificación al correo electrónico <--- IMPORTANTE!
        }

        public async Task<AccountInfo> GetAccountInfo()
        {
            var user = await _userRepository.GetAsync((long)AbpSession.UserId);

            return new AccountInfo
            {
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                EmailAddress = user.EmailAddress
            };
        }

        [AbpAuthorize(PermissionNames.Pages_Users)]
        public async Task<IEnumerable<DetailedUserDto>> GetUsers()
        {
            List<User> userList = new List<User>();

            if(AbpSession.TenantId == null)//IS ADMIN
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MayHaveTenant))
                {
                    userList = await _userRepository.GetAllListAsync();
                }
            }
            else//IS A TENANT
            {
                userList = await _userRepository.GetAllListAsync();
            }

            return userList.Select(s => new DetailedUserDto
            {
                Id = s.Id,
                UserName = s.UserName,
                Name = s.Name,
                LastName = s.Surname,
                EmailAddress = s.EmailAddress,
                IsActive = s.IsActive,
                RoleType = (s.TenantId == null ? L("Administrator") : L("Supplier")),
                SupplierName = _tenantService.GetTenantName(s.TenantId)
            });
        }

        public async Task ChangeUserStatus(int userId)
        {
            User user;

            if (AbpSession.TenantId == null)//IS ADMIN
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MayHaveTenant))
                {
                    user = _userRepository.Get(userId);
                }
            }
            else//IS A TENANT
            {
                user = _userRepository.Get(userId);
            }

            if(user == null)
            {
                throw new Exception("The user could not be found");
            }

            user.IsActive = !user.IsActive;
            await _userRepository.UpdateAsync(user);
        }

        public string GetUserName(long? userId)
        {
            if(userId == null)
            {
                return "-";
            }

            var user = UserManager.FindById((long)userId);

            if (user == null)
            {
                return "-";
            }

            return user.UserName;
        }
    }
}