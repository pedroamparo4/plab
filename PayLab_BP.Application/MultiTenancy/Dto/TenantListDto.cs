using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace PayLab_BP.MultiTenancy.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantListDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }

        public int? TenantType { get; set; }

        public bool IsActive { get; set; }
    }
}