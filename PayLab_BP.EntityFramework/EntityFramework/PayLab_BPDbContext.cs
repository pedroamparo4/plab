using System.Data.Common;
using Abp.Zero.EntityFramework;
using PayLab_BP.Authorization.Roles;
using PayLab_BP.MultiTenancy;
using PayLab_BP.Users;
using PayLab_BP.Lots;
using System.Data.Entity;
using PayLab_BP.Payments;
using Abp.Auditing;

namespace PayLab_BP.EntityFramework
{
    public class PayLab_BPDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        // Lots
        public virtual IDbSet<Lot> Lots { get; set; }

        // Payments
        public virtual IDbSet<Payment> Payments { get; set; }

        // Payment Types
        public virtual IDbSet<PaymentType> PaymentTypes { get; set; }


        //AuditLogs
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual IDbSet<Abp.Auditing.AuditLog> AuditLogs { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public PayLab_BPDbContext()
            : base("PayLab_BP")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in PayLab_BPDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of PayLab_BPDbContext since ABP automatically handles it.
         */
        public PayLab_BPDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public PayLab_BPDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
