namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTrafficIsSendCheckname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseTraffics", "IsSend", c => c.Boolean(nullable: false));
            AlterColumn("dbo.SystemLogs", "Subject", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SystemLogs", "Subject", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.CaseTraffics", "IsSend");
        }
    }
}
