using System.Threading.Tasks;
using Abp.Application.Services;
using PayLab_BP.Roles.Dto;

namespace PayLab_BP.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
