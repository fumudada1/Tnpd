namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtownid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases", "TownId", c => c.Int());
            AddForeignKey("dbo.Cases", "TownId", "dbo.Towns", "Id");
            CreateIndex("dbo.Cases", "TownId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cases", new[] { "TownId" });
            DropForeignKey("dbo.Cases", "TownId", "dbo.Towns");
            DropColumn("dbo.Cases", "TownId");
        }
    }
}
