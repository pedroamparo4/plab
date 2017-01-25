using PayLab_BP.Payments.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PayLab_BP.AbpAuditLogs.Dto
{
    public class AbpAuditLogsDto
    {
        public string BrowserInfo { get; set; }
        //
        // Summary:
        //     IP address of the client.
        public string ClientIpAddress { get; set; }
        //
        // Summary:
        //     Name (generally computer name) of the client.
        public string ClientName { get; set; }

        //
        // Summary:
        //     Total duration of the method call.
        public int ExecutionDuration { get; set; }
        //
        // Summary:
        //     Start time of the method execution.
        public string ExecutionTime { get; set; }
        //
        // Summary:
        //     ImpersonatorTenantId.
        public int? ImpersonatorTenantId { get; set; }
        //
        // Summary:
        //     ImpersonatorUserId.
        public long? ImpersonatorUserId { get; set; }
        //
        // Summary:
        //     Executed method name.
        public string MethodName { get; set; }
        //
        // Summary:
        //     Calling parameters.
        public string Parameters { get; set; }
        //
        // Summary:
        //     Service (class/interface) name.
        public string ServiceName { get; set; }
        //
        // Summary:
        //     TenantId.
        public int? TenantId { get; set; }

        //
        // Summary:
        //     Service (class/interface) name.
        public string TenantName { get; set; }

        //
        // Summary:
        //     UserId.
        public long? UserId { get; set; }

        //
        // Summary:
        //     Service (class/interface) name.
        public string UserName { get; set; }
    }
}
