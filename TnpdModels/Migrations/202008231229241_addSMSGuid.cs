namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSMSGuid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrafficSMS", "CaseGuid", c => c.String(maxLength: 200));
            AddColumn("dbo.TrafficSMS", "Times", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrafficSMS", "Times");
            DropColumn("dbo.TrafficSMS", "CaseGuid");
        }
    }
}
