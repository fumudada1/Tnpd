namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTrafficViolationItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrafficViolationItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        IsEnable = c.Int(nullable: false),
                        TrafficViolationType = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TrafficViolationItems");
        }
    }
}
