namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLeaderNameImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaders", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Leaders", "UpImage1", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leaders", "UpImage1");
            DropColumn("dbo.Leaders", "Name");
        }
    }
}
