namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addContacts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wandas", "Contacts", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Wandas", "Contacts");
        }
    }
}
