CREATE TABLE [dbo].[Units] (
    [Id] [int] NOT NULL IDENTITY,
    [ParentId] [int],
    [Subject] [nvarchar](100) NOT NULL,
    [Alias] [nvarchar](50),
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Units] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ParentId] ON [dbo].[Units]([ParentId])
CREATE TABLE [dbo].[Members] (
    [Id] [int] NOT NULL IDENTITY,
    [Account] [nvarchar](50) NOT NULL,
    [Password] [nvarchar](max),
    [PasswordSalt] [nvarchar](100),
    [Name] [nvarchar](50) NOT NULL,
    [CName] [nvarchar](50),
    [TEL] [nvarchar](50),
    [Mobile] [nvarchar](50),
    [URL] [nvarchar](500),
    [Memo] [nvarchar](max),
    [Gender] [int] NOT NULL,
    [Email] [nvarchar](200) NOT NULL,
    [MyPic] [nvarchar](50),
    [JobTitle] [nvarchar](50),
    [UnitId] [int] NOT NULL,
    [Permission] [nvarchar](500),
    [ADMessage] [nvarchar](max),
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Members] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UnitId] ON [dbo].[Members]([UnitId])
CREATE TABLE [dbo].[Roles] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](100) NOT NULL,
    [Permission] [nvarchar](max),
    [Alias] [nvarchar](50),
    [UnitId] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Roles] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[SystemLogs] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](500) NOT NULL,
    [Poster] [nvarchar](50),
    [InitDate] [datetime],
    CONSTRAINT [PK_dbo.SystemLogs] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[MetaIndexes] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](100),
    [SiteCode] [nvarchar](100),
    [Theme] [nvarchar](max),
    [ThemeName] [nvarchar](max),
    [Cake] [nvarchar](max),
    [CakeName] [nvarchar](max),
    [Service] [nvarchar](max),
    [ServiceName] [nvarchar](max),
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.MetaIndexes] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[News] (
    [Id] [int] NOT NULL IDENTITY,
    [Serno] [nvarchar](100),
    [Subject] [nvarchar](200) NOT NULL,
    [Article] [nvarchar](max),
    [Views] [int] NOT NULL,
    [StartDate] [datetime],
    [EndDate] [datetime],
    [AssignDateTime] [datetime],
    [MemberId] [int],
    [IsConfirm] [int] NOT NULL,
    [WebCategoryId] [int] NOT NULL,
    [OwnWebSiteId] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.News] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_MemberId] ON [dbo].[News]([MemberId])
CREATE TABLE [dbo].[NewsLinks] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200),
    [NewId] [int] NOT NULL,
    [LinkUrl] [nvarchar](500),
    [Target] [int] NOT NULL,
    [ListNum] [int] NOT NULL,
    [WebCategoryId] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.NewsLinks] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_NewId] ON [dbo].[NewsLinks]([NewId])
CREATE TABLE [dbo].[NewsFiles] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [NewId] [int],
    [UpFile] [nvarchar](500),
    [ListNum] [int] NOT NULL,
    [WebCategoryId] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.NewsFiles] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_NewId] ON [dbo].[NewsFiles]([NewId])
CREATE TABLE [dbo].[NewsImages] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [NewId] [int],
    [UpFile] [nvarchar](500),
    [ListNum] [int] NOT NULL,
    [WebCategoryId] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.NewsImages] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_NewId] ON [dbo].[NewsImages]([NewId])
CREATE TABLE [dbo].[NewsCatalogs] (
    [Id] [int] NOT NULL IDENTITY,
    [Serno] [nvarchar](100),
    [WebCategoryId] [int],
    [MetaId] [int] NOT NULL,
    [WebSiteId] [int],
    [Subject] [nvarchar](200) NOT NULL,
    [ListNum] [int] NOT NULL,
    [Confirm] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.NewsCatalogs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_WebCategoryId] ON [dbo].[NewsCatalogs]([WebCategoryId])
CREATE INDEX [IX_MetaId] ON [dbo].[NewsCatalogs]([MetaId])
CREATE INDEX [IX_WebSiteId] ON [dbo].[NewsCatalogs]([WebSiteId])
CREATE TABLE [dbo].[WebNewsCatalogs] (
    [Id] [int] NOT NULL IDENTITY,
    [Language] [int] NOT NULL,
    [Subject] [nvarchar](200) NOT NULL,
    [ListNum] [int] NOT NULL,
    [Confirm] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.WebNewsCatalogs] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[WebSiteNames] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](100),
    [SiteCode] [nvarchar](100),
    [OrgNumber] [nvarchar](100),
    [XmlDoc] [nvarchar](max),
    [ColorName] [nvarchar](100),
    [UpImageUrl] [nvarchar](200),
    [Enable] [int] NOT NULL,
    [ListNum] [int] NOT NULL,
    [SubjectEn] [nvarchar](100),
    [Path] [nvarchar](100),
    [Language] [int] NOT NULL,
    [UnitId] [int] NOT NULL,
    [PubUnitDN] [nvarchar](100),
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.WebSiteNames] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UnitId] ON [dbo].[WebSiteNames]([UnitId])
CREATE TABLE [dbo].[SysMenus] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](100),
    [Url] [nvarchar](40),
    [Value] [nvarchar](40),
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.SysMenus] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[WebArticles] (
    [Id] [int] NOT NULL IDENTITY,
    [MetaId] [int] NOT NULL,
    [UnId] [nvarchar](100),
    [Subject] [nvarchar](100),
    [MemberPath] [nvarchar](100),
    [Article] [nvarchar](max),
    [Views] [int] NOT NULL,
    [Enable] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.WebArticles] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_MetaId] ON [dbo].[WebArticles]([MetaId])
CREATE TABLE [dbo].[Templates] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [UpImageUrl] [nvarchar](200),
    [BeSelect] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Templates] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[HomeLinks] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [DataType] [int] NOT NULL,
    [StartDate] [datetime],
    [EndDate] [datetime],
    [UpImage] [nvarchar](200),
    [Url] [nvarchar](200),
    [ListNum] [int] NOT NULL,
    [Enable] [int] NOT NULL,
    [WebSiteNameId] [int],
    [OwnWebSiteId] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.HomeLinks] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_WebSiteNameId] ON [dbo].[HomeLinks]([WebSiteNameId])
CREATE TABLE [dbo].[HomeAds] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [UpImage] [nvarchar](200),
    [Url] [nvarchar](200),
    [ListNum] [int] NOT NULL,
    [Enable] [int] NOT NULL,
    [WebSiteNameId] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.HomeAds] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_WebSiteNameId] ON [dbo].[HomeAds]([WebSiteNameId])
CREATE TABLE [dbo].[HomeThemes] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [UpImage] [nvarchar](200),
    [Url] [nvarchar](200),
    [ListNum] [int] NOT NULL,
    [Enable] [int] NOT NULL,
    [WebSiteNameId] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.HomeThemes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_WebSiteNameId] ON [dbo].[HomeThemes]([WebSiteNameId])
CREATE TABLE [dbo].[Towns] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](100) NOT NULL,
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Towns] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Villages] (
    [Id] [int] NOT NULL IDENTITY,
    [TownId] [int] NOT NULL,
    [Subject] [nvarchar](100) NOT NULL,
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Villages] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_TownId] ON [dbo].[Villages]([TownId])
CREATE TABLE [dbo].[AboutLinkCatalogs] (
    [Id] [int] NOT NULL IDENTITY,
    [Serno] [nvarchar](100),
    [WebSiteId] [int],
    [Subject] [nvarchar](200) NOT NULL,
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.AboutLinkCatalogs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_WebSiteId] ON [dbo].[AboutLinkCatalogs]([WebSiteId])
CREATE TABLE [dbo].[AboutLinks] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [CategoryId] [int],
    [UpImageUrl] [nvarchar](200),
    [URL] [nvarchar](max),
    [Status] [int] NOT NULL,
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.AboutLinks] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CategoryId] ON [dbo].[AboutLinks]([CategoryId])
CREATE TABLE [dbo].[WebSiteColors] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](100),
    [SiteCode] [nvarchar](100),
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.WebSiteColors] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[FontSizes] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](100) NOT NULL,
    [BSize] [int] NOT NULL,
    [MSize] [int] NOT NULL,
    [SSize] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.FontSizes] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[StationInfoes] (
    [Id] [int] NOT NULL IDENTITY,
    [ParentId] [int],
    [TownId] [int],
    [Subject] [nvarchar](100) NOT NULL,
    [Tel] [nvarchar](500),
    [PostalCode] [nvarchar](100),
    [Address] [nvarchar](500),
    [Twd97X] [nvarchar](20),
    [Twd97Y] [nvarchar](20),
    [Article] [nvarchar](max),
    [ListNum] [int] NOT NULL,
    [Serno] [nvarchar](100),
    [OwnWebSiteId] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.StationInfoes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ParentId] ON [dbo].[StationInfoes]([ParentId])
CREATE INDEX [IX_TownId] ON [dbo].[StationInfoes]([TownId])
CREATE TABLE [dbo].[StationInfoFIles] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [StationInfoId] [int],
    [UpFile] [nvarchar](500),
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.StationInfoFIles] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_StationInfoId] ON [dbo].[StationInfoFIles]([StationInfoId])
CREATE TABLE [dbo].[Eses] (
    [Id] [int] NOT NULL IDENTITY,
    [Serno] [nvarchar](100),
    [Subject] [nvarchar](200) NOT NULL,
    [Undertaking_Unit] [nvarchar](200),
    [Apply_method] [nvarchar](max),
    [NeedDoc] [nvarchar](max),
    [Process] [nvarchar](200),
    [Pdate] [nvarchar](200),
    [Tel] [nvarchar](200),
    [Fax] [nvarchar](200),
    [Mail] [nvarchar](200),
    [Remark] [nvarchar](max),
    [Online] [nvarchar](max),
    [Status] [int] NOT NULL,
    [Memo] [nvarchar](max),
    [Views] [int] NOT NULL,
    [OwnWebSiteId] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Eses] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[EseFiles] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [EseId] [int],
    [UpFile] [nvarchar](500),
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.EseFiles] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EseId] ON [dbo].[EseFiles]([EseId])
CREATE TABLE [dbo].[EseCatalogs] (
    [Id] [int] NOT NULL IDENTITY,
    [Serno] [nvarchar](100),
    [WebCategoryId] [int],
    [MetaId] [int] NOT NULL,
    [WebSiteId] [int],
    [Subject] [nvarchar](200) NOT NULL,
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.EseCatalogs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_WebCategoryId] ON [dbo].[EseCatalogs]([WebCategoryId])
CREATE INDEX [IX_MetaId] ON [dbo].[EseCatalogs]([MetaId])
CREATE INDEX [IX_WebSiteId] ON [dbo].[EseCatalogs]([WebSiteId])
CREATE TABLE [dbo].[BigBanners] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [UpImage] [nvarchar](200),
    [ListNum] [int] NOT NULL,
    [Enable] [int] NOT NULL,
    [URL] [nvarchar](max),
    [WebSiteNameId] [int],
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.BigBanners] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_WebSiteNameId] ON [dbo].[BigBanners]([WebSiteNameId])
CREATE TABLE [dbo].[Wandas] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [StationInfoId] [int],
    [beginning] [nvarchar](200) NOT NULL,
    [destination] [nvarchar](200) NOT NULL,
    [Article] [nvarchar](max),
    [Status] [int] NOT NULL,
    [OwnWebSiteId] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Wandas] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_StationInfoId] ON [dbo].[Wandas]([StationInfoId])
