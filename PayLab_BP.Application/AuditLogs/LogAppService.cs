using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using PayLab_BP.Authorization;
using PayLab_BP.AbpAuditLogs.Dto;
using System.Linq;
using PayLab_BP.AbpAuditLogs;
using PayLab_BP.Users;
using PayLab_BP.MultiTenancy;

namespace PayLab_BP.Logs
{
    [AbpAuthorize(PermissionNames.Logs)]
    public class LogAppService : PayLab_BPAppServiceBase, ILogAppService
    {
        private readonly IRepository<Abp.Auditing.AuditLog, long> _logRepository;
        private readonly IUserAppService _userService;
        private readonly ITenantAppService _tenantService;

        public LogAppService(IRepository<Abp.Auditing.AuditLog, long> logRepository, IUserAppService userService, ITenantAppService tenantService)
        {
            _logRepository = logRepository;
            _userService = userService;
            _tenantService = tenantService;
        }

        public IEnumerable<Abp.Auditing.AuditLog> GetLogs(string dtFrom, string dtTo)
        {
            DateTime dtF;
            DateTime dtT;

            try
            {
                dtF = Convert.ToDateTime(dtFrom);
                dtT = Convert.ToDateTime(dtTo);
            }
            catch
            {
                throw new Exception("Could not convert data to date format");
            }

            var logs = _logRepository.GetAllList().AsQueryable().Where(x => x.ExecutionTime.Date >= dtF && x.ExecutionTime.Date <= dtT).Take(250).OrderByDescending(o => o.Id);
/*

var logs = _logRepository.GetAllList().AsQueryable().Where(x => x.ExecutionTime.Date >= dtF && x.ExecutionTime.Date <= dtT).Take(250).OrderByDescending(o => o.Id).Select(s => new AbpAuditLogsDto
            {
                ServiceName = s.ServiceName,
                BrowserInfo = s.BrowserInfo,
                ClientIpAddress = s.ClientIpAddress,
                ClientName = s.ClientName,
                ExecutionDuration = s.ExecutionDuration,
                ExecutionTime = s.ExecutionTime.ToString(),
                MethodName = s.MethodName,
                Parameters = s.Parameters,
                TenantId = s.TenantId,
                UserId = s.UserId,
                UserName = _userService.GetUserName(s.UserId),
                TenantName = _tenantService.GetTenantName(s.TenantId)

            }).ToList();*/

            return logs;
        }
    }
}