using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PayLab_BP.MultiTenancy.Dto;

namespace PayLab_BP.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task <TenantListDto> GetTenant(int id);

        Task CreateTenant(CreateTenantInput input);

        Task SaveEditTenant(EditTenantInput input);

        Task ChangeTenantStatus(int tenantId);

        string GetTenantName(int? tenantId);
    }
}
