namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLeaders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeaderCatalogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WebSiteId = c.Int(),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebSiteNames", t => t.WebSiteId)
                .Index(t => t.WebSiteId);
            
            CreateTable(
                "dbo.Leaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LeaderCatalogId = c.Int(),
                        Title = c.String(nullable: false, maxLength: 50),
                        Education = c.String(nullable: false, maxLength: 100),
                        Experience = c.String(nullable: false, maxLength: 500),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LeaderCatalogs", t => t.LeaderCatalogId)
                .Index(t => t.LeaderCatalogId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Leaders", new[] { "LeaderCatalogId" });
            DropIndex("dbo.LeaderCatalogs", new[] { "WebSiteId" });
            DropForeignKey("dbo.Leaders", "LeaderCatalogId", "dbo.LeaderCatalogs");
            DropForeignKey("dbo.LeaderCatalogs", "WebSiteId", "dbo.WebSiteNames");
            DropTable("dbo.Leaders");
            DropTable("dbo.LeaderCatalogs");
        }
    }
}
