namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BigBinnerLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BigBanners", "URL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BigBanners", "URL");
        }
    }
}
