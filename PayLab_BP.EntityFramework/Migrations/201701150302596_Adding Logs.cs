namespace PayLab_BP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingLogs : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Logs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        ServiceName = c.String(),
                        MethodName = c.String(),
                        Parameters = c.String(),
                        ExecutionTime = c.DateTime(nullable: false),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientIpAddress = c.String(),
                        ClientName = c.String(),
                        BrowserInfo = c.String(),
                        Exception = c.String(),
                        InpersonatorUserId = c.Long(),
                        InpersonatorTenantId = c.Int(),
                        CustomData = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
