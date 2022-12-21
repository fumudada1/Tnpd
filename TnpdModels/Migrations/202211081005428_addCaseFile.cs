namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCaseFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases", "Upfile4", c => c.String(maxLength: 200));
            AddColumn("dbo.Cases", "Upfile5", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cases", "Upfile5");
            DropColumn("dbo.Cases", "Upfile4");
        }
    }
}
