namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMailFrom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SMTPServerSettings", "MailFrom", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SMTPServerSettings", "MailFrom");
        }
    }
}
