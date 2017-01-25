using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Runtime.Validation;

namespace PayLab_BP.Users.Dto
{
    public class ChangeUserPasswordInput
    {
        [Required]
        [StringLength(User.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        [Required]
        [StringLength(User.MaxPlainPasswordLength)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [DisableAuditing]
        public string PasswordConfirm { get; set; }
    }
}