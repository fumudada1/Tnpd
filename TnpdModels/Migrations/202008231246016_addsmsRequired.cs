namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsmsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrafficSMSCarInfoes", "CarNO", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.TrafficSMSCarInfoes", "CarOwner", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.TrafficSMSCarInfoes", "Pid", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrafficSMSCarInfoes", "Pid", c => c.String(maxLength: 10));
            AlterColumn("dbo.TrafficSMSCarInfoes", "CarOwner", c => c.String(maxLength: 10));
            AlterColumn("dbo.TrafficSMSCarInfoes", "CarNO", c => c.String(maxLength: 10));
        }
    }
}
