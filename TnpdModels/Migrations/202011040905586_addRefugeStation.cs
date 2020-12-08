namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRefugeStation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefugeStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DIstrict = c.String(nullable: false, maxLength: 10),
                        Village = c.String(nullable: false, maxLength: 10),
                        Subject = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        Number = c.Int(nullable: false),
                        Precinct = c.String(nullable: false, maxLength: 20),
                        Twd97X = c.String(maxLength: 20),
                        Twd97Y = c.String(maxLength: 20),
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
            DropTable("dbo.RefugeStations");
        }
    }
}
