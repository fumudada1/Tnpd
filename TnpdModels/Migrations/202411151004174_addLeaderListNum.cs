namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLeaderListNum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaders", "ListNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leaders", "ListNum");
        }
    }
}
