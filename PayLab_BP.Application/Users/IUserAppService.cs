using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PayLab_BP.Users.Dto;
using System.Collections.Generic;

namespace PayLab_BP.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<IEnumerable<DetailedUserDto>> GetUsers();

        Task CreateUser(CreateUserInput input);

        Task ChangePassword(ChangeUserPasswordInput input);

        Task<AccountInfo> GetAccountInfo();

        Task ChangeUserStatus(int userId);

        string GetUserName(long? userId);
    }
}