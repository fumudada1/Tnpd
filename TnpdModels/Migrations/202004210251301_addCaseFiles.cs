namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCaseFiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases", "Upfile1", c => c.String(maxLength: 200));
            AddColumn("dbo.Cases", "Upfile2", c => c.String(maxLength: 200));
            AddColumn("dbo.Cases", "Upfile3", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cases", "Upfile3");
            DropColumn("dbo.Cases", "Upfile2");
            DropColumn("dbo.Cases", "Upfile1");
        }
    }
}
