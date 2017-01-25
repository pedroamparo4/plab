using Abp.Domain.Entities;
using PayLab_BP.Lots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayLab_BP.Payments
{
    public class Payment : Entity<int>, IMustHaveTenant
    {
        public virtual Guid TransactionNumber { get; set; }
        public virtual Int64? ContractNumber { get; set; }
        public virtual Double Amount { get; set; }
        public virtual int SupplierId { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual int LotId { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual int PaymentTypeId { get; set; }
        public virtual DateTime? Date { get; set; }
        public int TenantId { get; set; }

        public Payment()
        {
            Date = DateTime.Now;
            TransactionNumber = Guid.NewGuid();
        }
    }
}
