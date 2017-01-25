using System.Threading.Tasks;
using Abp.Application.Services;
using System.Collections.Generic;
using PayLab_BP.AbpAuditLogs.Dto;

namespace PayLab_BP.AbpAuditLogs
{
    public interface ILogAppService : IApplicationService
    {
        IEnumerable<Abp.Auditing.AuditLog> GetLogs(string dtFrom, string dtTo);
    }
}