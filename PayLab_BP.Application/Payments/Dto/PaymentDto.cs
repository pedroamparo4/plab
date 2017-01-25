using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayLab_BP.Payments.Dto
{
    public class PaymentDto
    {
        public Guid TransactionNumber { get; set; }
        public Int64? ContractNumber { get; set; }
        public Double Amount { get; set; }
        public string TenantName { get; set; }
        public int LotId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string PaymentType { get; set; }
    }
}
