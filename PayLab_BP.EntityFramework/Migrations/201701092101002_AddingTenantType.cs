namespace PayLab_BP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTenantType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpTenants", "TenantType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpTenants", "TenantType");
        }
    }
}