CREATE TABLE [dbo].[Cases] (
    [Id] [int] NOT NULL IDENTITY,
    [CaseType] [int] NOT NULL,
    [CaseID] [nvarchar](200) NOT NULL,
    [CaseGuid] [nvarchar](200),
    [Subject] [nvarchar](200) NOT NULL,
    [Content] [nvarchar](max),
    [Name] [nvarchar](200) NOT NULL,
    [Gender] [int] NOT NULL,
    [Email] [nvarchar](200) NOT NULL,
    [TEL] [nvarchar](200),
    [Job] [nvarchar](200),
    [IP] [nvarchar](200),
    [WebSiteId] [int],
    [Predate] [datetime] NOT NULL,
    [Unit] [nvarchar](200),
    [OrgName] [nvarchar](200),
    [pcnt] [nvarchar](4),
    [ODate] [datetime],
    [STime] [nvarchar](200),
    [ETime] [nvarchar](200),
    [Pid] [nvarchar](10),
    [PostalCode] [nvarchar](200),
    [Address] [nvarchar](200),
    [PPostalCode] [nvarchar](200),
    [PAddress] [nvarchar](200),
    [HomeTEL] [nvarchar](200),
    [OfficeTEL] [nvarchar](200),
    [CaseReportType] [int] NOT NULL,
    [Oplace] [nvarchar](200),
    [IsUnitAssign] [int],
    [OArea] [nvarchar](10),
    [ParentId] [int],
    [ChiefMailboxUnitId] [int],
    [IsAutoClose] [int],
    [FilterString] [nvarchar](500),
    [FilterType] [nvarchar](100),
    [Upfile1] [nvarchar](200),
    [Upfile2] [nvarchar](200),
    [Upfile3] [nvarchar](200),
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Cases] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_WebSiteId] ON [dbo].[Cases]([WebSiteId])
CREATE INDEX [IX_ParentId] ON [dbo].[Cases]([ParentId])
CREATE TABLE [dbo].[CasePoprocs] (
    [Id] [int] NOT NULL IDENTITY,
    [CaseId] [int] NOT NULL,
    [CaseType] [int] NOT NULL,
    [CaseTime] [datetime] NOT NULL,
    [UnitId] [int],
    [MemberId] [int],
    [Status] [int] NOT NULL,
    [AssignDateTime] [datetime],
    [AssignMemo] [nvarchar](max),
    [EndDateTime] [datetime],
    [DelyMemo] [nvarchar](max),
    [delyflag] [int],
    [DelyDateTime] [datetime],
    [Upfile1] [nvarchar](200),
    [Type1] [nvarchar](200),
    [type2] [nvarchar](200),
    [noplyreason] [nvarchar](max),
    [process] [nvarchar](10),
    [PoprocsType] [int],
    [PoprocsSubType] [int],
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.CasePoprocs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CaseId] ON [dbo].[CasePoprocs]([CaseId])
CREATE INDEX [IX_UnitId] ON [dbo].[CasePoprocs]([UnitId])
CREATE INDEX [IX_MemberId] ON [dbo].[CasePoprocs]([MemberId])
CREATE TABLE [dbo].[CasePoprocLogs] (
    [Id] [int] NOT NULL IDENTITY,
    [CaseId] [int],
    [UnitId] [int],
    [MemberId] [int],
    [Action] [nvarchar](200),
    [ActionMemo] [nvarchar](max),
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.CasePoprocLogs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CaseId] ON [dbo].[CasePoprocLogs]([CaseId])
CREATE INDEX [IX_UnitId] ON [dbo].[CasePoprocLogs]([UnitId])
CREATE INDEX [IX_MemberId] ON [dbo].[CasePoprocLogs]([MemberId])
CREATE TABLE [dbo].[Casewqs] (
    [Id] [int] NOT NULL IDENTITY,
    [CaseId] [int],
    [Q1] [int],
    [Q2] [int],
    [Q3] [int],
    [IP] [nvarchar](200),
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Casewqs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CaseId] ON [dbo].[Casewqs]([CaseId])
CREATE TABLE [dbo].[CaseFilters] (
    [Id] [int] NOT NULL IDENTITY,
    [FilterType] [int] NOT NULL,
    [Subject] [nvarchar](max) NOT NULL,
    [TypeId] [int],
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.CaseFilters] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_TypeId] ON [dbo].[CaseFilters]([TypeId])
CREATE TABLE [dbo].[PoprocsSubTypes] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](max) NOT NULL,
    [Article] [nvarchar](max),
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.PoprocsSubTypes] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[CaseTraffics] (
    [Id] [int] NOT NULL IDENTITY,
    [CaseType] [int] NOT NULL,
    [CaseID] [nvarchar](200) NOT NULL,
    [CaseGuid] [nvarchar](200),
    [Subject] [nvarchar](200) NOT NULL,
    [Content] [nvarchar](max),
    [Name] [nvarchar](200) NOT NULL,
    [Pid] [nvarchar](10) NOT NULL,
    [Gender] [int] NOT NULL,
    [Email] [nvarchar](200) NOT NULL,
    [TEL] [nvarchar](200),
    [Job] [nvarchar](200),
    [Address] [nvarchar](200),
    [IP] [nvarchar](200),
    [UnitId] [int],
    [Upfile1] [nvarchar](200),
    [Upfile2] [nvarchar](200),
    [Upfile3] [nvarchar](200),
    [Upfile4] [nvarchar](200),
    [Upfile5] [nvarchar](200),
    [Upfile6] [nvarchar](200),
    [violation_date] [datetime] NOT NULL,
    [violation_time] [nvarchar](10),
    [Predate] [datetime] NOT NULL,
    [violation_place] [nvarchar](500),
    [violation_carno] [nvarchar](10),
    [violation_place_area] [nvarchar](100),
    [violation_place_road] [nvarchar](100),
    [itemno] [nvarchar](50),
    [IsUnitAssign] [int],
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.CaseTraffics] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UnitId] ON [dbo].[CaseTraffics]([UnitId])
CREATE TABLE [dbo].[CaseTrafficPoprocs] (
    [Id] [int] NOT NULL IDENTITY,
    [CaseId] [int] NOT NULL,
    [CaseTime] [datetime] NOT NULL,
    [UnitId] [int],
    [MemberId] [int],
    [Status] [int] NOT NULL,
    [AssignDateTime] [datetime],
    [AssignMemo] [nvarchar](max),
    [EndDateTime] [datetime],
    [DelyMemo] [nvarchar](max),
    [delyflag] [int],
    [DelyDateTime] [datetime],
    [Upfile1] [nvarchar](200),
    [Type1] [nvarchar](200),
    [type2] [nvarchar](200),
    [noplyreason] [nvarchar](max),
    [process] [nvarchar](10),
    [PoprocsType] [int],
    [TrafficViolationsClassId] [int],
    [ViolationsRejectclassId] [int],
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.CaseTrafficPoprocs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CaseId] ON [dbo].[CaseTrafficPoprocs]([CaseId])
CREATE INDEX [IX_UnitId] ON [dbo].[CaseTrafficPoprocs]([UnitId])
CREATE INDEX [IX_MemberId] ON [dbo].[CaseTrafficPoprocs]([MemberId])
CREATE INDEX [IX_TrafficViolationsClassId] ON [dbo].[CaseTrafficPoprocs]([TrafficViolationsClassId])
CREATE INDEX [IX_ViolationsRejectclassId] ON [dbo].[CaseTrafficPoprocs]([ViolationsRejectclassId])
CREATE TABLE [dbo].[TrafficViolationsClasses] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.TrafficViolationsClasses] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[TrafficViolationsRejectclasses] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.TrafficViolationsRejectclasses] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[CaseTrafficPoprocLogs] (
    [Id] [int] NOT NULL IDENTITY,
    [CaseId] [int],
    [UnitId] [int],
    [MemberId] [int],
    [Action] [nvarchar](200),
    [ActionMemo] [nvarchar](max),
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.CaseTrafficPoprocLogs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CaseId] ON [dbo].[CaseTrafficPoprocLogs]([CaseId])
CREATE INDEX [IX_UnitId] ON [dbo].[CaseTrafficPoprocLogs]([UnitId])
CREATE INDEX [IX_MemberId] ON [dbo].[CaseTrafficPoprocLogs]([MemberId])
CREATE TABLE [dbo].[CaseTrafficqws] (
    [Id] [int] NOT NULL IDENTITY,
    [CaseId] [int],
    [Q1] [int],
    [Q2] [int],
    [Q3] [int],
    [IP] [nvarchar](200),
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.CaseTrafficqws] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CaseId] ON [dbo].[CaseTrafficqws]([CaseId])
CREATE TABLE [dbo].[TrafficMailChecks] (
    [Id] [int] NOT NULL IDENTITY,
    [CaseGuid] [nvarchar](200),
    [Email] [nvarchar](200) NOT NULL,
    [InitDate] [datetime] NOT NULL,
    [IsConfirm] [int] NOT NULL,
    [ConfirmDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.TrafficMailChecks] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[CaseMailChecks] (
    [Id] [int] NOT NULL IDENTITY,
    [CaseGuid] [nvarchar](200),
    [Email] [nvarchar](200) NOT NULL,
    [InitDate] [datetime] NOT NULL,
    [IsConfirm] [int] NOT NULL,
    [ConfirmDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.CaseMailChecks] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[TrafficDepartmentdetails] (
    [Id] [int] NOT NULL IDENTITY,
    [MemberId] [int],
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.TrafficDepartmentdetails] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_MemberId] ON [dbo].[TrafficDepartmentdetails]([MemberId])
CREATE TABLE [dbo].[Trafficdisdatas] (
    [Id] [int] NOT NULL IDENTITY,
    [MemberId] [int],
    [isAutoAssign] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Trafficdisdatas] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_MemberId] ON [dbo].[Trafficdisdatas]([MemberId])
