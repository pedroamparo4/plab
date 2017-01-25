using Abp.Domain.Entities;
using PayLab_BP.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayLab_BP.Lots
{
    public class Lot : Entity<int>
    {
        public virtual string Number { get; set; }
        public virtual DateTime DateFrom { get; set; }
        public virtual DateTime DateTo { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public DateTime CreationDate { get; set; }

        public Lot()
        {
            Number = String.Format("{0}", DateTime.Now.ToString("yyyyMMdd"));
            DateFrom = DateTime.Now.Date.AddSeconds(1);
            DateTo = DateTime.Now.Date.AddDays(1).AddTicks(-1).AddSeconds(-1);
            Payments = new HashSet<Payment>();
            CreationDate = DateTime.UtcNow;
        }
    }
}
