namespace PayLab_BP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCodetoPaymentType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentTypes", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentTypes", "Code");
        }
    }
}
