using System;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using Abp.Runtime.Validation;

namespace PayLab_BP.MultiTenancy.Dto
{
    [AutoMapTo(typeof(Tenant))]
    public class EditTenantInput : ICustomValidate
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(Tenant.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public int? TenantType { get; set; }

        public int? Id { get; set; }

        [MaxLength(AbpTenantBase.MaxConnectionStringLength)]
        public string ConnectionString { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (TenantType == null || TenantType <= 0)
            {
                context.Results.Add(new ValidationResult("You have to select the company/supplier type"));
            }
        }
    }
}