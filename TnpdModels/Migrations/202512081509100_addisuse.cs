namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addisuse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseMailChecks", "IsUsed", c => c.Boolean(nullable: false));
            AddColumn("dbo.CaseMailChecks", "UsedDate", c => c.DateTime());
            AddColumn("dbo.CaseMailChecks", "UsedIp", c => c.String(maxLength: 50));
            
            AddColumn("dbo.CaseMobileChecks", "IsUsed", c => c.Boolean(nullable: false));
            AddColumn("dbo.CaseMobileChecks", "UsedDate", c => c.DateTime());
            AddColumn("dbo.CaseMobileChecks", "UsedIp", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseMobileChecks", "UsedIp");
            DropColumn("dbo.CaseMobileChecks", "UsedDate");
            DropColumn("dbo.CaseMobileChecks", "IsUsed");
            
            DropColumn("dbo.CaseMailChecks", "UsedIp");
            DropColumn("dbo.CaseMailChecks", "UsedDate");
            DropColumn("dbo.CaseMailChecks", "IsUsed");
        }
    }
}
