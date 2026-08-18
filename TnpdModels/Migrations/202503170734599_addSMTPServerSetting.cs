namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSMTPServerSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SMTPServerSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Address = c.String(nullable: false, maxLength: 200),
                        MailAccount = c.String(nullable: false, maxLength: 200),
                        MailPassword = c.String(nullable: false, maxLength: 200),
                        Port = c.Int(nullable: false),
                        EnableSsl = c.Boolean(nullable: false),
                        UseDefaultCredentials = c.Boolean(nullable: false),
                        SmtpDeliveryMethod = c.Int(nullable: false),
                        Enable = c.Boolean(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SMTPServerSettings");
        }
    }
}
