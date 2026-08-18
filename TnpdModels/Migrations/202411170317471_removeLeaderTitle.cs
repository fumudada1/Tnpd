namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeLeaderTitle : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Leaders", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leaders", "Title", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
