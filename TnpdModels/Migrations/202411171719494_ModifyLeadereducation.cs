namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyLeadereducation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leaders", "Education", c => c.String(nullable: false, maxLength: 3000));
            AlterColumn("dbo.Leaders", "Experience", c => c.String(nullable: false, maxLength: 3000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leaders", "Experience", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Leaders", "Education", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
