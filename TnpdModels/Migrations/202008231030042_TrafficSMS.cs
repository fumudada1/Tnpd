namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrafficSMS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrafficSMS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Mobile = c.String(nullable: false, maxLength: 10),
                        CheckCode = c.String(maxLength: 10),
                        IsAction = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrafficSMSCarInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarNO = c.String(maxLength: 10),
                        CarType = c.Int(nullable: false),
                        CarOwner = c.String(maxLength: 10),
                        Pid = c.String(maxLength: 10),
                        CarAllow = c.Int(nullable: false),
                        checkStatus = c.Int(nullable: false),
                        TrafficSMSId = c.Int(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrafficSMS", t => t.TrafficSMSId)
                .Index(t => t.TrafficSMSId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TrafficSMSCarInfoes", new[] { "TrafficSMSId" });
            DropForeignKey("dbo.TrafficSMSCarInfoes", "TrafficSMSId", "dbo.TrafficSMS");
            DropTable("dbo.TrafficSMSCarInfoes");
            DropTable("dbo.TrafficSMS");
        }
    }
}