CREATE TABLE [dbo].[TrafficRegions] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [UnitId] [int],
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.TrafficRegions] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UnitId] ON [dbo].[TrafficRegions]([UnitId])
CREATE TABLE [dbo].[TrafficRoaddatas] (
    [Id] [int] NOT NULL IDENTITY,
    [RegionId] [int],
    [Subject] [nvarchar](200) NOT NULL,
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.TrafficRoaddatas] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_RegionId] ON [dbo].[TrafficRoaddatas]([RegionId])
CREATE TABLE [dbo].[Holidays] (
    [Id] [int] NOT NULL IDENTITY,
    [IsHoliday] [bit] NOT NULL,
    [HolidayCategory] [nvarchar](max),
    [Description] [nvarchar](max),
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Holidays] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[WayPoints] (
    [Id] [int] NOT NULL IDENTITY,
    [CityName] [nvarchar](200),
    [RegionName] [nvarchar](200),
    [Address] [nvarchar](200),
    [DeptNm] [nvarchar](200),
    [BranchNm] [nvarchar](200),
    [Longitude] [nvarchar](200),
    [Latitude] [nvarchar](200),
    [direct] [nvarchar](200),
    [limit] [nvarchar](200),
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.WayPoints] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[TrafficSMS] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](200),
    [Mobile] [nvarchar](10) NOT NULL,
    [CheckCode] [nvarchar](10),
    [IsAction] [int] NOT NULL,
    [CaseGuid] [nvarchar](200),
    [Times] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.TrafficSMS] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[TrafficSMSCarInfoes] (
    [Id] [int] NOT NULL IDENTITY,
    [CarNO] [nvarchar](10) NOT NULL,
    [CarType] [int] NOT NULL,
    [CarOwner] [nvarchar](10) NOT NULL,
    [Pid] [nvarchar](10) NOT NULL,
    [CarAllow] [int] NOT NULL,
    [checkStatus] [int] NOT NULL,
    [TrafficSMSId] [int],
    [TrafficSMSCarInfoRejectId] [int],
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.TrafficSMSCarInfoes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_TrafficSMSId] ON [dbo].[TrafficSMSCarInfoes]([TrafficSMSId])
CREATE INDEX [IX_TrafficSMSCarInfoRejectId] ON [dbo].[TrafficSMSCarInfoes]([TrafficSMSCarInfoRejectId])
CREATE TABLE [dbo].[TrafficSMSCarInfoRejects] (
    [Id] [int] NOT NULL IDENTITY,
    [Subject] [nvarchar](200) NOT NULL,
    [ListNum] [int] NOT NULL,
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.TrafficSMSCarInfoRejects] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[RefugeStations] (
    [Id] [int] NOT NULL IDENTITY,
    [DIstrict] [nvarchar](10) NOT NULL,
    [Village] [nvarchar](10) NOT NULL,
    [Subject] [nvarchar](50) NOT NULL,
    [Address] [nvarchar](100) NOT NULL,
    [Number] [int] NOT NULL,
    [Precinct] [nvarchar](20) NOT NULL,
    [Twd97X] [nvarchar](20),
    [Twd97Y] [nvarchar](20),
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.RefugeStations] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[RoleMembers] (
    [Role_Id] [int] NOT NULL,
    [Member_Id] [int] NOT NULL,
    CONSTRAINT [PK_dbo.RoleMembers] PRIMARY KEY ([Role_Id], [Member_Id])
)
CREATE INDEX [IX_Role_Id] ON [dbo].[RoleMembers]([Role_Id])
CREATE INDEX [IX_Member_Id] ON [dbo].[RoleMembers]([Member_Id])
CREATE TABLE [dbo].[NewsCatalogNews] (
    [NewsCatalog_Id] [int] NOT NULL,
    [News_Id] [int] NOT NULL,
    CONSTRAINT [PK_dbo.NewsCatalogNews] PRIMARY KEY ([NewsCatalog_Id], [News_Id])
)
CREATE INDEX [IX_NewsCatalog_Id] ON [dbo].[NewsCatalogNews]([NewsCatalog_Id])
CREATE INDEX [IX_News_Id] ON [dbo].[NewsCatalogNews]([News_Id])
CREATE TABLE [dbo].[EseCatalogEses] (
    [EseCatalog_Id] [int] NOT NULL,
    [Ese_Id] [int] NOT NULL,
    CONSTRAINT [PK_dbo.EseCatalogEses] PRIMARY KEY ([EseCatalog_Id], [Ese_Id])
)
CREATE INDEX [IX_EseCatalog_Id] ON [dbo].[EseCatalogEses]([EseCatalog_Id])
CREATE INDEX [IX_Ese_Id] ON [dbo].[EseCatalogEses]([Ese_Id])
ALTER TABLE [dbo].[Units] ADD CONSTRAINT [FK_dbo.Units_dbo.Units_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Units] ([Id])
ALTER TABLE [dbo].[Members] ADD CONSTRAINT [FK_dbo.Members_dbo.Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[News] ADD CONSTRAINT [FK_dbo.News_dbo.Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([Id])
ALTER TABLE [dbo].[NewsLinks] ADD CONSTRAINT [FK_dbo.NewsLinks_dbo.News_NewId] FOREIGN KEY ([NewId]) REFERENCES [dbo].[News] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[NewsFiles] ADD CONSTRAINT [FK_dbo.NewsFiles_dbo.News_NewId] FOREIGN KEY ([NewId]) REFERENCES [dbo].[News] ([Id])
ALTER TABLE [dbo].[NewsImages] ADD CONSTRAINT [FK_dbo.NewsImages_dbo.News_NewId] FOREIGN KEY ([NewId]) REFERENCES [dbo].[News] ([Id])
ALTER TABLE [dbo].[NewsCatalogs] ADD CONSTRAINT [FK_dbo.NewsCatalogs_dbo.WebNewsCatalogs_WebCategoryId] FOREIGN KEY ([WebCategoryId]) REFERENCES [dbo].[WebNewsCatalogs] ([Id])
ALTER TABLE [dbo].[NewsCatalogs] ADD CONSTRAINT [FK_dbo.NewsCatalogs_dbo.MetaIndexes_MetaId] FOREIGN KEY ([MetaId]) REFERENCES [dbo].[MetaIndexes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[NewsCatalogs] ADD CONSTRAINT [FK_dbo.NewsCatalogs_dbo.WebSiteNames_WebSiteId] FOREIGN KEY ([WebSiteId]) REFERENCES [dbo].[WebSiteNames] ([Id])
ALTER TABLE [dbo].[WebSiteNames] ADD CONSTRAINT [FK_dbo.WebSiteNames_dbo.Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[WebArticles] ADD CONSTRAINT [FK_dbo.WebArticles_dbo.MetaIndexes_MetaId] FOREIGN KEY ([MetaId]) REFERENCES [dbo].[MetaIndexes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[HomeLinks] ADD CONSTRAINT [FK_dbo.HomeLinks_dbo.WebSiteNames_WebSiteNameId] FOREIGN KEY ([WebSiteNameId]) REFERENCES [dbo].[WebSiteNames] ([Id])
ALTER TABLE [dbo].[HomeAds] ADD CONSTRAINT [FK_dbo.HomeAds_dbo.WebSiteNames_WebSiteNameId] FOREIGN KEY ([WebSiteNameId]) REFERENCES [dbo].[WebSiteNames] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[HomeThemes] ADD CONSTRAINT [FK_dbo.HomeThemes_dbo.WebSiteNames_WebSiteNameId] FOREIGN KEY ([WebSiteNameId]) REFERENCES [dbo].[WebSiteNames] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Villages] ADD CONSTRAINT [FK_dbo.Villages_dbo.Towns_TownId] FOREIGN KEY ([TownId]) REFERENCES [dbo].[Towns] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[AboutLinkCatalogs] ADD CONSTRAINT [FK_dbo.AboutLinkCatalogs_dbo.WebSiteNames_WebSiteId] FOREIGN KEY ([WebSiteId]) REFERENCES [dbo].[WebSiteNames] ([Id])
ALTER TABLE [dbo].[AboutLinks] ADD CONSTRAINT [FK_dbo.AboutLinks_dbo.AboutLinkCatalogs_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[AboutLinkCatalogs] ([Id])
ALTER TABLE [dbo].[StationInfoes] ADD CONSTRAINT [FK_dbo.StationInfoes_dbo.StationInfoes_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[StationInfoes] ([Id])
ALTER TABLE [dbo].[StationInfoes] ADD CONSTRAINT [FK_dbo.StationInfoes_dbo.Towns_TownId] FOREIGN KEY ([TownId]) REFERENCES [dbo].[Towns] ([Id])
ALTER TABLE [dbo].[StationInfoFIles] ADD CONSTRAINT [FK_dbo.StationInfoFIles_dbo.StationInfoes_StationInfoId] FOREIGN KEY ([StationInfoId]) REFERENCES [dbo].[StationInfoes] ([Id])
ALTER TABLE [dbo].[EseFiles] ADD CONSTRAINT [FK_dbo.EseFiles_dbo.Eses_EseId] FOREIGN KEY ([EseId]) REFERENCES [dbo].[Eses] ([Id])
ALTER TABLE [dbo].[EseCatalogs] ADD CONSTRAINT [FK_dbo.EseCatalogs_dbo.WebNewsCatalogs_WebCategoryId] FOREIGN KEY ([WebCategoryId]) REFERENCES [dbo].[WebNewsCatalogs] ([Id])
ALTER TABLE [dbo].[EseCatalogs] ADD CONSTRAINT [FK_dbo.EseCatalogs_dbo.MetaIndexes_MetaId] FOREIGN KEY ([MetaId]) REFERENCES [dbo].[MetaIndexes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[EseCatalogs] ADD CONSTRAINT [FK_dbo.EseCatalogs_dbo.WebSiteNames_WebSiteId] FOREIGN KEY ([WebSiteId]) REFERENCES [dbo].[WebSiteNames] ([Id])
ALTER TABLE [dbo].[BigBanners] ADD CONSTRAINT [FK_dbo.BigBanners_dbo.WebSiteNames_WebSiteNameId] FOREIGN KEY ([WebSiteNameId]) REFERENCES [dbo].[WebSiteNames] ([Id])
ALTER TABLE [dbo].[Wandas] ADD CONSTRAINT [FK_dbo.Wandas_dbo.StationInfoes_StationInfoId] FOREIGN KEY ([StationInfoId]) REFERENCES [dbo].[StationInfoes] ([Id])
ALTER TABLE [dbo].[Cases] ADD CONSTRAINT [FK_dbo.Cases_dbo.WebSiteNames_WebSiteId] FOREIGN KEY ([WebSiteId]) REFERENCES [dbo].[WebSiteNames] ([Id])
ALTER TABLE [dbo].[Cases] ADD CONSTRAINT [FK_dbo.Cases_dbo.Cases_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Cases] ([Id])
ALTER TABLE [dbo].[CasePoprocs] ADD CONSTRAINT [FK_dbo.CasePoprocs_dbo.Cases_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[Cases] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[CasePoprocs] ADD CONSTRAINT [FK_dbo.CasePoprocs_dbo.Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id])
ALTER TABLE [dbo].[CasePoprocs] ADD CONSTRAINT [FK_dbo.CasePoprocs_dbo.Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([Id])
ALTER TABLE [dbo].[CasePoprocLogs] ADD CONSTRAINT [FK_dbo.CasePoprocLogs_dbo.Cases_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[Cases] ([Id])
ALTER TABLE [dbo].[CasePoprocLogs] ADD CONSTRAINT [FK_dbo.CasePoprocLogs_dbo.Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id])
ALTER TABLE [dbo].[CasePoprocLogs] ADD CONSTRAINT [FK_dbo.CasePoprocLogs_dbo.Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([Id])
ALTER TABLE [dbo].[Casewqs] ADD CONSTRAINT [FK_dbo.Casewqs_dbo.Cases_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[Cases] ([Id])
ALTER TABLE [dbo].[CaseFilters] ADD CONSTRAINT [FK_dbo.CaseFilters_dbo.PoprocsSubTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[PoprocsSubTypes] ([Id])
ALTER TABLE [dbo].[CaseTraffics] ADD CONSTRAINT [FK_dbo.CaseTraffics_dbo.Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id])
ALTER TABLE [dbo].[CaseTrafficPoprocs] ADD CONSTRAINT [FK_dbo.CaseTrafficPoprocs_dbo.CaseTraffics_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[CaseTraffics] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[CaseTrafficPoprocs] ADD CONSTRAINT [FK_dbo.CaseTrafficPoprocs_dbo.Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id])
ALTER TABLE [dbo].[CaseTrafficPoprocs] ADD CONSTRAINT [FK_dbo.CaseTrafficPoprocs_dbo.Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([Id])
ALTER TABLE [dbo].[CaseTrafficPoprocs] ADD CONSTRAINT [FK_dbo.CaseTrafficPoprocs_dbo.TrafficViolationsClasses_TrafficViolationsClassId] FOREIGN KEY ([TrafficViolationsClassId]) REFERENCES [dbo].[TrafficViolationsClasses] ([Id])
ALTER TABLE [dbo].[CaseTrafficPoprocs] ADD CONSTRAINT [FK_dbo.CaseTrafficPoprocs_dbo.TrafficViolationsRejectclasses_ViolationsRejectclassId] FOREIGN KEY ([ViolationsRejectclassId]) REFERENCES [dbo].[TrafficViolationsRejectclasses] ([Id])
ALTER TABLE [dbo].[CaseTrafficPoprocLogs] ADD CONSTRAINT [FK_dbo.CaseTrafficPoprocLogs_dbo.CaseTraffics_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[CaseTraffics] ([Id])
ALTER TABLE [dbo].[CaseTrafficPoprocLogs] ADD CONSTRAINT [FK_dbo.CaseTrafficPoprocLogs_dbo.Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id])
ALTER TABLE [dbo].[CaseTrafficPoprocLogs] ADD CONSTRAINT [FK_dbo.CaseTrafficPoprocLogs_dbo.Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([Id])
ALTER TABLE [dbo].[CaseTrafficqws] ADD CONSTRAINT [FK_dbo.CaseTrafficqws_dbo.CaseTraffics_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[CaseTraffics] ([Id])
ALTER TABLE [dbo].[TrafficDepartmentdetails] ADD CONSTRAINT [FK_dbo.TrafficDepartmentdetails_dbo.Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([Id])
ALTER TABLE [dbo].[Trafficdisdatas] ADD CONSTRAINT [FK_dbo.Trafficdisdatas_dbo.Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([Id])
ALTER TABLE [dbo].[TrafficRegions] ADD CONSTRAINT [FK_dbo.TrafficRegions_dbo.Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id])
ALTER TABLE [dbo].[TrafficRoaddatas] ADD CONSTRAINT [FK_dbo.TrafficRoaddatas_dbo.TrafficRegions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[TrafficRegions] ([Id])
ALTER TABLE [dbo].[TrafficSMSCarInfoes] ADD CONSTRAINT [FK_dbo.TrafficSMSCarInfoes_dbo.TrafficSMS_TrafficSMSId] FOREIGN KEY ([TrafficSMSId]) REFERENCES [dbo].[TrafficSMS] ([Id])
ALTER TABLE [dbo].[TrafficSMSCarInfoes] ADD CONSTRAINT [FK_dbo.TrafficSMSCarInfoes_dbo.TrafficSMSCarInfoRejects_TrafficSMSCarInfoRejectId] FOREIGN KEY ([TrafficSMSCarInfoRejectId]) REFERENCES [dbo].[TrafficSMSCarInfoRejects] ([Id])
ALTER TABLE [dbo].[RoleMembers] ADD CONSTRAINT [FK_dbo.RoleMembers_dbo.Roles_Role_Id] FOREIGN KEY ([Role_Id]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[RoleMembers] ADD CONSTRAINT [FK_dbo.RoleMembers_dbo.Members_Member_Id] FOREIGN KEY ([Member_Id]) REFERENCES [dbo].[Members] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[NewsCatalogNews] ADD CONSTRAINT [FK_dbo.NewsCatalogNews_dbo.NewsCatalogs_NewsCatalog_Id] FOREIGN KEY ([NewsCatalog_Id]) REFERENCES [dbo].[NewsCatalogs] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[NewsCatalogNews] ADD CONSTRAINT [FK_dbo.NewsCatalogNews_dbo.News_News_Id] FOREIGN KEY ([News_Id]) REFERENCES [dbo].[News] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[EseCatalogEses] ADD CONSTRAINT [FK_dbo.EseCatalogEses_dbo.EseCatalogs_EseCatalog_Id] FOREIGN KEY ([EseCatalog_Id]) REFERENCES [dbo].[EseCatalogs] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[EseCatalogEses] ADD CONSTRAINT [FK_dbo.EseCatalogEses_dbo.Eses_Ese_Id] FOREIGN KEY ([Ese_Id]) REFERENCES [dbo].[Eses] ([Id]) ON DELETE CASCADE
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](255) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId])
)
BEGIN TRY
    EXEC sp_MS_marksystemobject 'dbo.__MigrationHistory'
END TRY
BEGIN CATCH
END CATCH
INSERT INTO [__MigrationHistory] ([MigrationId], [Model], [ProductVersion]) VALUES ('202202221413281_init', 0x1F8B0800000000000400ED7D5B9324B975DEBB23FC1F26FAC95684A6676677492D63568A9EBEEC36D53DD33BD533BB7CAAC8AE4257A7272BB326336B7B9A4F6BCB9248D1B218619172C80CDD42A6245AA2BC94254AA444FD999DBD3CE92F18C82B1277249097AA46C4C66C352E07E7200FBE73001C00FFF6F37F7DF86BAF96C19D8F409CF851F8CECEFDBBF776EE807016CDFD70F1CECE3ABDFCE55FD9F9B55FFDF7FFEEE1E17CF9EACEF3B2DC1BA81CAC1926EFEC5CA5E9EA6BBBBBC9EC0A2CBDE4EED29FC551125DA67767D172D79B47BB0FEEDD7B7BF7FEFD5D0049EC405A77EE3C7CBA0E537F09B23FE09FFB513803AB74ED05A7D11C0449910E732619D53B8FBD254856DE0CBCB3731EAEE679A99D3B7B81EF410E2620B8DCB9B37AF36BCF123049E3285C4C565EEA7BC1F9CD0AC0FC4B2F4840C1EFD7566FAAB27CEF016279D70BC32885E4A2B095C83B9530509C4328767A83D8CA447A67E759E8A7780958E6D7C14D2301269DC5D10AC4E9CD537059D43B9EEFDCD96DD6DB252B56D5B03AA869F82B4CDF78B073E7F13A08BC8B00543D04BB7092463178178420F652303FF3D214C421AA0B32D6A9568936CEBC181614B494C66B202332595FFC27304B4B1AF093426DDCB973E4BF02F313102ED2AB8AE353EF559972FF1ED449D89F5079AB762809C50D67FAA4D9EC5BA25655A43DF193F4F17A29FB36927E8F12F8A134597F60CABA0FB5F749BCE8BBD963D8EC0154CFB25DF4FB1CC2892E9D67ABB93740AFE5CD0ED06F79C3AD7AEEB1F791BFC8409039E03318BBF314045991E4CA5FE5A87C17654CF1324771B47C1A0505F46159D349B48E6788B3889D7FEEC50B90AA73760A9617D05E31D9CAF3A6A73724538D8CAAC992A5666EC930CED0C3DD1AE285C09F93DA26E8DF9BCD2268D66DC2A71AF47949721DC573CD86E14FC3E154363CF102BBB64AA571F46FEF5DBD6FBB551549CF0F4FFA6EF234BAF083DE057DF6545F506349C132EA7DE0408C9963661701729E8412B495F270E9F981B629351E0BA73767FEAC6F15F97A7471EEA703A826B477721B22814B102FFD24C9E6513D6BF9DEC12948126FA1DB6F166C84F3CB9D5F3E845F5EBAB6ED9D5FD21F67BBC6AAFC201A6C5F1CE54C2B67BDE6064FA73CF146A691238E286D931B3ED8E2496B7CFFCA3D73801F62E1C68A5174F6C1D98741EC83607544099149F3C084EB56883CB981636279122D1C2C8B5DDFEE10C6181BDB0D758DD5B3D43B8633C6574E45CC9792267E0AF661EDDE1B3EBF02DACB49E653B2ACD5160B59E62DEF7B2F86697410692720FEC89F0DD6EE2032BBBD3CE7138EDB275436B18FC175B255D61596D65DEAB6605B5BD9740BCBD37B71EACFB4178ACD11F0B98FF4C608FF26A9175BC184C3706E83CC5E92F88BB0AE6D462D9F221946C81C27FB5178E9C795A5C9E66D8F223807F3C256BB281F808B7D28D1228A6F4CD7349E5C87901AF22ADDEA88B384A3B584A2D51164FC4EFCF0057B7DA4CC9D6636125B206964506BD6CD5CD6A2B58CA5233F00096719BDCA677255E730D9C2B2DBF075BCF41622BE503E60F255E730F9C2B2DBF00511CD0BA2059FB1A240D602A0986BE63219248AE832E965A6A58805E231396D966AF2D8C8A416E5E812462B73A5066F955FD88187A614C003AE4DCD23FA14CF62DDD807F3EDF46A20D4AE479ED4CAF3B03267B6E8BE38A7C3391D83391D26FE060BFC696FA435F0673E82437E0B737331F4ABA9DF519B00C5511C557050EDA07AABA15A6916C6026BC624AD355A67332787D60EAD1D5A3BB476686DB836C5426BC6D2556BB42E9691B60AAF07D96F53832BB57D9AD433453CF936C8A8F710AD1810DB1B55CE8A382B328815C9A1258369962D81D9E48640BDFD501B1641316A8341545677A7A18E9894ED856025D9DB215501A659A44BE91E15295053CA68558ECD66912D64B22CA3CB62B95F64635B49C41FB1F3D4CABF686AD136B918275EB858E3A7FA50FF9789AD8C8B33B5CED43A533BB8A995EEDD5B30B624ECAA18E6B6F88B6C4C168FBC45E07BEB4E2BC0B10511FA427B689BB7FCE132388874EF1AB07072200AA2B84518BDB9C0CF56D9E28A7E6C837958C66188CA5A33A156EC7A31D40E750FD69A7F08880A57BD376ADFADB37252777D81C81C3CEEFF233837CCB96143B861823B2430A7867191049DCB72B7C822A687864F41B8761E9605EBAB6D76DF346DF2B917AC75DD0CE346DD613D07CEE306679D396679E06B8B00D0C616DCB3B0A630F2537FE6EDE631FE8338ED1B7CDED0F294CF590467118671D7855B7CB58960EFF0B1F2595E3B5DC8C86F3F07CB5580BA618BECD660BB4803AE9B3D0250CB70A11D8C3A181D2D8C2AC3D37BD112B8138F96E0097E1B2FC702A3D5F0915D0F51806EEF883B04CA5B5932B1EC6F632B89868196EEE20A67BC36C078B58D9D2B6D192B708ECCA33C7FAA8091D78FA8EDCD9D51B5E7F33BF3336AF3E3AC86B31A9B6B35F6E63C9B51E7302D06966D6C2FF2DB629DC97026C3990C67329CC918B7C9C8D09A67351A994CC3D12C61B6C3105D87CE6C5878E0C5C5CB38C01A37602963C2731F12D9AEFB5110CC997A130E5A1CB43868D1F685320F83E508153033CD0BD43E109E4EB93F8D4C23CF67EF225AA768D1760BCFFE0E76BDC8ADBFD0C3C1AC83D9D14D3949A863CD3C79652804E616D4BD26A222C43EC55C654FAB8B4518DC9699D4F5207409A387502B725B652686825A4BB7500D18D1A7FF1CBD85B7BB522F5DD70FB98EE2F4ADB377CEDE0D62EF44B74D699A0EAE85A38C4BDBE347F9B50C41143BEBB1C1775C38C07480396EC054C6A4A3284C27FE37B76A8D77B0F5D947594F1AA1C2A93989893909874D0E9B86C72634CD81FEDC7178196D133C9D79312C6838E395EC6375B7BE6A0126CF41FF8F712144F38241BCC5BDF93C0649D2BBC8E7D7F3B7BFFA61DFA890B5FA8DBE5B1DEA5CBD9D8BCB06D9A471C7789CB7B001DE8268E927B7A4859BC05C00C25C882951BA5E08E216A21684F82575F73CB89BE17813E486389927E48FBB312E620B23C0DE8C31EC50727346DEF52D383F3AE6BFFF4B169BE24E265B04AA9C480ABAB0D16E1349799BFCE0C1369DB04EBDC54F5639ABEDACF62056BB81B85D41B4C032F2F1BC15441F26DBB5783AC86464B8435EE11CFEE5BD80CD4DF3FB597B8E1FD85BAD829BE912A45791EE0D7CE6D3D7C700CC87B8331DFE39D35F1231EFECB3398664BDB5AABFDE65DEE611FA463DB779EAF9FD0BFA142CBDF845EF1AFC240CFCB0FFF526CB913FA760A98BF5A3B88BD2AD5D392F78C3BDE0EC156BCE2205742951F614B99698C38B25534B10789EEED289F00D2148B00C7E3A245E6923B2583C35F28D56420A01B7CAD51ECAE9857DE9163E1CE43BC897356C17F233386F07F7E47206CB14B4855577ECCDD222867B55DBA28D7250EFA07E73A15EF20A36E619E32599AE755D80650518A574F77CC517E2636D302FC467E58B18EDE6C1EB664F90471AE95C4957B63AC6782898CEA94EA1046C1D1A3F71FDC85F3CF2426853B7CAD4DFB6EBE1C67855DB108720ED5D68EDBC04E7250CE525702D5A05D62C8346655286832E617674D10BE79E331BA30A3EBA000B3F0C33B6FB16620E92D40F8BB8BFBE1B1F2AD6DBF2DE9BDBC372F66BC3ED972C922BB31ABCF02D2A937E468D2A6164C3F6BDED8AD442F2E4C88321529DA80922A8E2F14117686E47D277D7BE6E7C94F95C6BB87B71A230857DD37F4098B7B43BA35592166AC61C8362A4C779522B4D3E5CDA8E43523BD578A83B0736D7CFAF4717BDB7797CD67B9376B632CE623097983FC590D101C244A1C7607D6CAAB4BB9A69A3D09BA6A2DA70EF2679A59E7BEB709056CFB42DE3FDC18E535B08986E759CDA422F0F28F3D95042678F0C0C60D89E5C5EFAB3415A46BEE653B08AE294E95BE359BA13FE55E0CDFAD79DE30419ACBD24F11761431CCEE285D2E7D98B81D73BE6D8B83763FFCA07972850FD227A85FAC590DC71B2B74EA3FD204A80ADBE3DF203383B2A3BB5E700BBBCF186E6F7153DF36C75E907E07EEFE3236FF7C140EDBED1BF2D71AB856EB5706CBB5DC8B4B236BAF0746A7DB091A91BAD7116ADE268C60ED84084F3FC69B668483084655131EF64BE6E2C7EC15670C28BC7AF1B804504EC95B9020EAB22FA07061270FD92CFDFF54B2663453293A3324FBBBF32A7609637C753AC462142B9B03CB682E10574950C55E27793325FAC0E6332DE7A5D3C57886D5B1D37DD58B3BFC28EAF0F182D401907182F2F406C1A1B4C6F8866BA941FEE2D73757777B3894ADD3766763BA736C831C7C3706E4B8A0310DC0C22C31C367C19780B5B731B2488AD4E196ABE809AEBBFD51436D7FF1C258C56C10D9CEA27DA711EE6BAB76A7547808595CDCC01C361BF6550614667B2BEB040CACDD8DC8C6D80199BD0AB569C20B17C6AD6044A95272FB3E8F91E9C9833BC2493BFBA80884BAC543B5E735F4B8DDBB2AC80DFBC889CE3A29C71C84C3557BC45B303A50137122F7C6F663D0C53BDD941BC52670F9D3D1CAB3D545C94E3A337B56E67DD30A21664B6B15946C26E3F16B26E496424C9524AACDB3295D72F9D8D6C9078FFBE59F50766D5DF30AA3E40A09BB36CCEB28DCFB249B67358084B6EF5B486D43C2061ABEEBD66C458943D8D67E99E2B6B15AA2E76C6D5E29E617BEE00AE83BD4D843D7291960180CD22530C939A373988CA511BB8C2C2469BB984445B049B8301DC50E74D1D2A3A541CCC19C430AE2B5024FD4625046DED489EC71E8A29DF2644740740DD0150DB4607FDDBBFB4964F30B943A75D8E89210E9D0E75FE698035400BFB8AEE28459FEDBE3950BB6F0DD4EE577A6FF7233FCA5DD0A99503DC35B954FFB8AE798099A563E8B5146DCE159A1F15ABDB9F796D6E5BB6D67C26FED46B7322D1361371E4E93B32C633F6142CB5FBFFAD311E2B752B1E6EC56390150F85D88962FD4010394197606E8D318AD93EBB56342188D0A44B300F19318A7572A0ADD10E2F84865948CE7667A7DC8A565E5E8BB82D72456C96458C0F7135C4DEB6852E2B67B9DCE92B77FA4A9B943B7DC51A05EEF4953B7D35DED35785297C5ECECF92FD00FA5B86885B537B0AD0AEC0CC024D37E571539E41A63CC288BF561308C164A7E3035FCD56A43334B5E35FDCC29DC4B8B35A63C7B9F34B2A8B218877170942A0A9A22C542D913844610589C81AED85C2405D5BB4465D3501B12A5A62E2F58CA222D836729BE68DEEF13AE71A38D78049A93D4AE050E7B0C26185C30A8715BC4568776BC058578FDDAD010E221D448E6CA5456FE7533A69ECF22A01AA21C55517858B0544E5BB5F7B915D33202CAC238FAD4B07AA5D6467691B24DCDD03CE3EF2FBD7D9C78DB28F6A413602F425E3704CD642D0F311FB5760F662DB1077902356039D9C5141003542C97E145EFA71B58263FC326C41AF05775A8E835363A7C61BAEC605201F809517A74BA8217390A2CFB0450A6D65B5C7F976CEB71BC4B793CEBF3923983B0557294FF9814A956C7886733F817DE839FC2191207B264DFDB090DBF372A0B6F9A056A0810CCB58C57810C62C6B03B99E8205DA0FDA22E01A6C43DFC2169F8B0970F8B8E9F8C8DD3E6A200E67DF885786878B74412BA01879F36DF3E7F29E323D7BE682A51C303A60D405C6C2C91281628138D3B2280D89CD125C40248A19C1E17B51E0CFBD9B6D82C1E3A414AA68AA98896A834941661F72B188E21B4D35350F6B3A00C92CF6572DA2B95C489503D21103A9323A7DE0DD9C457E986E133CEDC332E857EFDB58B9B918A4E9A1EEF83B00ABF4F1B2F7661FC55E38BB1AA0E193285CF8E97ADEFF173E815ECF200DCFFDD8F67445A5D9C05FFAFDB7EAACB2B3CAC35BE5621E32399D6C935D1EC4309E4617BEF6FB03E6172467B141FB91365C1B5F02729C10A7538C2351860A0A42E3299169A5437387E6235CAC4A0B005F26FB5E7C1C5E46EC5B116A9C2F8A4DEB8A8CE52B6639EA763C6161A37BF228CADB649DA0488F9FF46F273CD66366EDAED187F59E5C87DA23D75C88215E2080C2EE0541744DF65C91A8496D86CC35E35240A8EA2D2F03ACC78AE1761135E8F23B045C50A3B38C9B6D19BBB3889C6D1DB1F95415E09C34EDF9785493262F3B65B90702D9B8B5E4B69F5FD5AE275074C216F9036E9BDEE1BBC3F7DE663EB691516A03E4A0DA0A1921B0AD1700398D5B16127A709C40B5D40644734FFFB90F8B2FFA5FC26B6700846F8328B5DB6E2B4FFC148A52C3D0E85CD4E8D7D2EEC460E6872DCCA6F1636ED7F3B7BFFA61DF089AB5FA8DBE5B75C6DD19F7CE8C3BDFD2AD97989DC39F7B3C4E8E2042279518CF50567003C5C4A1A4C96D7EFCA220F6F9EFFF74E7CE732F58C3DFF728C11A455FFFEFBFAB8ADEA739CF7914F09D1B774B7CBFFEBD1F7EF9FDEF7CF1C3EF7FFEA33F5015E0B31FFCF5EB1FFC5F58EDF5F7FED84892132F5CACA161B424CBA7FFF8379F7DFFB73FFFF6B75405F9E23B9F1015DA48D1D89A3216E2B3FFF9B7CA8AF4DD1F1A315EBF156CCCF5973FFCFE97DFFBE9A7FFFAA79FFFF81365F6BFF55BAF3FF91856FBE25BBFFDFACFFFE2CB3FFC1DF41F45E5BE840ABFE603891AFFE8CFBEF8D6B7BFF8C98F3EFFF8B7889A6F886B7EFE0F3FF9E2A77FFBFA4F3EF9EC4F6BD57953C2E7EFFDC6177FF1311A3A9F7CFCD91FFEE567DFFEF8F5277FFDD95FFDF1977FF0838AC45B12127FF5C3D7FFF4CFAF7FFCEDD7BFF9975FFCCD0F3FFFF39FA161F8F33FAAEA7FA5A5123C05AB28B685289F7FEF1F3EFDD9773EFDD9CF9447E19FFD9FD73FFE3BBC82E493C32EFBF45FFF0B5EE1414BC1CF9AAFE5980FDE1FFC086AF567FFEF17CA63E017BFF9C52FB8A3982AFDD39FE0A525FAFDF9DF7F17D74F894E7FF9F1C7100B95B5F993EFBDFEF93F282BEEF77FFCE9BFFCEE67BFFF4F78CF7C45A215BFF821CEFD575B7EE1FC09795BC6F22FFEC7EBEFFEAEEAB7459778292AF4FFFAF9EBBFF9EE97BFF1F79F627DDA4EA76D89FAD927FFF2C5CF7F477908FFF3F75EFFD177209AE1756442FFF6EFD2756490FD9DEFE3A5DF68D745C5069B711F41563EFDC79F4348D7013BACCEEBEFFEB7CFFEEBEF1999706C77CF5CBB7FFA932F3FFEC32FFFF37F57F7037F445490E1D72F7EF3F5DFFED5677FF28F7A4ABE9724D1CCCF96A3CA99013AE277E6C5204CB3A383CD360FC3F99D7CC98C28572FA9D5FBA3F9D1C3D37590FAABC09FC169C33B3BBF4489C12759ED5F8849DEBB7B97EE9CA7E0122042BE17EC476192C69E1FA6F4FA9A1FCEFC9517881920AA292ECCA12EAF1A20730EC00A4D91C254DC9B2A2DE7B5D8ED57CD104B86B2DE79B88B2986585F8A93F2A737426D699462E94A79865F5D5B9A241575E53ED9A50F9F8407200029B89347C665A66DE6CDE919341C3F730B3AC664BB070D637E019576CB43F7836817E27D9AB34EBE89506B025E88A55B285F47B31AF4188AA5A4AB1A423E06D7C9891FBE98A21F5C291BA55862960574446D1265C89AB134BA41C464BB8741C4FC062AEDC2F2838D21C42BF4D54122D7AFBA184FC1B212BA1A86915554317B369DDD7E4FBA42F7E72628CBF1D25B00B9B2D4C578CA9295D055168CEC20CA42B7DF93B2D0FD397665F9005C2076F7BDD40BA2C514FBCDD71C411D961A358B2B7D79B5B638BAC5698856DA56FAA5207C0FCAA6D02D2A5C4032E5D9FC41E1AA94E314A4DE316CEF9510B5A8D23CF052D604197DA60759B23A4AD78A2B434F48C8FD462AED67950644C4899F02F4876C4A4C17E5E05F594A471319C4376686CCE7BD1F6CE47C95D1CF95F1915388A1848445D98E70B0A4CE503F816EDB75E938BCF48C66443F2B9A585465142A857E03F18CA059B423852A882BCE0DCC56659A178C8AB8E3DC31AAC59C84A8F24A94DDC1C364A2A7A1C3EC553527A0BC5B792837602F4EFD59B68E28F34A598539AE40514ED313A0A96F964B2A12A11F9780FB7D46EF90BE172D41B66229F308C8822C0D2CCBE8E81F4577284780C7480F1AC4EB5B0D1700FD31A812EDCD9554A82EC653A0BDB9AEFA6034F5946778E862B3DF93CAD1DF629314EEFC0AC039988ACE354AF2D42E2BA4AB794DCA9BA87C4C097AD23FE677D914152C0E164DCFA36BF2A456AD23782196E255A793D4D5AE4192A171193FA3533516D73D6819ABFF559A45E507D3ACBD8B689D22774075B1865781A57164591DD5E3B63394CB2663A8070D93F5FD26ACE254324C4BA5906A405952A862AD74ABA2CC502A99F276A059243B7DAA14D9C92A6D0FBEE3561C5ECE2ECAC8430FD9C799EB4FCFADC1522EACB08E7AF1DB60A899A00D7B1A26E5A8074D93F6BC0A0F03C797E232087D31B26047DAA5E39775A34D3DFB58BC7E1DBD9F85317E748CE2B0F0AFAFF0A9A94A128DCACAB7542BBAAD11E01697A97EB58EFB1D54D8C0AA0DA68887D959293085FFE7EA1D5686A56645B68E76E11419CA749874E8B833DAEE4167187DA8D22A2C3EA46A60130B997BCE2CCD519716F33F3679F6E44F3728AFAD1AF119EA47A1F8FDAD38F31BDC61C78490EF45B20A77A2609BBA172912A15F8DDCBCBDC8E670122E79D145BB82B94117B8F8ACF48E6E9BB6A885717F288A4B22CA75A246879C8024868FD53E1EE991BF78E4852188A583872AC912BA2AA423334D79A891C3E5A48781C3ED5F8D7133E8DED6075E38F794A6C354496660122AA4159344511D6AB6CBE5A40735E2F6EDA6CC6BD125205230C20BB19407E5EBE84E83DE50E8C362A2078D61F5E526D8EAEC3EA0681547B369F6B945DF162BC75397BC88AED2E084197AC3D2C3E1A71B1CC67BD235C6B7506919951F83AE610FA02B2806EB29748B7AC77840BDB77B4D846CF4AB4B742FABB43FE8B91A8A7BC971004EF90EB56AB80302125686D0AD8D3B2C504B7002E7928A06B22C2AD6AA13BD292D83BCA2A5EC42A3481E7A5526B27F37C8EE21D6B54C5FB342871A350E1BC8E6A477EDDA684B580BA06C0CC92A9DEBD918AC228F9B81B46D236DE3F54BB9512CCAF074EAFAA5AE32950407B17F44E33D290BD1871B61F18AB0B6995441F082D696A1709A83280A8B83BED6A1181DAAD2F4C0E18439222693F505FA50D3FA1E63D11E92A8124B999AE5352F2D1236C6D1B2A24047B716A988DF83D6A9748C0A1BA8F6A0A055BC36A5EAA4D3C579005694D4C531460383B8E77C3E7AC2347E4F6F846B5EB0AFBAD24E1797E855BB952A46331C14E3A8EF3856E1F962F4AB9C9BBA26DF645F0FFA9457E82D2AEA48C070E8657BD917D84060545EB7E057EB4DFF865DC3907334A0166EE45A465390E77E54BC7DBA1F40A1349491A8D9833E922DB28E40E51548A97A54510E93836829E71329CD50981D394AC5CD1FCA9DB5565FAC7EAF4A8CB7ABA2CAB89C83283483E181D59AF1E954386212188F6E2BEDC1326B28E96F8B7D0D76637A73A80E7574A06D5AE127D8BC1991C6A6ADA05E6F2A38AAD9D1A01BBA0A5F63F3E6485ADBBBC29A3D6BE488E64B036FFD2A7D95CD9A35BDBC56B6CC455189F6BDBC6EA97625F911186182957EB58BE8E6D19BDD826DD89217A74B48730E52CF0FD4B04EA5324BE138F574544FA9E901404F87AF1E3453E70B6D00F415E2CCFD64EEA59E9692B2EA0874B328DE4225990D0DA7892276FA534051EF6F8EDE3D050B745594C2AC845741A07179D9160A4737D2FB0444C6497F8AC6EDF1D14F394A01226F9E0D95421F641FBF595CA45F45C9361A4634C25F1E642BB17D456333D4A39AB1BB5D8581BCC6D06A36399DEC7B7176C6362D5296FCF56A512581CAD5E55B281DBB31BEEAC1F29DEB9D90A7FEB44FF81D34B653209DF1E861BEF68D0954A46B68259784928EE6B5356313357810AAAEF238B1A3BED29E1A4299A55DA7A7DA0DAA3DE9F921AC93DEC03A70961582B8BCA8C49BBD80EDA064F08A761A51950948312724D9B99327531E1CA588CDCAE5C3E074F572BA2121803E0DAB3A4A97569EDC2429589EA0C74E690A55A68210D93D4EFE8CC909761596844EF13E174D227F794AA136BA93984720BFEF598148F6E431978FE2F968053AD96BB83C32C5C3C20A64AAF768D984AA7B7F24A49A37DEB1A89177E2C90996D75470A8D5B758C8F5F014846B8E16A22C156E8A278E38CC540F4F49089D83E52AF0522699324F4AA47C2E8745A47E824881C8DE9C47628F02482681EC050B1E8DE24511598F44D721B337B2FB8225958BF70D58F5ABA7252424C83BD559B4E87BD755890AA9A90E82FD288898288EE74B891D417B33F1BFC9ECAC324F3E96EA6B7598E309BF93489D5476832D9331FAFA6009D543369DECA63179D5027E59D5F3EB65E524046A84DFA32621545DA1C5A2835D4D2653207485125373F22BA924D5D15E04AB767E704BA17271248543A28C585226C4F1258853B10AE4AE5FF2E8A093900A04EA33516C32D5812309ADE6991D1635F2AC94027385D3CBE3ADDA7D532725FE8E44009A0CF099C17B6C8BC80998D46D010BA4526BA711CDA6DB4D022DA577F5D589BFBC965045BBB56A3D73EAF9C1FE159831CD1359464A7406591052DCC70BA8F2486E520958A5770ED59A28B6210494AB7D1F3582F9629E805EB920AA48AE58561411AC9671A5DE5AE0CFBD1BB6AF96652918939BB308CE7CD9F624CF9312A9A7F3A29198ADDE2953AA968644F4AA2515B5BEAFC9E658A044BC5C3392341183CBF50214AE0D737A8D17A0C8618B0DCD7582E2C871BE348095C2960CF022E4FA47BD8C4594ACD6CB2A41AA75096A11854FA45CF0921169ACA6C0620AC2173B88F97BF42CD19B05F83C37CAB1C4AE5654048237897428366A6E5A71444BDDC8E7F38B1763C95C2C0209246E506008CCE9B51622974B2DD37CD18696B95980CF72A31C4B6A6CC1472079930C43F472D5C98AE4D9F444283A5642CC745D90277CB55025111FA3D4B5FCD9B296507EAC8498EBBA204FFE72814D223E46A84BF19BCB67D3E6621BDD17A2E27C7904B558BD442DFA09BA4A449AD36F3CBA2D95877EE481A34392D720280DE03F08A12890024526A862EBE236F4AB5C5015D8514629E127270A7394085BE8156B1049AE43DB8A7F86F20A6789BE306F7AE67E5BF2CA67535D21AF7C56EC62C3AE29F14DDC3345293541F2C2D6FAA520D7B5696A461F72FA4310A248F1CF0E4D94B22FA1D3A97356EF83884196594E38F0A9E21C24A93769C44042D3EB1C60CB8D19119A5065F8429045591D82ED1309BA83A2D43D82E43B4CB29EC04A88B9AF0BF27A21DBE892F40146A59F1E683CFFCEEB846621B104CCF7E4C9AE28B7EC24BDD1A4D57D87349E9367F405FFB9F906EBCC07E731AEEB0D4381FCCC27E63122C596A5B1CCDC87CE19F2AB3D8ADE1043FA2C3A2612631754D041D287D0BBD313FAAD6E5167B11FF466CB423DE9CDEA1EC57EA11EF1D6E9EA16BDC27F749AD13B8A2F54372493BF518D49D8DC2416F497FC556A45AA865DC6031DF1DBCA5C5178E0D3AA5FBA0621E12BC0E23E91BC1ACC1389FF6E30BBAFAAE800B51EE33F15DC9D32E12FD832FA8CFBC06D4306D613B718CB757882A01F588FDA3669581297F12A2B5B70D9F3AD24FB82075C9B8228592AF193ADEA6B59669D249C0DC9DF1FE549249C0DB5E8A11E67438CA72EA5EA237177044F621A2B4E4FCE0DF966A3B84BE8671D79121C7216545A74C6216731C512B2D08F3832FA40F2D2638375FE5B8F18F378B495A01BF8AF3B76A712F45384AC0515F17B85CDD50FEE8B85B81045DC98681585FB46617766363B4B2F508C463E9F75BC184BFA22EC4D207C8342F74A5007B8E5173C7044E75E6C4BF1CEBAD396E8802AE44BD20DAC7B6B657D69D605D8815B7147F04EE6F264601CCC35EB14C621DC0EB62AA806F98BD0BCA21AD2F017A45B77501F6BD38D18518551C4BEE68C230775C319B3574E64669641B0971145DCAD25ED17CD71C5B982CB461FF53DC0A87B9F14FB4A6398712F88B2D75F7D8DB7F2611A4E2731DFADA198275FAE21BA218BE496C84FBE55D3D1606ABCB5C2F34EB8EFB1D0FE05EB45167D2F85F5064B071D207C3784D119EAEF8C3444527A6904138F8AAF177497D2C32244D761325AD1A122A454018E1925C59A4057E069557D7E40A25C0C921DC37023805E882E8C924AC228B8C6E4A108B55E527094791D6FA5B394554AD77BE655EBA4FB86523515AB2FA8A12FA2D8FA5BE8C3BEBC8066DBD4591E95AE14DE032F11967713BCCD0EE5DDFD8E6FD7704F3D75DAC98D834C7A5DCDBDB95CB93358779777D3EDACDBCA459D4F1C08B3FF0964934C76610DE165534EC689349DBE954D40BBB4546AD35151154D41354D56BBDE1CC870A9CE58C5955AC9AA61C14CBAB4673B565EB22BEE47E655BC3C89C8CB78D93DF592BEA54446B0FBC1CB3981295539A57A7C5955AAB3BA937FB654D0B14A8D75AA7ECDD3A8AA9D2BBF179525A6F06654BA2BEB33B4F21E14DE85DA65C7D13775F27B4D72AB274B2EFEBD9E747F554784E5DDC5BFC9B303C3419C272EEF9B14F493E8664AA6389CBB29197D549F7B56E825CE6D94CA5DDFBEB3D8B726F2BB4CE196459684E27B1631395927A2E51D28BE599149DE8ACFAC7EE19B52972A5E1828EE01F99581B45A314E8AEB74BAFC8640AD2F2CF810E8B54F44A9BA8FAECA7BB83B995D81A557243CDC8545666095AEBDE0349A832029334EBDD5CA0F17495DB348B933597933E484FCF264E7CEAB651026EFEC5CA5E9EA6BBBBB49463AB9BBF46771944497E9DD59B4DCF5E6D1EE837BF7DEDEBD7F7F7799D3D89D35BED54382DBAAA5348ABD05207261D390D3233F4ED203880617998BB63F5F52C5D46EDF2B1BC32FE1A33F6A796CBF2C8D7E17CA1DAEE679D7DD65D990BAE38EA02CC8A7C8C40204B0D3D560C5C9CC0BBCB8BCEA10BB7C713F0AD6CBB0FE9BD4427EEDFACD719C06FF25723EA5C9FA22BF0201275425AAD3D90B7C04FF389522499DC6899FA48FD7CB26952A51A377A224458E56A36F8A34752A3EFC9E4FE245934C95A84EE7185639C86E896B7CEF2A559DD2B315B49BA46055A22E1D4A362C5997162D1F9E4E537BB84B8C2672A8EE526395004D72E02BC102FF6A052560607BE40AD0C0ABD80D38ECCD66D13A24867495A8033249721DC514C894A9FA94265E90B2A9E539EA14D1BF4D4A798A3A857D9AC4BE2E8DF3C39326852C41BDFE6974915D52879328D33486E053828B2C41830BB08C081EB214750AEFA25B78095C2AD3D4A91C2EB3ABA070224592862C3767E8A2B4863079923A8DAF4717E77E4A7E973A55E3CB140F12343E0EE791023E9533102F7D38A744B3BBC6D8C1D235D0E1E014244976D166031FEA6467689DA125F3350D6D7E03514B339BDD55AD6F64D9D5BA31B1B6BC66CB43DB820F6E09B21C3028D0B985C0805D31DF121DEA7BE8F521425077E43861653CE98F83C1E669F5D1B5D67335CEC93CA5E91AB7EEB8D524BF5B7C4E7CE03A556366935F05DF98DBB06E8797D2A0675958B2C66CCD7B414ED6B2143D0A8C295F95AAD1CB20FEC89F919D5C266AD3A1996A64B8E53367BC4702CBDC5BC194109975C7A30218B3AB7584C3200E236A2C865A6B21D616D4CB074A1AEE3CFBD512119DE77E76FB264EA548D29029F562C620C492355679C2394DA94AD4E89F6C971DD53AF7490025F3B4D6C28A874A89F530CEF3A5024D4CF6A3F0D28F093CC692D569E53721804514DF90AC1159EA349F5C87C5A157926433C7D90D6737C8FC167623BF8BC9C07464EFEFB4331FECAAE376E521DF242345928E4318BE781607A44358246A38F2455C43C38B2FD2FA774FBB0043075D2A746E297495F7AA1B80577E0F543BF4E2D4DD7EF87AB63AA27647CB34073B0E76B61C768AE70C0C50277F54B41DEA70EA3AD451A3E250C7A1CE46A28EE0D11165DCE15C1BA9883CDCDAE35DF3EB6294669B50D43A549EA6C5196B89A7D5FA8E2D94B6858DCCF5B516AB6B0E0F55E8DC423C24AF986D0989C443EAFAA82823D00D309E78E1624DC509D6A90E381C70A8D0B99DC051DF26DA1E354A1AED20835F7BDC93387B412C50C520325C909A8C25ABD3FA70191C4444647999A6833C4114671F85C09E2A5967446473746A811F4FD7D996F52EC8696F99D63F42176A731832B5E9502B2AF6CC4BAF088CCE5286B08496A26BD717A8D2C16342AC3AD9D93067C3C87CFD48DD5310AE4DE27451FD7651BAEC9AE3B65B3410EB21F0732F58138A5124B91040870A234105FCC9C6F68E6D19C6D6CAAFE556EE061EECACC33D0B69B33F0C4CE5B16CB45384A76F6694A21D17D6419F0A9D5B087DE760B90A204BAD81AF24D002F6F85547EE13599DA33E021310506CD5A96E90BB414EE66B0EF2FA05E69683BC24D06290F3AB8E7B90C34FE8A1BE6812AA5335381AE1398302AA98F8D5E7F4D0D6D4CE8E9784AD2F73F696CB2C8DE55A7722C1C17C7F308F9E983700F9BD794B8867551C37C03B08EC0B021D60A9D0B9A580951D9C37C2ACFC087F3BD8E2D475C8A552DF21971A15875CEAB43604B9CEA3EBB0FD9219ACDC66B98C596DDC50E5B6EEC494DC4024F33507E2733F084C4E1415F55B0C476ECD6E46241AFD248532CD8D6B37AE55696DC8B8DEBB88D6295A37368D532709B518E972121D19613B8778B6F6A88C030F153AB7193CCC51C3042E36CD59E79DF66B77D4CFEE2EB9E975F093D44BD7442C4E99E690CB21D74890ABB0CBFBE8E884E9219B8C48FB53369CEAE386307BC76CDC50175372439DCCD71CEA4751984EFC6FB65FBA2809B418E2FCAAE31EDE8F32A69B8172799246A8304DE35497C684A631D1A5E1804185CE2D0406E496FA51C87B3552ED9C524DA3CD592551ED6E10C2DEDB7FE35A283D07C4F4274BD043092FA05D1A3C5D9DDADE7C1E83847C4DA54CD490EA7AFEF6573F24042BD234A97C8341E51B5A32593AAC61ED94AF85553B1734E8AC4DFFD6E6E8D8E84A50929299E1E19018B77F8AF14F324464E968D398AEEE7338A242E716E2C8A10172C0BA2DC082596BBC3B71D6A2FFD00BA4A9F70276CD347B9DBBA91B54AE8627B55A0537D32548AF22A2B79A39EA141F0330A72E98A9123550278E6694DF5A256AD0995383A948EACFA33FF25E35EB67091AEB17D47BB2A79ACFC93E054B2F7ED1A451A669F8A861E087446796697DEFCB983FF66BE3A8B6F3DA9DB5EDCFDA326FD0D7B0B89907D9CAEAB26B8EDB33876C938C1449CE1377D8A0466B73B0C13468AE26D10E21363350CEDD76ED40CC81D83840EC91BF78E48521681F05535168016182BAE376736C1D0D1CD7D13ED3E03F7734D021577F217C5E38F7DAC7EEA1DA6D82F6D8F5C68D565D6C975C80851F86B0AB9AF4B064755A7390A47E9831D2A4D6C8E87FABDACEA2995BAE7298D81326EE7B06BB43A8720B446457EB0610515BF4656175AA1EA5E3039A0E4AD3A3F2EEDAA7CE7194A9FD43FD7E14A6F02B110C95891A3B4A1EF9CC409EA24EE15D80B6C89A34CA340DC776496DC314491A3B4987845B9B25A8D7FF7A74D1AC9F256800E419A1F367432D4B9CC580B1335726EAEC8DD2FBA17A7BA0E8190D4AC5AA44753AAB19A9EC798A062734B63FD1355C93739F94A548D2D0749AC6A12E8D33128ACEF450689C5190675CB6DAF175C664ECAC0567D9D54624BA54891A3A7879E9CF684A58B29E4D7A0A56519CB22D259EA7C1E12AF066E43029D23490304138B19724FE82F0F49B391A7CEDC5C023D8CA9334F4C15A4CF4FE950F2E5198C045F48AF55A0B2B5FA7F7F6D669B41F4409E96BE3191A11127E001DEB491A53B3B8668E2E455AEFF0741DD7FBD20FC07DD2EF2E1275E93C60D179A04FE70D169D37DCF4CD4DDFE8FC16D3B7B368154733A3495C4EA2E5548E57B9BB091D7D0C5E3760C0EEB490F6C3EA543D17D9FCB1AEFC5D0D7A6FB34CED7B2D2B37D068D8D0BD44E6E952A583CBF0749DBD90399BC146863ABD0310DCD0BCD5A93AEB9CC1CD65E011B856A7EAF1C416B299D3BFB145238EA05224A9D3486105C25C1749EA34C26815DC40473021D7941B191A134D5668ECAA45686C8EEF345C3532B4E94DD6175C92559E73519C8B42E6B776514E0CE29E1A548C1C1566FD31FB2A63F30BF666F4B65B99A64B8561C1B174873E0E7DC8FC16E873FDD20876AE5FB6C41B56C53103CDFB8417F6BE960BF63EE17FBDAFE57CBD4F2C92BCAFB53E62B65FE2C04385CE2D058F7C21D27093BC20D21247B8B5BB0113BB4BB2D6AEEB80ED52978714696EA8BBA14EE66B0EF5E6C4B7F56027E6CFFAE35D46A09B216F6B90DA0AA373C35485CE2D1CA6D9BA7AECA15D6E23835CD0686990B9B5BBF3EE5D0C9B90A3D1C4B099C6D3B81838DBB1406673433B8B6FDB1A979057799345E74D7D3A6FB1E8BCA54FE72B2C3A5FD1A1F3911F05D91982291DFC48E6B5A19A529B70645EFF119A35078CC02D2AB30DDD99471D06A6325BF33BF5A8B02E7689F62DC4913717B79097D0F05053B024BBA44C1B3246CEF9DF2A746EB7FF6D21ECA841C9CC17DFD82024173AC4A5E242875CE890988E0B1D12D3B11D3A5480EDF3D2ED49F6032F49A865796E29F596EACA4F015A0D98B11AE216729E8EF374C87CDD7784994ADC7E67804DAFCD63C38A84C6BD53E0AE29125372E397CC371DBF9889B0388A31AA36C6B2909C1BD16E44DFE2114DCDF64DE38949623656205C74B18B2E7690740B21E9E5B50D2C7A796D0642ACFA63461F1772EC10C5210A3D6D41373EEC5F8159FB77D24942ED67270212DD418B9DF8221B7130F606C771B21F85977E4CCC91B064AD8827548566AC91311AB59EC18F67AED3FB38959686D269B3D3662ABF1D481F809517A7A8D93948E17730C56A925E7BC89653EA46D7ED4D039D83A442E7F63A48733F81CCB5BFCDB949A6FD48E31218FB00F3B31BC0584163CD1C3764DD9025F3DB0DD9A76081B6380C476C4EA5FD80E5D51FF7E68A9D555AB74523E96537D289FC96233DF2E636AC7349C760B473297433DE73742169D4A96E5BD68D79555A1B32E6DF8B027FEEDDB41EEB45FD16639C5BB39BB17D9C940D120B1E55B23AADA24AF9785D932295A913AA9BCC627F456FA13632DCA877A39ECCD77E4BE9E62CF2C3D4E039A59C40AB179578553B5AC1850DA35FC4D26495AAEB1ED0B4F0748D20074B27640FC02A7DBC24F1224F53A7F228F6C2D91549A74ED57059A270E1A76BF2EE7E2C59839697B24855A91AE729FC98F2C7CA34752A81BF241FC228921C283B5026F3354139CD273D93A57984EBE474D27ED6C5ACDC0D36D358AA8BA2A7D105F5767899A66123D09622FDDE0896ACE368B282E0EAD421F61ED1092FC2D214490EB71C6E91F9AD716BDF8BD1C39216D0ABA06404625C1A1DF9995EFCF8093954B3242D1AACAB82B4AFED83559E5C87A452D7A91AA3DEF0461CD8E65E1044D7142745AA3AA5190263D6D1E446860626568AC2390159E4B4A158A85E7E1E844F9E2AE6D0D8A13199DF6E11BF46E35CBBEC61724ECF0632F3288D7B33CF2DC98B29B9114CE66B8EE0185CAE17A078A8BBF5B87D8A5369315A25F5BB19A307C7491AFBE420AD53D5293DF783C05B109FBB4AEC1F356CADF64190B920075799A6813D3198F92129569DAAE1F35CCFDFFEEA87848353A46952F90683CA371CA23A44A5F37988BA9724D1CCCF208B82D5A75100A679AC1D1C716CCC6C14A11011E6E6990C4C9E937B0518A9E9245AC733D67DD24A109A9162E128EA8DAA5D4D96CEBD7801585E9C124B39154DA61EEE323F8FFA177C0CAEA1539B7A41B498A2DF80F71D5905C9AF8995413F153E294DD5F0C3E2042D7C5F067F665F1911E9FB1B1F26A094E190FF81A952E4D7AD0BC05F0ADF962068F861316A16BE2BC99BD9478524AC7FD31296D19DCD9E1F82982C52E17E9152FD9D9409E80342D72CFFBE75BDC9EC0A2CBD4C9464E5CDD0D2112C71E4C709B2A2DE859780BCC8CE1DC8FB47FE1CC4D03DBB81367F791715B83B7919EC073E40174997054EBDD0BF04497A1EBD00E13B3B0FEEDDFB959D3B7B81EF25B02A082E77EEBC5A0621FCE32A4D575FDBDD4DB20692BB4B7F1647497499DE9D45CB5D6F1EEDC2AA6FEFDEBFBF0BE6CBDD2499370E6A607E3A1605DAFC6A0F7F1D50DFBFFC04D0FFC6BEE02EF145C88A0F195F1D358D3C97B4181EEF02F865A0619E9F7929F41C42540A644CEEDC79BC869EF14500CB5F7A01FDAA3449BE7E2B1B6BA4A691C66B2989CA87CE29841F79F1ECCA83FECCA9F7EA04848BF4EA9D9DFBF7EE69B3967D4821D5B7EEE9F25ACDEE99D2AAF558E1870AF87AA0CD57E5955AA55ABBA83959E4D0E5D71AEBD1A93C54ABDC61DE6A0774F5E5C65D56E1F067B98E1B0B007BB359B40EC5A3F72DFDC17BE625C97514CF49BAFF61E9BDFA8FBA5FB42436F1022D9851A18DFEB52DFCBE2651153EB35BFFAD522C37D7AD127DF654C6A63E9FD92D3116D4A87CBFA13DF617C77C8570A5AF2EA73767FECCF277F87A7471EEA7F63F6F71D6C5C07C8278E92749164061574FF60E4E419264AB823630C799F95B6EE6D12ACB9618F9AE5C74FE606E35E6BAF0F82D209643825B8E04F9320475A5E056C3C15B6DE0403E52F407703B9DD698CCA5DE31F40B5FDDA26FDB629A34F15390C7CFDAA57B7E05E8A9522BE39151624DBC5A51DBF75ED823648DAB09883FF267566959E3CD2DAD394B6B8CC6F406DEE60231C81E52B30CC30AE8DE6609A27A1BD802103CF7D14734808149EAC5560657F1F68F2919F28D23336AF52541AD775CB0BBDDDAF7F207E0A23CD96C363F7A721D425AC84170F32C87FEA6E87FE2872FB6C502E883B5D28E05B8361B68A88B9FC5E275EC16EBAF4534831163C62EA435547368E4D0E83A39F203F208EF6D822325BA223C52FB9047F27DC821223D1C96382CB18825C74BB437E9B0C46189C312872566585284116F0B9A74B154A632E2D4D66B52CF6CC8CA5647865CF8B3006D16D6A21CA8DD7650836364FB70EDC40B176B2C20ADD562B81BF86EE06FF7C047C631DB86DE8E51BF6111185067CA63E776097FB80C0E222AAEB95D10451444312B52C194C767AB6C562E5B0D6FB14A7F18A2B203AF8617AA78288EB56ED16F70945CD9A669C35A5A88F65C5F2022078FAD77993375B7DCD44D6E925310AE9D99137E050914BFA94DF1B917ACC586439FA60B76737860C3F52DE3AEB60312CC97CA9E8575FD3E63E65A90CDE3B8BA708446158C67EEC73AA0BBED40770E96AB0091DF0E98EB6A49AEBBC9E823300101C6B21BC66E18EB0FE3F7A225B8DDE1994A74D16D4588A4D1C2CDC842E00B68B28D4B1D409D859999B9CB832D6A1BEDFABAD07A87DDF6B07B6FEE90DBA15CBF28E7E04934E01D3C61F0941F057708E510CA219443A8B121D479741DDE22706A731B94DB1473E3CC789C952F7B6CC75043A8616679DC60758375B48375EF225AA7684978BBE2B6BB3A8FB2CDA7401C18383028C0605B50A0A3C166E5545A777BC58C1BBCDBDD2F57BCE3DA1E521C2A3954B21164979F9C08A26D79B261C30E98B861EC86B1F1303E8AC274E27F735BD605BA9AD53FCAFAC860A49D9A129898127063FDB68FF5E2A962F4B0F8960C770BAFAC0957123B0A83575BE204D66F124410E0055DF822D53BCD76192EDF49B63ACECA6793AD12B579B6C0C601D52E56DA5CD49CB344D62DD1D1B1BB095265C9A7E8AEEDBCC5CD21C26D4704EA11EACD05814D7A9FE0197AB931F55EF8E1629ABDBE6C79D17B6FB50A6EA64B905E45769E2C7D0CC0DCD6ED27F0CF99CC696D21F3D91C1B26B688CA66032D481E79AF6C933CD57CC65385E653B0F4E217563EF89330F0433B3EBAF92E8CB5E7578D8F223BBFDE59716B563CF330B7C492776474612F3937DE01C09602800B599312755728BB30150748BD00D2237FF1C80B211C6C0B1E6DD601B8519C56B3157967EBF601874AB71D953EF0C2B9E710A9974D8F0BB0F0C3D00F654AA2CFE11C24A91F665C5AA76D733FD97CADCAAD1039E4B3837CFBDED66CF420511079930181681C1FC8D1A30BDEDF5DFBE23B415BF87B9D9DED88C2148414DD765B489EE4BEFB360CC26F33AF47792B3F77A9B979A216457648F9BEA6DFF8EBD1856D92C767B629DA58D8388BC15C8284CA97E7DB960FBD2EA1A9C72A6457337A903568BEA9CDA80D233AC11F88B725EB611744CF24987ABFABA0CD369BF30A419B6DBAA03B8ECF3A6239BB26C93E523EB9BCF4675D1046E6FB295845716AEA803C5905DECCFA773A4E10E6ED2589BF080D00F8C95E0C3CDBC3C93C647CFFCA079728D4E1227A257C1845ADABF6D669B41F4409EF3B2AC572F801F40827692C9BE7B6D818CC69E38A666B1FE4D9EAD20FC07DDBDA97937DD00DD937AC639A9B13BB3931388B567134DBA299B1D94291ADB935EEE219F9EF86FBB4E8490BB34D56E335BCDC1A233596758A92EF9851B316C3565CA76D83B30310DC58E36B0E895D06DEC2E0CB217E6CC9D691C144D4AC134D2135EB36388C56C10D740A137AC9BDD5F75D298400B799B221BA8900C034A84CD617C6849C87E13C8C5C9D4EB626324BE86428F5FA08CCFADE4C77F7509DAA3523E8F0C3E14702AE5F3AE0C809BC7FDFA4F20393CA6F1854B6BFC9E280C1014376E203F6C8B69CE1A6573B2DDE2E4C995FB53D5C48C3051CBA716A304E89B9E4760C55AB83CC66C09D1B6FB77DBC656BCFB187F67FB764B0B968376500DAFE6837BD28171740D779005D477131F6A78CC68B6E2E66A022FB663764DFEA86EC576C93FDC88F82ECF0C5D44A94664D2E95C505B6D811B2144A5A33298FDB6A11EA53939F79F273CFEDA967CC4F3D797497711B71E4C94C95BE279E82A5A473DE1A2A64CE4D3DDCD4A39A7AB8A0221710C4A7E002825C401087AC0B081A63405081EACF4B1727D90FBC243142919AD65380963266C6149D0372DB1D10B69A6E8917E22E3B72236F73461E06EA6EFCB9F1E7C65FCF536F176D3BAA39B38BB67530B25930F2F2DAE1474EC005DD3A7C70F880BBF9E80682FD2B30DB9677C1BB8AD2E9262E4445B7D50825FB5178E9C746738F82440B86B4AC925339A7723DAA5C01740760E5C5E9127EE3394851C76E87F25998D0385FC0F902D91099FB0924BC2D570A5B18197E76AD92309CC62DEAB911A63EC29E82055A3BDA8E01D6DD7382868B7C6E19DE8D586B2336F2E65B64147300722FF5B851BB9DA3F6BD28F0E7DECD968CD6E3A494A768E5C2D71F140589F2AD302BDB4B072099C5FE8AB511E6B6ABDCC06D31703FF06ECE223488B663E4EEC34AE897ED35C1DC7E7741B9A3C37F0760953E5EDAA6FA28F6C2D9957DBA2751B8F0D3B5FD3BC74FBCB413BA733FD6F4C554A806FED2FE63030EE36F3BC61753AAC9E9644B50BE0B1C3E8D2E64EF32B738189FEDB7491F53D03F93709C34C3A1DA9EA8EA62FF0C1D8D313ADBE410CB21568958FB5E8C1E11DC12E082D23C7E621D633CE31BCF208927D7A144FF5AB0D6C1552390D5BD2088AE4DC49D2154363F83596BA9D1C21EA5EC79E0BFDB5B76106A134273ADDA1220758BE16EE88D76E8C191B35E80E209E42D197007C7491AFB9211D7C29C3FF76171C9D3F52DC8AAE0C35BFA645516EBEEB7801D08391766F7A79DC560E6875244D4F771AEE76F7FF543CB233023FA0DCB441DEEDE7ADC8D0290479FB5015D547B4A232FB36CDE0AA3B41CA7AB66DA8F75AC755522CA7DF8185C27FB5EEA05D102FD6CD3911809D5FE44555AF526D956FB4EAD58B0DEA5870928583C6CF77C7A4D40B53F618D56DD49B4D4BE374B064C3B732F49A2999F39525894DA347F07347B9AB9C9C66138BF834658F3DDE609082EEFE609A7EB20F557813F838DBDB373EFEEDDFB94284D1AA8AE94CE2F5144E0B70088471FBDA51B42DFC9839D407F3868B2FD9517E03C1385141D44D485153932E700ACD005AD614ACAA5D256FDE62ADD624598D03399F40F77B10F2BFEDE05D89DDE987FEDFBA4BC0F9F8407200029B893AFA6676BE2336F4EAB3754CF39AFEDC2E4E0AD97491BAD252C53CA69A90C1D1D444332E4F6CA1BCC68EBDFE24B497021B38C38953CA193AFADF1150CBF376DEF39EDD411FE837DEF133F7C316538289ADFA823442839A4DACF133BD113E5AF67414B323154DA8285075593233F0089053D51C083AC298A5091BAF15F3C9763133EF9F1D25B807E3E79D61445A848DDF84F9ECB31F64FFE01B8C0E780D86FFEF76FD6697C40324B5B27584405142DE905C1764F1AA2D31CE4B08C841E141E4A453905A9770CDB7B257016CB124D7FB14AEDCD951846A7383D340E75CA981B831E41BD9EF829B9ACD3C01A948F47AC954053A76F0ECA543C8F50270AF686344565F78C79B9A285466ED8C285AE9E0EBA7A01992D5EAFDB189B54B34C2A5095BCF916099366F406E9BD6809B21591FEAD51D974835C9DB80D76A89246C308A13F065587BD7917CAD011A0E40C532A8492B64581A02C9BA43EE757007A319BA54119CF941215A9DBA247B9389BA24A4574D7F43CBA2603E1EA6F9765E29F2D4FE8456F88F0B3ACF92AAD139DA1BBA21B6529A550690AF134988EEC5D44EB14D9D7E1A6D4240B0DB274E636600925D526CCB02BA6A74CAE6D7D51556D6113ED483F5A7D305B4AA2D4D8E0EBBB45C4353AEC504408B163B0EB4F89D5687CCC46BA96726035A9C82101554B4A82B7D0837E90C2AA3439703811AE23E65E89BA32F4AC087D7919BA0A37A8A781317B748C0203B8DCF70511191B3CB245E69601452E954A9B58A5C154E6300128F4614A47EED6DF13E5E19F30FB5B4B238A56482A795A27DF9F92A79BEF5E0AA1D2D461329C6389C53DE71BD542E7B2ABF8819A0B5213B62D7A00134971DE31B87389A9C8A6ECD30CA54FBDEED368AAD2A0FB344D98E97991634074E97361431F5A065DD278E42F1E796108E20134A26ABB410F4BDD067DA8C5D150874117CD3FF0C2B937D0DC246BBBA95E79CA36CC4272513665EA81AEE71A001450B30D5279C236404126C9261885ECD3E70B65339A69DD8FA5F0C1A9A5CA0E3FBAF257B0F0B537655512F19A3F3A3B35FFDE1DCD266A1E290ECAE48DD797421095D6CA373187D697FCE063E747A187FAFA7DC58C6A7EFD414346A9AFDFD3B1D7A174A0BF23B09A5A30F041D89ADB936861C170287FFE13620D81C8D9123340BDFFCE6970249600E940EFC660084DE8DF24A86AC248AC42AD09BD1B8621F46108F3A0AA1123B010D72FFB310DD72F292A2869E38D011462F4562057CA64B2BE405F20FBDE477E90823821F7DAB18FD6ACD4F8786496B63214AD531A51A577A21604DB3D294829934A7388AF41D1A0B816B84F4FA16892D2852A7DE3BD8452928DF0110A6655979B34BF5E878B4F0DC679FC74BE14A5F3ADEDA9D6262D4C3515AC7FA019504106009D4D5AB46269468F5394E1F5A3DFA98ABE868C60BAD2D491E77E14642592FD00EA0B3F5A3BAF45166FC46F738A6CA01A714419AD5AB1F91DA59AE5AF64CCF4940DAF2454B946C16D503C6E778D49FD98EC8E47FB9416EF35DD705D6D62ADDBD105B6D2A9DE98657E4A698672AD075496C11CEC8DD902E068C9506EF680BA32A0B3BD41DB0305DF2FAF07B4412FAF793451CE96591D28D2E8CD4DC12A6CC98BD325A43907A9E707FD6209870996874D97D97044E1C9B501A052B03EF793B9977A83A84CD1364B53AAACED5090529CCDD18BA76001D3FAF25D1B8DB2F4A1CCD9686FB529CBE8DDD492DDC89B6710C1E2DBFC1B2A6A46C104936E95D7E5628AC657B3A426A5582A4DE6DC0DAD2AF52BB6D3B448594AD7E76025D657CD92DBA80AF61A399B6C95DBA5BA20F6FBD315DE0BEC9C46EB5AE3D1987CCD0FD39B225D457F9A4F270BBF7A5966A3354BF054F438D58C7A317D109D43B24C73F72A994EA2753CE34FA4D1BF8DAF9C27F4124581FE65B8DA7872270A9689D88336618228D9B688F7E267EF4A73EEC50BC0778595E7475BA638FDCDA7345587FB006C4FCA43BE7903A4B8D3E2D988EE9F3461BEC4D4C8EBEC19A561DECA517EC049FA88EE405A2601AAA15FDD1B5EAFC6AC50C3691276F3CDA1025869DF55D3F90D4A8C4BDEF09CAE2E691BE44E2ED5CBE164EF520FA158127C52B8AB6F5B5569B43AD497F21C662F8CC33A29AC01E2729F2D9A83233F4ED203C8F8056B0B14D59A80145B39DDB97358BD56DE589B9ECCAEC0D27B67677E8166F2F96BE7282761AC0134C9966E3445B8CC60912E660F52E2F91493229D27B308A31C39D9C94D928265B6814FD1C6F2580D54D92A3D53DD9AC7E89C2A8FDD3F45B68230B95B40B59027B388E7390A54F34BC19994F32C1E7594ABD644F1CC2CB38D228FD74891ADD048F1B029B391228FD74896ADD64A65E099ED54B9BC96AAC73F656D91377552CD9105582D36CB28355A5FCBC46AB1CEE5345716501A9DA7205CB3C7669EC3199928534996EAB92F962855264792225FDED03958AE022F65355367B11A2973E54DD40F56514DD459AC26CA5CB526D08346CC0650068FFCDE5C8D78F1D00D937E91C76B22CB56F810D90DE8F447C892991F00E6C8C956EFAD5094AB1C16F122534E9F7EEE816A882EC26A912CA5D1B4A84D4963CAA8B21F0511CB8368660B70252B216FEE087A5013FF9BAC0F5667B19A297315B00BBFC090C62F3C9789617501BDC68A2BD7452D164524CD16A5242D67FE3ED55896CAA28F26382A34F37BC35974F31C0E6D354F009F83B39A108EA03A5FDE10760F29D50E96C76AA6CA56183BF9F596F4A0C9D399A30565C929E7D7285084F364165D94A346B63CE3C1245E66F29A280EC96B34C4F6F0897C71734A9E7E796504B32994C16BE3FAA51AF1EAF601660B552EAF99AA80A429F2F604AA35B200ABC1661935F9AA405CA67C552E4FBEA280565B425524CA48DA55554CDE413EDA31E11464BA2ACCB22A4E91F08C979CA7467125CEB01A7A7A211BCD7431A52FA63CB6B1986E51FB285FD2F04B85A97751F4D4F383FD2B3063B9607411C107A84AA9092B6A97C8E709ABD122370E9A27335D52203A5958999F2ADA96C7465540D07A5146B9D1321C90D766992F68322FA2DE621528C86DB32A216AB528A432DF0BFCB977C39CED1539ECB95E96A9E215DD9C457EC85AE8ACB3D8BE519EABDC7559341EAFD7B24C418765F9CAED54415482E6AA32E2568B62EAE6818E13933351965462252FACB0140C2ED70B50BE8BC758136EE6331787F1226A8BCFDCD56D3C93B710ADBACA4DEDF18A5610C5EBBA8D42CA1322EE740ECF174F8B98533C6C3FA3B90F51DC119EEF3D60A5B03D09BC08B9C54286DA57929509D4A64CB3467E6BB8A456633B06965210AD08AE39BDE109D62C6053ACA6A26675CA2463B1B21DF8C67118866874210BCC324234B03A4C2D6F275EF6366B4E8F2D5A5DC09041AA06BECA56D5CA13AD8896AD8E0865C34AD816AEB1BF5355632FD8B4132FDBA1118A8795B02D5E6367A9AA56A41A8BD7DCA49936376C685945C5F962B0B7933259C82C4967B04808EAB7FCDEF45B669CCF2E79F48C002462ABB6C0A42A7554A2976FAB4804673EC1427E7B7257AFFCF075FAE0A263DC084C2BA3944DFBDAA2ABDA895AEC2E8AF59B59AE1BF5A6774B4BB9AB6463B1CBCD4891665365BA536B725335AB5C275A11776F2E13162BD1ADA8F5E66E25E81E15D8D452CC6C83562669B350B7C236369B2B798B5463918BADDDFC816E86B48D7C3EA3F87E75C6639E20108DD88ACE2A5569C66291FBC7A20FCA2DDBDD77E5ED936744E84C7BDD31AD680AFAA12AD491006C4A6C121684C7DFA1CFA7E8D59A0BDD09FCC27C11185BE899108D74411760E5A879BE80866157F0463C55C6E6A86FD1556662D2AFCF8B4596BC56DFD5776F44439044D8110EFADD82BFB0CEE805EE03EC0DA6B195B78CCFEC6F819044904459274FB32112E33571B670B267C73B9864D2111CA5FCF6B09DF95AB6B807BAF6C07B165B60DB65CF405BB5EA7D884D3F62CC905AF2D2B155A1A9D8A1AC36966A3EC5A41EE965CD2FC52FF95AC5ED462053DE59798AB1A8682B5AF4611BF9DD7D533C9E2AAB9827D8110F7F6C952722F741567D36A91A948B655138FCA5078E6CDCC7202C88D68C10AAEA95C93645C42E95120BCABB7DCA70756B1071F97B49BCA2B67794FA15BBBA2E592831FB52656BEA7C42985322C7B2B8AA7A2DBA14D89A6A0F21BA8A8E8BEFBAB5AAE67D7441F96C1B4766E6AB6E16D4BB8EBFADEAA0246391842F943164547FD1AC21023BF4361385CC927404112B5CF5062F08B8DD572EE278144638A3A4EDF14D840F572257E9364556F04A1825AD0AC0ABCFB165446E079DA1AC05DDBB31E3E80A31EE0B6A7481FD43770915C7AED22B923762F0755361587DBE92CA29B2311DD708B6D7EB3EC14B27A24E649C06607765A3E0B83B54E681B30BF708DD2CFF8C2ED04DC7686278C72EFBC8BA4503CF7B71E707EE9EF2D105717F309F66E86A00D58773480A28C75878CE8112A97A28D5B3AD21924335388ED3656C7515F31E7D7E0FC9AFDDB7D731C4E91EBC3FAA2C5BDD405F1BCFEF03C915F386B0CA3C6584CB5EE658139DB8275D20B8E846751B42306910879F1A54AA3C5B9DC1BE099CDF250A3787B384C28E25E1F2300E1C716A13878D082255AEFD6EE15F77ADD2498A97658B856E1E71628B5E96195567A276C97B9C19BDC62AC61703BF7C2BE33C4F10084E9F98AAEA593B8CD290A1B8344F262AEB6E3D0BD6A40F71F9F7E54AA2BF0597EB36846811C3CDABCF3C43213A9166A53BF83AA0780B2C258EFE3990FE3A80731FA93858837B73694310ED800B4E6D464C139E63BD0BF80AA072C72629846E389665A1D1559C884875DB6395F770373F815924C03FD328F616E0349A8320C9521FEE3E5DC3DA4B90FF7500901F59917808698620BB09B4265A964186A8BCE492E0A82C526663D71B2217091D03B8F46629CC9E01E8B986504F9E7BC11A750F04C1F971F8649DAED6291419826270837706BA2C53D4FEC35D8AE7874F56D9B2930D11209B3E14013C091FADFD605EF17DE405E447E39140B770BE0B607AFE2DA1FF9182C54D45E971142A122ABAAFBA3CB4BA03EE4938F13E027CDEE47DD8ECB18707BEB788BD6552D0A8EBC33FA1FACD97AF7EF5FF039E36D80202530500, '5.0.0.net45')
