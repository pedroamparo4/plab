using Abp.Domain.Entities;
using PayLab_BP.Lots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayLab_BP.Payments
{
    public class PaymentType : Entity<int>, IMustHaveTenant
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public int TenantId { get; set; }
    }
}
