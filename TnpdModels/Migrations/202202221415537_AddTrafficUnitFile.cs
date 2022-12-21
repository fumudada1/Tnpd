namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrafficUnitFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseTraffics", "UnitFile", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseTraffics", "UnitFile");
        }
    }
}
