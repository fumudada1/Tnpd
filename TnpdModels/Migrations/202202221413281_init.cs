namespace TnpdModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Alias = c.String(maxLength: 50),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 50),
                        Password = c.String(),
                        PasswordSalt = c.String(maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 50),
                        CName = c.String(maxLength: 50),
                        TEL = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 50),
                        URL = c.String(maxLength: 500),
                        Memo = c.String(),
                        Gender = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 200),
                        MyPic = c.String(maxLength: 50),
                        JobTitle = c.String(maxLength: 50),
                        UnitId = c.Int(nullable: false),
                        Permission = c.String(maxLength: 500),
                        ADMessage = c.String(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Permission = c.String(),
                        Alias = c.String(maxLength: 50),
                        UnitId = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SystemLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 500),
                        Poster = c.String(maxLength: 50),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MetaIndexes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 100),
                        SiteCode = c.String(maxLength: 100),
                        Theme = c.String(),
                        ThemeName = c.String(),
                        Cake = c.String(),
                        CakeName = c.String(),
                        Service = c.String(),
                        ServiceName = c.String(),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Serno = c.String(maxLength: 100),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Article = c.String(),
                        Views = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        AssignDateTime = c.DateTime(),
                        MemberId = c.Int(),
                        IsConfirm = c.Int(nullable: false),
                        WebCategoryId = c.Int(nullable: false),
                        OwnWebSiteId = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.NewsLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 200),
                        NewId = c.Int(nullable: false),
                        LinkUrl = c.String(maxLength: 500),
                        Target = c.Int(nullable: false),
                        ListNum = c.Int(nullable: false),
                        WebCategoryId = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewId, cascadeDelete: true)
                .Index(t => t.NewId);
            
            CreateTable(
                "dbo.NewsFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        NewId = c.Int(),
                        UpFile = c.String(maxLength: 500),
                        ListNum = c.Int(nullable: false),
                        WebCategoryId = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewId)
                .Index(t => t.NewId);
            
            CreateTable(
                "dbo.NewsImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        NewId = c.Int(),
                        UpFile = c.String(maxLength: 500),
                        ListNum = c.Int(nullable: false),
                        WebCategoryId = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewId)
                .Index(t => t.NewId);
            
            CreateTable(
                "dbo.NewsCatalogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Serno = c.String(maxLength: 100),
                        WebCategoryId = c.Int(),
                        MetaId = c.Int(nullable: false),
                        WebSiteId = c.Int(),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Confirm = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebNewsCatalogs", t => t.WebCategoryId)
                .ForeignKey("dbo.MetaIndexes", t => t.MetaId, cascadeDelete: true)
                .ForeignKey("dbo.WebSiteNames", t => t.WebSiteId)
                .Index(t => t.WebCategoryId)
                .Index(t => t.MetaId)
                .Index(t => t.WebSiteId);
            
            CreateTable(
                "dbo.WebNewsCatalogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Language = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Confirm = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebSiteNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 100),
                        SiteCode = c.String(maxLength: 100),
                        OrgNumber = c.String(maxLength: 100),
                        XmlDoc = c.String(),
                        ColorName = c.String(maxLength: 100),
                        UpImageUrl = c.String(maxLength: 200),
                        Enable = c.Int(nullable: false),
                        ListNum = c.Int(nullable: false),
                        SubjectEn = c.String(maxLength: 100),
                        Path = c.String(maxLength: 100),
                        Language = c.Int(nullable: false),
                        UnitId = c.Int(nullable: false),
                        PubUnitDN = c.String(maxLength: 100),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.SysMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 100),
                        Url = c.String(maxLength: 40),
                        Value = c.String(maxLength: 40),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MetaId = c.Int(nullable: false),
                        UnId = c.String(maxLength: 100),
                        Subject = c.String(maxLength: 100),
                        MemberPath = c.String(maxLength: 100),
                        Article = c.String(),
                        Views = c.Int(nullable: false),
                        Enable = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MetaIndexes", t => t.MetaId, cascadeDelete: true)
                .Index(t => t.MetaId);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        UpImageUrl = c.String(maxLength: 200),
                        BeSelect = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HomeLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        DataType = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        UpImage = c.String(maxLength: 200),
                        Url = c.String(maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Enable = c.Int(nullable: false),
                        WebSiteNameId = c.Int(),
                        OwnWebSiteId = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebSiteNames", t => t.WebSiteNameId)
                .Index(t => t.WebSiteNameId);
            
            CreateTable(
                "dbo.HomeAds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        UpImage = c.String(maxLength: 200),
                        Url = c.String(maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Enable = c.Int(nullable: false),
                        WebSiteNameId = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebSiteNames", t => t.WebSiteNameId, cascadeDelete: true)
                .Index(t => t.WebSiteNameId);
            
            CreateTable(
                "dbo.HomeThemes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        UpImage = c.String(maxLength: 200),
                        Url = c.String(maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Enable = c.Int(nullable: false),
                        WebSiteNameId = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebSiteNames", t => t.WebSiteNameId, cascadeDelete: true)
                .Index(t => t.WebSiteNameId);
            
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 100),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Villages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TownId = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 100),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Towns", t => t.TownId, cascadeDelete: true)
                .Index(t => t.TownId);
            
            CreateTable(
                "dbo.AboutLinkCatalogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Serno = c.String(maxLength: 100),
                        WebSiteId = c.Int(),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebSiteNames", t => t.WebSiteId)
                .Index(t => t.WebSiteId);
            
            CreateTable(
                "dbo.AboutLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        CategoryId = c.Int(),
                        UpImageUrl = c.String(maxLength: 200),
                        URL = c.String(),
                        Status = c.Int(nullable: false),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AboutLinkCatalogs", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.WebSiteColors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 100),
                        SiteCode = c.String(maxLength: 100),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FontSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 100),
                        BSize = c.Int(nullable: false),
                        MSize = c.Int(nullable: false),
                        SSize = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StationInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        TownId = c.Int(),
                        Subject = c.String(nullable: false, maxLength: 100),
                        Tel = c.String(maxLength: 500),
                        PostalCode = c.String(maxLength: 100),
                        Address = c.String(maxLength: 500),
                        Twd97X = c.String(maxLength: 20),
                        Twd97Y = c.String(maxLength: 20),
                        Article = c.String(),
                        ListNum = c.Int(nullable: false),
                        Serno = c.String(maxLength: 100),
                        OwnWebSiteId = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StationInfoes", t => t.ParentId)
                .ForeignKey("dbo.Towns", t => t.TownId)
                .Index(t => t.ParentId)
                .Index(t => t.TownId);
            
            CreateTable(
                "dbo.StationInfoFIles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        StationInfoId = c.Int(),
                        UpFile = c.String(maxLength: 500),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StationInfoes", t => t.StationInfoId)
                .Index(t => t.StationInfoId);
            
            CreateTable(
                "dbo.Eses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Serno = c.String(maxLength: 100),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Undertaking_Unit = c.String(maxLength: 200),
                        Apply_method = c.String(),
                        NeedDoc = c.String(),
                        Process = c.String(maxLength: 200),
                        Pdate = c.String(maxLength: 200),
                        Tel = c.String(maxLength: 200),
                        Fax = c.String(maxLength: 200),
                        Mail = c.String(maxLength: 200),
                        Remark = c.String(),
                        Online = c.String(),
                        Status = c.Int(nullable: false),
                        Memo = c.String(),
                        Views = c.Int(nullable: false),
                        OwnWebSiteId = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EseFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        EseId = c.Int(),
                        UpFile = c.String(maxLength: 500),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Eses", t => t.EseId)
                .Index(t => t.EseId);
            
            CreateTable(
                "dbo.EseCatalogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Serno = c.String(maxLength: 100),
                        WebCategoryId = c.Int(),
                        MetaId = c.Int(nullable: false),
                        WebSiteId = c.Int(),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebNewsCatalogs", t => t.WebCategoryId)
                .ForeignKey("dbo.MetaIndexes", t => t.MetaId, cascadeDelete: true)
                .ForeignKey("dbo.WebSiteNames", t => t.WebSiteId)
                .Index(t => t.WebCategoryId)
                .Index(t => t.MetaId)
                .Index(t => t.WebSiteId);
            
            CreateTable(
                "dbo.BigBanners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        UpImage = c.String(maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Enable = c.Int(nullable: false),
                        URL = c.String(),
                        WebSiteNameId = c.Int(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebSiteNames", t => t.WebSiteNameId)
                .Index(t => t.WebSiteNameId);
            
            CreateTable(
                "dbo.Wandas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        StationInfoId = c.Int(),
                        beginning = c.String(nullable: false, maxLength: 200),
                        destination = c.String(nullable: false, maxLength: 200),
                        Article = c.String(),
                        Status = c.Int(nullable: false),
                        OwnWebSiteId = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StationInfoes", t => t.StationInfoId)
                .Index(t => t.StationInfoId);
            
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseType = c.Int(nullable: false),
                        CaseID = c.String(nullable: false, maxLength: 200),
                        CaseGuid = c.String(maxLength: 200),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Content = c.String(),
                        Name = c.String(nullable: false, maxLength: 200),
                        Gender = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 200),
                        TEL = c.String(maxLength: 200),
                        Job = c.String(maxLength: 200),
                        IP = c.String(maxLength: 200),
                        WebSiteId = c.Int(),
                        Predate = c.DateTime(nullable: false),
                        Unit = c.String(maxLength: 200),
                        OrgName = c.String(maxLength: 200),
                        pcnt = c.String(maxLength: 4),
                        ODate = c.DateTime(),
                        STime = c.String(maxLength: 200),
                        ETime = c.String(maxLength: 200),
                        Pid = c.String(maxLength: 10),
                        PostalCode = c.String(maxLength: 200),
                        Address = c.String(maxLength: 200),
                        PPostalCode = c.String(maxLength: 200),
                        PAddress = c.String(maxLength: 200),
                        HomeTEL = c.String(maxLength: 200),
                        OfficeTEL = c.String(maxLength: 200),
                        CaseReportType = c.Int(nullable: false),
                        Oplace = c.String(maxLength: 200),
                        IsUnitAssign = c.Int(),
                        OArea = c.String(maxLength: 10),
                        ParentId = c.Int(),
                        ChiefMailboxUnitId = c.Int(),
                        IsAutoClose = c.Int(),
                        FilterString = c.String(maxLength: 500),
                        FilterType = c.String(maxLength: 100),
                        Upfile1 = c.String(maxLength: 200),
                        Upfile2 = c.String(maxLength: 200),
                        Upfile3 = c.String(maxLength: 200),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebSiteNames", t => t.WebSiteId)
                .ForeignKey("dbo.Cases", t => t.ParentId)
                .Index(t => t.WebSiteId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.CasePoprocs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        CaseType = c.Int(nullable: false),
                        CaseTime = c.DateTime(nullable: false),
                        UnitId = c.Int(),
                        MemberId = c.Int(),
                        Status = c.Int(nullable: false),
                        AssignDateTime = c.DateTime(),
                        AssignMemo = c.String(),
                        EndDateTime = c.DateTime(),
                        DelyMemo = c.String(),
                        delyflag = c.Int(),
                        DelyDateTime = c.DateTime(),
                        Upfile1 = c.String(maxLength: 200),
                        Type1 = c.String(maxLength: 200),
                        type2 = c.String(maxLength: 200),
                        noplyreason = c.String(),
                        process = c.String(maxLength: 10),
                        PoprocsType = c.Int(),
                        PoprocsSubType = c.Int(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.CaseId)
                .Index(t => t.UnitId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.CasePoprocLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(),
                        UnitId = c.Int(),
                        MemberId = c.Int(),
                        Action = c.String(maxLength: 200),
                        ActionMemo = c.String(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cases", t => t.CaseId)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.CaseId)
                .Index(t => t.UnitId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Casewqs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(),
                        Q1 = c.Int(),
                        Q2 = c.Int(),
                        Q3 = c.Int(),
                        IP = c.String(maxLength: 200),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cases", t => t.CaseId)
                .Index(t => t.CaseId);
            
            CreateTable(
                "dbo.CaseFilters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FilterType = c.Int(nullable: false),
                        Subject = c.String(nullable: false),
                        TypeId = c.Int(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PoprocsSubTypes", t => t.TypeId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.PoprocsSubTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false),
                        Article = c.String(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CaseTraffics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseType = c.Int(nullable: false),
                        CaseID = c.String(nullable: false, maxLength: 200),
                        CaseGuid = c.String(maxLength: 200),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Content = c.String(),
                        Name = c.String(nullable: false, maxLength: 200),
                        Pid = c.String(nullable: false, maxLength: 10),
                        Gender = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 200),
                        TEL = c.String(maxLength: 200),
                        Job = c.String(maxLength: 200),
                        Address = c.String(maxLength: 200),
                        IP = c.String(maxLength: 200),
                        UnitId = c.Int(),
                        Upfile1 = c.String(maxLength: 200),
                        Upfile2 = c.String(maxLength: 200),
                        Upfile3 = c.String(maxLength: 200),
                        Upfile4 = c.String(maxLength: 200),
                        Upfile5 = c.String(maxLength: 200),
                        Upfile6 = c.String(maxLength: 200),
                        violation_date = c.DateTime(nullable: false),
                        violation_time = c.String(maxLength: 10),
                        Predate = c.DateTime(nullable: false),
                        violation_place = c.String(maxLength: 500),
                        violation_carno = c.String(maxLength: 10),
                        violation_place_area = c.String(maxLength: 100),
                        violation_place_road = c.String(maxLength: 100),
                        itemno = c.String(maxLength: 50),
                        IsUnitAssign = c.Int(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.CaseTrafficPoprocs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        CaseTime = c.DateTime(nullable: false),
                        UnitId = c.Int(),
                        MemberId = c.Int(),
                        Status = c.Int(nullable: false),
                        AssignDateTime = c.DateTime(),
                        AssignMemo = c.String(),
                        EndDateTime = c.DateTime(),
                        DelyMemo = c.String(),
                        delyflag = c.Int(),
                        DelyDateTime = c.DateTime(),
                        Upfile1 = c.String(maxLength: 200),
                        Type1 = c.String(maxLength: 200),
                        type2 = c.String(maxLength: 200),
                        noplyreason = c.String(),
                        process = c.String(maxLength: 10),
                        PoprocsType = c.Int(),
                        TrafficViolationsClassId = c.Int(),
                        ViolationsRejectclassId = c.Int(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CaseTraffics", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .ForeignKey("dbo.TrafficViolationsClasses", t => t.TrafficViolationsClassId)
                .ForeignKey("dbo.TrafficViolationsRejectclasses", t => t.ViolationsRejectclassId)
                .Index(t => t.CaseId)
                .Index(t => t.UnitId)
                .Index(t => t.MemberId)
                .Index(t => t.TrafficViolationsClassId)
                .Index(t => t.ViolationsRejectclassId);
            
            CreateTable(
                "dbo.TrafficViolationsClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrafficViolationsRejectclasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CaseTrafficPoprocLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(),
                        UnitId = c.Int(),
                        MemberId = c.Int(),
                        Action = c.String(maxLength: 200),
                        ActionMemo = c.String(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CaseTraffics", t => t.CaseId)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.CaseId)
                .Index(t => t.UnitId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.CaseTrafficqws",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(),
                        Q1 = c.Int(),
                        Q2 = c.Int(),
                        Q3 = c.Int(),
                        IP = c.String(maxLength: 200),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CaseTraffics", t => t.CaseId)
                .Index(t => t.CaseId);
            
            CreateTable(
                "dbo.TrafficMailChecks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseGuid = c.String(maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 200),
                        InitDate = c.DateTime(nullable: false),
                        IsConfirm = c.Int(nullable: false),
                        ConfirmDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CaseMailChecks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseGuid = c.String(maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 200),
                        InitDate = c.DateTime(nullable: false),
                        IsConfirm = c.Int(nullable: false),
                        ConfirmDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrafficDepartmentdetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Trafficdisdatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(),
                        isAutoAssign = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.TrafficRegions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        UnitId = c.Int(),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.TrafficRoaddatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegionId = c.Int(),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrafficRegions", t => t.RegionId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Holidays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsHoliday = c.Boolean(nullable: false),
                        HolidayCategory = c.String(),
                        Description = c.String(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WayPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(maxLength: 200),
                        RegionName = c.String(maxLength: 200),
                        Address = c.String(maxLength: 200),
                        DeptNm = c.String(maxLength: 200),
                        BranchNm = c.String(maxLength: 200),
                        Longitude = c.String(maxLength: 200),
                        Latitude = c.String(maxLength: 200),
                        direct = c.String(maxLength: 200),
                        limit = c.String(maxLength: 200),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrafficSMS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Mobile = c.String(nullable: false, maxLength: 10),
                        CheckCode = c.String(maxLength: 10),
                        IsAction = c.Int(nullable: false),
                        CaseGuid = c.String(maxLength: 200),
                        Times = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrafficSMSCarInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarNO = c.String(nullable: false, maxLength: 10),
                        CarType = c.Int(nullable: false),
                        CarOwner = c.String(nullable: false, maxLength: 10),
                        Pid = c.String(nullable: false, maxLength: 10),
                        CarAllow = c.Int(nullable: false),
                        checkStatus = c.Int(nullable: false),
                        TrafficSMSId = c.Int(),
                        TrafficSMSCarInfoRejectId = c.Int(),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrafficSMS", t => t.TrafficSMSId)
                .ForeignKey("dbo.TrafficSMSCarInfoRejects", t => t.TrafficSMSCarInfoRejectId)
                .Index(t => t.TrafficSMSId)
                .Index(t => t.TrafficSMSCarInfoRejectId);
            
            CreateTable(
                "dbo.TrafficSMSCarInfoRejects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        ListNum = c.Int(nullable: false),
                        Poster = c.String(maxLength: 20),
                        initOrg = c.String(maxLength: 20),
                        InitDate = c.DateTime(),
                        Updater = c.String(maxLength: 20),
                        UpdateOrg = c.String(maxLength: 20),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.RoleMembers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Member_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Member_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Member_Id);
            
            CreateTable(
                "dbo.NewsCatalogNews",
                c => new
                    {
                        NewsCatalog_Id = c.Int(nullable: false),
                        News_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NewsCatalog_Id, t.News_Id })
                .ForeignKey("dbo.NewsCatalogs", t => t.NewsCatalog_Id, cascadeDelete: true)
                .ForeignKey("dbo.News", t => t.News_Id, cascadeDelete: true)
                .Index(t => t.NewsCatalog_Id)
                .Index(t => t.News_Id);
            
            CreateTable(
                "dbo.EseCatalogEses",
                c => new
                    {
                        EseCatalog_Id = c.Int(nullable: false),
                        Ese_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EseCatalog_Id, t.Ese_Id })
                .ForeignKey("dbo.EseCatalogs", t => t.EseCatalog_Id, cascadeDelete: true)
                .ForeignKey("dbo.Eses", t => t.Ese_Id, cascadeDelete: true)
                .Index(t => t.EseCatalog_Id)
                .Index(t => t.Ese_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.EseCatalogEses", new[] { "Ese_Id" });
            DropIndex("dbo.EseCatalogEses", new[] { "EseCatalog_Id" });
            DropIndex("dbo.NewsCatalogNews", new[] { "News_Id" });
            DropIndex("dbo.NewsCatalogNews", new[] { "NewsCatalog_Id" });
            DropIndex("dbo.RoleMembers", new[] { "Member_Id" });
            DropIndex("dbo.RoleMembers", new[] { "Role_Id" });
            DropIndex("dbo.TrafficSMSCarInfoes", new[] { "TrafficSMSCarInfoRejectId" });
            DropIndex("dbo.TrafficSMSCarInfoes", new[] { "TrafficSMSId" });
            DropIndex("dbo.TrafficRoaddatas", new[] { "RegionId" });
            DropIndex("dbo.TrafficRegions", new[] { "UnitId" });
            DropIndex("dbo.Trafficdisdatas", new[] { "MemberId" });
            DropIndex("dbo.TrafficDepartmentdetails", new[] { "MemberId" });
            DropIndex("dbo.CaseTrafficqws", new[] { "CaseId" });
            DropIndex("dbo.CaseTrafficPoprocLogs", new[] { "MemberId" });
            DropIndex("dbo.CaseTrafficPoprocLogs", new[] { "UnitId" });
            DropIndex("dbo.CaseTrafficPoprocLogs", new[] { "CaseId" });
            DropIndex("dbo.CaseTrafficPoprocs", new[] { "ViolationsRejectclassId" });
            DropIndex("dbo.CaseTrafficPoprocs", new[] { "TrafficViolationsClassId" });
            DropIndex("dbo.CaseTrafficPoprocs", new[] { "MemberId" });
            DropIndex("dbo.CaseTrafficPoprocs", new[] { "UnitId" });
            DropIndex("dbo.CaseTrafficPoprocs", new[] { "CaseId" });
            DropIndex("dbo.CaseTraffics", new[] { "UnitId" });
            DropIndex("dbo.CaseFilters", new[] { "TypeId" });
            DropIndex("dbo.Casewqs", new[] { "CaseId" });
            DropIndex("dbo.CasePoprocLogs", new[] { "MemberId" });
            DropIndex("dbo.CasePoprocLogs", new[] { "UnitId" });
            DropIndex("dbo.CasePoprocLogs", new[] { "CaseId" });
            DropIndex("dbo.CasePoprocs", new[] { "MemberId" });
            DropIndex("dbo.CasePoprocs", new[] { "UnitId" });
            DropIndex("dbo.CasePoprocs", new[] { "CaseId" });
            DropIndex("dbo.Cases", new[] { "ParentId" });
            DropIndex("dbo.Cases", new[] { "WebSiteId" });
            DropIndex("dbo.Wandas", new[] { "StationInfoId" });
            DropIndex("dbo.BigBanners", new[] { "WebSiteNameId" });
            DropIndex("dbo.EseCatalogs", new[] { "WebSiteId" });
            DropIndex("dbo.EseCatalogs", new[] { "MetaId" });
            DropIndex("dbo.EseCatalogs", new[] { "WebCategoryId" });
            DropIndex("dbo.EseFiles", new[] { "EseId" });
            DropIndex("dbo.StationInfoFIles", new[] { "StationInfoId" });
            DropIndex("dbo.StationInfoes", new[] { "TownId" });
            DropIndex("dbo.StationInfoes", new[] { "ParentId" });
            DropIndex("dbo.AboutLinks", new[] { "CategoryId" });
            DropIndex("dbo.AboutLinkCatalogs", new[] { "WebSiteId" });
            DropIndex("dbo.Villages", new[] { "TownId" });
            DropIndex("dbo.HomeThemes", new[] { "WebSiteNameId" });
            DropIndex("dbo.HomeAds", new[] { "WebSiteNameId" });
            DropIndex("dbo.HomeLinks", new[] { "WebSiteNameId" });
            DropIndex("dbo.WebArticles", new[] { "MetaId" });
            DropIndex("dbo.WebSiteNames", new[] { "UnitId" });
            DropIndex("dbo.NewsCatalogs", new[] { "WebSiteId" });
            DropIndex("dbo.NewsCatalogs", new[] { "MetaId" });
            DropIndex("dbo.NewsCatalogs", new[] { "WebCategoryId" });
            DropIndex("dbo.NewsImages", new[] { "NewId" });
            DropIndex("dbo.NewsFiles", new[] { "NewId" });
            DropIndex("dbo.NewsLinks", new[] { "NewId" });
            DropIndex("dbo.News", new[] { "MemberId" });
            DropIndex("dbo.Members", new[] { "UnitId" });
            DropIndex("dbo.Units", new[] { "ParentId" });
            DropForeignKey("dbo.EseCatalogEses", "Ese_Id", "dbo.Eses");
            DropForeignKey("dbo.EseCatalogEses", "EseCatalog_Id", "dbo.EseCatalogs");
            DropForeignKey("dbo.NewsCatalogNews", "News_Id", "dbo.News");
            DropForeignKey("dbo.NewsCatalogNews", "NewsCatalog_Id", "dbo.NewsCatalogs");
            DropForeignKey("dbo.RoleMembers", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.RoleMembers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.TrafficSMSCarInfoes", "TrafficSMSCarInfoRejectId", "dbo.TrafficSMSCarInfoRejects");
            DropForeignKey("dbo.TrafficSMSCarInfoes", "TrafficSMSId", "dbo.TrafficSMS");
            DropForeignKey("dbo.TrafficRoaddatas", "RegionId", "dbo.TrafficRegions");
            DropForeignKey("dbo.TrafficRegions", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Trafficdisdatas", "MemberId", "dbo.Members");
            DropForeignKey("dbo.TrafficDepartmentdetails", "MemberId", "dbo.Members");
            DropForeignKey("dbo.CaseTrafficqws", "CaseId", "dbo.CaseTraffics");
            DropForeignKey("dbo.CaseTrafficPoprocLogs", "MemberId", "dbo.Members");
            DropForeignKey("dbo.CaseTrafficPoprocLogs", "UnitId", "dbo.Units");
            DropForeignKey("dbo.CaseTrafficPoprocLogs", "CaseId", "dbo.CaseTraffics");
            DropForeignKey("dbo.CaseTrafficPoprocs", "ViolationsRejectclassId", "dbo.TrafficViolationsRejectclasses");
            DropForeignKey("dbo.CaseTrafficPoprocs", "TrafficViolationsClassId", "dbo.TrafficViolationsClasses");
            DropForeignKey("dbo.CaseTrafficPoprocs", "MemberId", "dbo.Members");
            DropForeignKey("dbo.CaseTrafficPoprocs", "UnitId", "dbo.Units");
            DropForeignKey("dbo.CaseTrafficPoprocs", "CaseId", "dbo.CaseTraffics");
            DropForeignKey("dbo.CaseTraffics", "UnitId", "dbo.Units");
            DropForeignKey("dbo.CaseFilters", "TypeId", "dbo.PoprocsSubTypes");
            DropForeignKey("dbo.Casewqs", "CaseId", "dbo.Cases");
            DropForeignKey("dbo.CasePoprocLogs", "MemberId", "dbo.Members");
            DropForeignKey("dbo.CasePoprocLogs", "UnitId", "dbo.Units");
            DropForeignKey("dbo.CasePoprocLogs", "CaseId", "dbo.Cases");
            DropForeignKey("dbo.CasePoprocs", "MemberId", "dbo.Members");
            DropForeignKey("dbo.CasePoprocs", "UnitId", "dbo.Units");
            DropForeignKey("dbo.CasePoprocs", "CaseId", "dbo.Cases");
            DropForeignKey("dbo.Cases", "ParentId", "dbo.Cases");
            DropForeignKey("dbo.Cases", "WebSiteId", "dbo.WebSiteNames");
            DropForeignKey("dbo.Wandas", "StationInfoId", "dbo.StationInfoes");
            DropForeignKey("dbo.BigBanners", "WebSiteNameId", "dbo.WebSiteNames");
            DropForeignKey("dbo.EseCatalogs", "WebSiteId", "dbo.WebSiteNames");
            DropForeignKey("dbo.EseCatalogs", "MetaId", "dbo.MetaIndexes");
            DropForeignKey("dbo.EseCatalogs", "WebCategoryId", "dbo.WebNewsCatalogs");
            DropForeignKey("dbo.EseFiles", "EseId", "dbo.Eses");
            DropForeignKey("dbo.StationInfoFIles", "StationInfoId", "dbo.StationInfoes");
            DropForeignKey("dbo.StationInfoes", "TownId", "dbo.Towns");
            DropForeignKey("dbo.StationInfoes", "ParentId", "dbo.StationInfoes");
            DropForeignKey("dbo.AboutLinks", "CategoryId", "dbo.AboutLinkCatalogs");
            DropForeignKey("dbo.AboutLinkCatalogs", "WebSiteId", "dbo.WebSiteNames");
            DropForeignKey("dbo.Villages", "TownId", "dbo.Towns");
            DropForeignKey("dbo.HomeThemes", "WebSiteNameId", "dbo.WebSiteNames");
            DropForeignKey("dbo.HomeAds", "WebSiteNameId", "dbo.WebSiteNames");
            DropForeignKey("dbo.HomeLinks", "WebSiteNameId", "dbo.WebSiteNames");
            DropForeignKey("dbo.WebArticles", "MetaId", "dbo.MetaIndexes");
            DropForeignKey("dbo.WebSiteNames", "UnitId", "dbo.Units");
            DropForeignKey("dbo.NewsCatalogs", "WebSiteId", "dbo.WebSiteNames");
            DropForeignKey("dbo.NewsCatalogs", "MetaId", "dbo.MetaIndexes");
            DropForeignKey("dbo.NewsCatalogs", "WebCategoryId", "dbo.WebNewsCatalogs");
            DropForeignKey("dbo.NewsImages", "NewId", "dbo.News");
            DropForeignKey("dbo.NewsFiles", "NewId", "dbo.News");
            DropForeignKey("dbo.NewsLinks", "NewId", "dbo.News");
            DropForeignKey("dbo.News", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Members", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Units", "ParentId", "dbo.Units");
            DropTable("dbo.EseCatalogEses");
            DropTable("dbo.NewsCatalogNews");
            DropTable("dbo.RoleMembers");
            DropTable("dbo.RefugeStations");
            DropTable("dbo.TrafficSMSCarInfoRejects");
            DropTable("dbo.TrafficSMSCarInfoes");
            DropTable("dbo.TrafficSMS");
            DropTable("dbo.WayPoints");
            DropTable("dbo.Holidays");
            DropTable("dbo.TrafficRoaddatas");
            DropTable("dbo.TrafficRegions");
            DropTable("dbo.Trafficdisdatas");
            DropTable("dbo.TrafficDepartmentdetails");
            DropTable("dbo.CaseMailChecks");
            DropTable("dbo.TrafficMailChecks");
            DropTable("dbo.CaseTrafficqws");
            DropTable("dbo.CaseTrafficPoprocLogs");
            DropTable("dbo.TrafficViolationsRejectclasses");
            DropTable("dbo.TrafficViolationsClasses");
            DropTable("dbo.CaseTrafficPoprocs");
            DropTable("dbo.CaseTraffics");
            DropTable("dbo.PoprocsSubTypes");
            DropTable("dbo.CaseFilters");
            DropTable("dbo.Casewqs");
            DropTable("dbo.CasePoprocLogs");
            DropTable("dbo.CasePoprocs");
            DropTable("dbo.Cases");
            DropTable("dbo.Wandas");
            DropTable("dbo.BigBanners");
            DropTable("dbo.EseCatalogs");
            DropTable("dbo.EseFiles");
            DropTable("dbo.Eses");
            DropTable("dbo.StationInfoFIles");
            DropTable("dbo.StationInfoes");
            DropTable("dbo.FontSizes");
            DropTable("dbo.WebSiteColors");
            DropTable("dbo.AboutLinks");
            DropTable("dbo.AboutLinkCatalogs");
            DropTable("dbo.Villages");
            DropTable("dbo.Towns");
            DropTable("dbo.HomeThemes");
            DropTable("dbo.HomeAds");
            DropTable("dbo.HomeLinks");
            DropTable("dbo.Templates");
            DropTable("dbo.WebArticles");
            DropTable("dbo.SysMenus");
            DropTable("dbo.WebSiteNames");
            DropTable("dbo.WebNewsCatalogs");
            DropTable("dbo.NewsCatalogs");
            DropTable("dbo.NewsImages");
            DropTable("dbo.NewsFiles");
            DropTable("dbo.NewsLinks");
            DropTable("dbo.News");
            DropTable("dbo.MetaIndexes");
            DropTable("dbo.SystemLogs");
            DropTable("dbo.Roles");
            DropTable("dbo.Members");
            DropTable("dbo.Units");
        }
    }
}
