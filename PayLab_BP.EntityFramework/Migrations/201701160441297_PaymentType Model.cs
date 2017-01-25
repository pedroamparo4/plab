namespace PayLab_BP.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentTypeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PaymentType_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Payments", "PaymentTypeId", c => c.Int(nullable: true));
            CreateIndex("dbo.Payments", "PaymentTypeId");
            AddForeignKey("dbo.Payments", "PaymentTypeId", "dbo.PaymentTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "PaymentTypeId", "dbo.PaymentTypes");
            DropIndex("dbo.Payments", new[] { "PaymentTypeId" });
            DropColumn("dbo.Payments", "PaymentTypeId");
            DropTable("dbo.PaymentTypes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PaymentType_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
