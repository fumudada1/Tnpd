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
    [Poster] [nvarchar](20),
    [initOrg] [nvarchar](20),
    [InitDate] [datetime],
    [Updater] [nvarchar](20),
    [UpdateOrg] [nvarchar](20),
    [UpdateDate] [datetime],
    CONSTRAINT [PK_dbo.Cases] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_WebSiteId] ON [dbo].[Cases]([WebSiteId])
CREATE TABLE [dbo].[CasePoprocs] (
    [Id] [int] NOT NULL IDENTITY,
    [CaseId] [int],
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
    [CaseId] [int],
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
ALTER TABLE [dbo].[CasePoprocs] ADD CONSTRAINT [FK_dbo.CasePoprocs_dbo.Cases_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[Cases] ([Id])
ALTER TABLE [dbo].[CasePoprocs] ADD CONSTRAINT [FK_dbo.CasePoprocs_dbo.Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id])
ALTER TABLE [dbo].[CasePoprocs] ADD CONSTRAINT [FK_dbo.CasePoprocs_dbo.Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([Id])
ALTER TABLE [dbo].[CasePoprocLogs] ADD CONSTRAINT [FK_dbo.CasePoprocLogs_dbo.Cases_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[Cases] ([Id])
ALTER TABLE [dbo].[CasePoprocLogs] ADD CONSTRAINT [FK_dbo.CasePoprocLogs_dbo.Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id])
ALTER TABLE [dbo].[CasePoprocLogs] ADD CONSTRAINT [FK_dbo.CasePoprocLogs_dbo.Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Members] ([Id])
ALTER TABLE [dbo].[Casewqs] ADD CONSTRAINT [FK_dbo.Casewqs_dbo.Cases_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[Cases] ([Id])
ALTER TABLE [dbo].[CaseTraffics] ADD CONSTRAINT [FK_dbo.CaseTraffics_dbo.Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([Id])
ALTER TABLE [dbo].[CaseTrafficPoprocs] ADD CONSTRAINT [FK_dbo.CaseTrafficPoprocs_dbo.CaseTraffics_CaseId] FOREIGN KEY ([CaseId]) REFERENCES [dbo].[CaseTraffics] ([Id])
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
INSERT INTO [__MigrationHistory] ([MigrationId], [Model], [ProductVersion]) VALUES ('201901100703246_init', 0x1F8B0800000000000400ED7D5B8F24C975DEBB01FF87463FD902343D33BB4B6A8919093D7DD9ED55F74C6F57CFECF2A9915D155D9D9EACCC9ACCACED693E2D6C48224D18D283481902615982BC926889F25236695136F96776F6F2E4BFE08CBC46469C888CC888BC5475008BD9EAB89C881379E23B276EE7FCBF7FF9CDA3DF7BBDF0B63E4161E406FEE3ED07F7EE6F6F217F1ACC5C7FFE787B155FFDF6EF6CFFDEEFFEEB7FF5E860B678BDF5A228F7162E97D4F4A3C7DBD771BCFCCECE4E34BD460B27BAB770A761100557F1BD69B0D87166C1CEC3FBF7DFDD79F060072524B6135A5B5B8FCE567EEC2E50FA47F2E75EE04FD1325E39DE4930435E94A727399394EAD6536781A2A533458FB7CFFDE52C2BB5BDB5EBB94ED28309F2AEB6B7966F7FE77984267118F8F3C9D2895DC73BBF5DA224FFCAF12294F7F73BCBB765BB7CFF21EEF28EE3FB419C900BFC562C6F97CC24EC1C246CC7B7B85B294B8FB79FFB6E4C9648CAFC3EBAAD252449A761B044617C7B86AEF27A47B3EDAD9D7ABD1DBA62598DA8839B4E7EF9F15B0FB7B79EAE3CCFB9F4503942C9104EE22044EF211F854E8C66A74E1CA3D0C77551DA75A655AA8D53274C0A0A5A8AC3156A2232595DFE3B348D0B1AC9274DA4717BEBD07D8D66C7C89FC7D7658F4F9CD745CA83FB894C26E399086FD90EC3A1B8E1549E149B7D47D4AA0CB7C76E143F5D2D9ABE4DC3B80751F2A114BBFE50B7EB6E22BDCFC279DFCD1E25CDEE27E259B48B7F9F2770A24AE7F972E60C306A59B3038C5BD670AB917BEA7CE2CE531004277C0A635B67C84B8B44D7EE3243E57B38E3822C7318068BB3C0CBA18FC8BA9804AB708A7B16C0F9E74E3847B17CCF4ED0E232D15760B7B2BC8B935BBA53B58CB2C9A24BF5DCA2C364871EED54102F04FE8CD42641FFEE741A246ADD247CCA419F1345374138536C38F9A9399D8A86278E675657C9348EFFED7DA8F74CB72AC3E9F9C171DF4D9E0497AED73BA3CFCFD419D5E6142D82DE274E82313342ED6240CE927082B2501E2C1CD75356A5DA73E1E4F6D49DF62D221F0497E76E3C806826FAAE598734C0250A176E14A5EBA89EA57C77FF04459133571D37033AC2DAE5D62E1FC22E2F4CDBF6C62F6D8FC3A6B16C7F300DD816C73917A5B15EF5864C672CF15AA696218E296D92193ED8E6496B7CFFD67D7D801F62E3C68852B4FAC1EA8741F4836077440A9169F500C2752B449EDC267362711CCC2D2C8B4DDFEE10461B1BDB4D7585DDB3D8394A568CAFAD88E86F254DDC18ED25B57B6FF8FC1A296F27E92FC9D2565B6C64E9B7BCE7BC1CA6D141B89DA0F013773A58BB83F06CCFF2AC4D386E9B505AC53E4537D14669D7A4B4EA56B701DDDA4AA71BD89EDE0D6377AABC51AC8F802F5C2C375AF837899DD008261CF83313648EA2BDC0BF72C312D6D345D2932059F0387EAB238B8FD0E55ED2997910DEEA6E203CBBF1136AD884B35B1156ED8C56ED88B622B0A63976FD97F06644917B912A246237A296C16C10D773A11DE2A62E1DBA1E8A387BD6653ED8AB2A07EC1691DDA65F470B672EEA17CE4760BFAA1CB05F44769B7E2588E678C19CDFB1BC40DA02623A57CF053B4815D1DAF62FE463A34C9C0E8C0DA9BB28E84657F9E04FF13C543DC6D73F192EC5AC52EC59522BBD6E64F967D038B02ADDAAF4C154BA8E36A70F17605DDF1AF8530D6C91DFC032530CFD72E277D8E6AEDD286EDD5BA8B650BDD1502DB5C681C01A5802B546EB745D62D1DAA2B5456B8BD616AD35777E20B40636865AA375BE49B351783DC8D1911C5CC93DF2881D5DC46B3E6418F57198110562FA18C86A11AB4506D12219B4A4300DE992249BDE6EAF36F72BC52228C66CDF8BCAAA1E365497FF9A4E1A8892F061435900548B6C29D5570F396A3676B42C077733CF1676B228A3DAC5E234C6C4A18DA87FD4B94E2BFBA22E459B64621C3BFE7C453E50C3E35F24B6522E56D55A556B55EDE0AAB6F164DC80B2A561574631B7C55FAC63D2ABB51B04BE77EEE27D32B71284BE549EDAFA2D7FBCF0F603D567F3062EC1075E10B6B811AECFF0F365BAB9A27EB741FF5AC6818FCB1A53A146F47A3ED50E54DF88EA7F880415AE7B6FD4BC5967E4D1E9EA1293D97FDAFF47B0669835C38630C304EE1008A306F089C0E642E6165D44F7FDEB09F257D6C232A07D95D5EEDBBA4DBE70BC95AA99A1DDA87D7766C179DCE0ACB2C62CDE2E6D10009A38827BEE571446FE804DBFDDCC93C42046FB1A3F9D33BCE4B31AC16A8461CC75E1115FA522E0133E281FB2DAD9425A76FB395A2C3D3C0C1BA4B7063B451A70DFEC094AA48C64DAC2A885D1D1C2A8343CBD1F2C907DF168089E926FE36458A0B51B3E324F0739E8F68EB843A0BC912D13C3F636B193A879D1D2BA85B0CA6B0D9457DBBB73852E832ECED1798CE5CF14D0B2FA31B5DD9955AAE66C7EAB7E46AD7EACD6B05A637DB5C6EE8CA733AA1C506310D9DAFA22737C6A55865519566558956155C6B855468AD63CAD51CB041547BD84DE094370E35BB561205689BD2F63016BDC80258D092FDC84C866F947C130A76B4D5868B1D062A145D9164A2D0CC810CA61E6222B50D940643A63FED432B52C9FDDCB6015E34DDB0D7CFB3B987B913BEFD0C3C2AC85D9D12D3969A883569EBC320C02730BAABA892809C1AF98CBEC8BD2B108D0DB2293710FC296D072EE5D92DB28353114D41AF24235E08D3EF5C8EA06C250C54EBCAA62928EE2F5ADD57756DF0DA2EF44DEA614550757C331CAA5EDF3A3CC2D831784567BACB18F0B0B981630C70D98D2987418F8F1C4FDDE46EDF10EB63FFB241D492D5438D12731D12761B1C962D3F0D8849739893D77E45F059B044FA74E9814D45CF1369C6375B7BF6A0026CF51FFC1B830A239DE20D6E2EE6C16A228EA9DE5F39BD9BBDFFEB86F54485BFD6EDFAD0EF5AEDE8CE3B2410E69EC331E6B2DAC81B520DAFAC934696E26801B408409714195AE3682B885980D217E49D5330FEE6138D9047D204EE709FBC73D1817758B20001FC6680E287D38D33CF42D7A7E78C48FAE4B17BB208D4C9805A69C880BB6B0D669134D7993ECE0C10E9D8841BDC321ABACD6B65A7B10AD5D43DCAE205AA019F978DE0AA20FA2CDDA3C1D643132DC232F7F96FCE5BC4C9ABBC8FCB3F67C7F6077B9F46E2F1628BE0E543DF0E92F5F9F22341BC2677AF2E7547D4B447FB04F670492F5D6AAFA7E977E9B87F81BF5DCE689E3F6CFE8195A38E1CBDE25F899EFB97EFFFB4D866FFE9CA0852AD68FC217A5DDBBB256F09A5BC169146BCE26456252E2EC0B6C5A12062F91CC6C419079AA5B27C2184209C1E2F2D30115A58DCA82FA54CBD7DA09C919DC28537B28A337194BBBF16121DF427E53C366213F85F376704F6F6740AAA02DACDA676F8636316C546D833ACA42BD85FAF585FA8628D884654C96044DEBAA00A4058052AA67BE6287F8441BA0437C285FD4D16E025ED747827ED2C8E6360C65AB678C0782E59CEC124AD0AD03ED10D74FDCF913C74F74EA46A9FABBE61E6E7D5DB5C9DED3B41ADB6AEC21343657BB94C009291726930171B684DE3342C79F3916C2477511E812CD5DDF4FBBDD37133314C5AE9FDFC1EBBBF1A1EE5D1B3E07B3E749567FADB9FE6ABA55956A0DDE552A26930D69C694D0D2617BCE66DD9AC2FC64C843205295A80822B8E2D17E17686E86D3F756AEEA5D25FD75CF703E6A023F4EC6A6FFCB59CEC2ECEA528ADB44326604146339CE925A49F2C1C2F49D20B9178607AA4E79F4E5F383E0B2F7368F4E7B6FD2CCB1C26988660DEA4FF2FAE6005736138BC1F8DC946977395546A1B775593561DE4DB24A3D8FD6C120AD9E2A6BC607833D6D367079B9D5D36603A33C20CFA743319D3AFC1F40B13DBBBA72A783B48C6DCD33B40CC218B4ADC92CD505FFD273A6FDCBCE518415D66E14B973BFC60E67F342EAF3EC86C8190273EC3687DDE618D5363DC60468879E4C6736366A99AA47BEA7C1320CA6F0A92F269CE55FA4BB1D5487882CE6E22C9DAF7AA137EF9677CCBBD45B3590141174AFC815F4B02CA27EEB384237AFF8FDBB7905762C4F067B54E4695D34AE58DBB40D2ACD359BF93D2ED242D75A026A5FB75B5CA250F7A61C7B24918A52F6D4ADC8553D5F494D856A6CF41450466D90473F791471135CEC23EF76101E6649C3579E333765B961464C0DCAF3E595EBA107BD5BB4B8B9FE5B8D93E61EF6DEAA1F2CBDDBC4D88E944F5AF5656FD9EAC5AC013B3FB52448D86F79AD27A533595D1A2065971E76E931C0D223B346F52C7D68F501AD0464FBE4A41A3DDB0517F78C2C09F6AF2A20EA2551AA5D5F335B4BAEB74559417FB322CD3DCECB691F5A978B1EBB3818A515BE3B357E114ABED941AC52AB0FAD3E1CAB3E94DC5DE2A337B301655C31E2169A7463BD4C4377FBD190554B2225499792EABA295579F3CAEAC81A890F1FE8557FA857FD2DADEA035C35B19ACD6AB6F169B68673090861E9338BD6907A1E3AF8307ED370D5DE9CB53767D7FFE6ACE1AB5FF6B6EEA6DDD61DEAE2D800A69B81EDA0A14EB4B276FB3F5DCADA7DABF7763F7183CCCEB9307223BB2217ABDFBFD53FAF3274AFBCE2A2CD45417D476755FB53A78D2B2363CDA7EC5F386DAE189AEE441838EA0A567B3518A385F2F8BF33C67BA276356D57D383ACA625B662F375AD6023962D01AEB48162A6EF74E64D080E7CD912E06D45A05827173D6BEDF076E4C142CDDDEEECF667DECAAB1B516FF35C51378B22DA57426B6C6FDA068C899BA1F62EA7BDCBA94CCADEE5846681BDCB69EF728EF72E67AE095F14CBB368CF4BCC2D4DC4ADA89D21BC593D3540D3AE78EC8A6790158FF0FCB0D5FA41B0D6E9F8FA68BD95C6059ADC65526EE14E6ECC40ADC1B766F825A5D910DC9E113142A1A9242F4C2D113B5461098EE81AED9922405D99B55A5D3906892A4A6C92F5B40EEB611DB949CB46EB18DE9A06D6340029B5470912EA2C5658ACB05861B182B7076DDF208D75F7D8BE41B210692172643B2D6A079F8D8BC62E1F26310D49EEBA483C531295EF7EEFA5E9D192B0B00A3FA69E309587C856D3DA974C563F5AFDB889FA51EE8E8D007DE96B383A7B21278EEBED5DA3E9CB4D43DC415EFE0CF4A0430601E408457B817FE586E50E8E76A4979C5E8BDEA94AF23E5A3A61BC4824648662FC193648A08D2C93AD52B44A7110A5D8B870E1CC60EEDA45A63CA340A52A9950A933374AC670A322C619C11F37DA5DC581FC230B7B5860416DFD412D4783262C838AF1200C2C6B02B9CED01C6FA46F10700D17AD58FF6CC41EA65A7C5C777CE4EEBBD71087B3E1CE2BC3C345B6A011500C9CD9A6D973D948E93EDA190A5B2DA6594C1B04D372FB48846739585C14455934AB97E06219554C0BC9DE0F3C77E6DC6E12821D4505537953F92252194C72327B492FE64178AB28A6FA5739F651340DDD658B1B2CF61A8905D21103A9343A7DE4DC9E06AE1F6F123CED2565F0AFDE4FA032753148D343B9DBDA47CBF8E9A2F7669F848E3FBD1EA0E1E3C09FBBF16A80509CC789D53348C3333734BDD29069D6731703843BB65AD96AE501B4F26A41E864D2A9E55174E839F3A864E339CEF26E1336492D5AEF6DB6299D13FBEA4F7FB9BDF5C2F156C9EFFB0C63B5A26FFEDB3F95451FB03DCFFA28E877B64C32D4EF377FFCD9373FFEE1D79FFDF8AB9FFE992C035FFEE4EFDFFCE47F24D5DEFCE82FB4383976FCF9CA992343BC7CF1BFFFE1CB1FFFD1573FF8BE2C235FFFF073AA421B2E6A477BDA4C7CF99FFF515A90FEE433AD8E571E91B57BFDCD673FFEE647BFFCE2377FF9D5CF3E97EEFEF7FFF0CDE79F26D5BEFEFE1FBDF9EBBFF9E6CFFF23FE8FA1F2A0810ABFE6C30631FEE95F7DFDFD1F7CFDF39F7EF5E91F5235DF12D7FCEA173FFFFA97FFF8E6BF7EFEE55F56A2F376433FFFF83F7CFD379FE2A9F3F9A75FFEF9DF7EF9834FDF7CFEF75FFEDD5F7CF3673F2949BCD340E2EF3E7BF3CFFFE7CDCF7EF0E60FFEF6EB7FF8ECABBFFE159E86FFF25FCAFADF6A290464D06B7D24FCD12FBEF8D50FBFF8D5AFA467E15FFDF7373FFB27B242C3274F86EC8BDFFC7BB2C2C3968C9FD69D2FE94FDE9FFC3491EA2FFFE7AFA5E7C0AFFFE0EB5F73673153FA973F274B37C8F757FFEB4F48F96C90E96F3EFD34C1426969FEFC476FFEE517D282FBE39F7DF17FFFD3977FFACFE4C87CAB412A7EFD19D9FB6FCB7CE1DD280AA66EBA6D491C515E9C3A61B2A24D4F5EEA0D1EF8B3AD6C47922A57ED58563738B2939B939517BB4BCF9D26F6C5E3EDDF6278E0932C1DCA8949DEBF778F95823374853021D7F1F6023F8A43C7F56376E7C0F5A7EED2F1C41DA0AA496E39E0212F1BA07392D529B6A5FC583C9A322D67B5E0F6CB66A8CD90A6D179B4430886585EF28B0627B74269A9958264A5B802212F2D759292B2F2801ED247CFFC7DE4A1186D650FF752949B3A33D6D44EE6CFCC808C81DDEE41C2C02F20D36E71676110E9C27DBFC8BA4EBFC5AF24812C04C916CE5791AC1A3D40B0A4645581C9A7E8263A76FD9717F80797CB5A2988CDA2800AAB75A200AF6997463789C06EF73089C06F20D36E527EB03984FB7AE87A286A96AFAA184FC0D212AA124690951431733A1D6EBF275961C7731D84E568E1CC51B3B054C578C2929650151682EC20C2C2B6DF93B0B0E3397661F9085DE2EEEE39B1E305F30BE2375F7204752031AA1797FAF2726D71648BD3102BB4ADE44B82F91E844D6258647A919029EE470C0A57051F2728768E92F65E0B518B29CD032F694968A20F5A90455747695A7179E80909B9DF48A6FDB4D2808838716384FF685A12B34539F8579452914480F8DAAC90F97DEF071B395F65F46B6572E6E46C4821615EB6231C2CA803E227906DB3261DA72F3DA31935CE922A1657198548E1DF48BC22A817ED48A072E2926B83F6BB32C9D8EF86B13B4DF7829A2C0BA83007CEF3728A68CE525F2FB342C4423FB0CEFD3EA3372ADE0F1628DD756A4275BA202481451915F963E80E05E6BC8EF42041BCB1558071FCC7A042B43B9312A1AA184F807667AAE243D054139EE1A10BEE7E4F22C77E8B7512B8F36B94D8D13232572BC913BBB490AAE4D529AFA3F0811CF4247FE0775917117CE17A1EDE533D0F6EE8F7EB958C908520C1CBF355C4AE461290B8B43FA31335A8D73D481934FE32CDE2F28349D6EE65B08AB13920BBE0E65580248E2EAB227ADC768632D99A3AD48384358DFD3AACC44B1E2E0AA1689480A2A450C45AC956491910AA26E1ED40B2E8EEF42952F420CBB43DF8A909BEBD895FFAFB57417EE92C4FE10A15B706245C446115F1E2B7018899A00D7312D6D8A31E24AD71E465FA30F01D419207A12D4617EC48BA54ECB26EA4A9671B8B37AEA3B7B3888E1F1EE1BB34E4D797F8D44CA506894ACBB7142BB6AD11E016B753FD4A1DF73BC87483A83698201E44085FE6BA48FECF953BA20C246679B68A74911401613A883A34DC81B67B9019600C655A3D888633CD93B689854593790E96E6884B8BF51F4C1E5EFCA95EAC6A2B46FC0EF52350FCF1965CF90D6EB0134C349F4542853B11B0753D8B14B1D0AF44AEDF59647D3A09B7BCD8A25DC1DCA01B5CFCAEF48E6EEBB6A945F4FE4074B7842AD789181D702E95003656FB3B254FDCF913C7F751D83879989210D36521159E59CA43CD1C6E4F7A9838DCF1559837839E6D7DE4F833476A39CC94042F26E1424A779218AA43AD76B93DE9418CB863BB2EEBDA3464501318918520E1C90217C9CB4E8DDE50E80375A2078981C6721D7475EAFC210DF097C5A9127D5BA21C4F5CB222AA42431206E4069243B3020374A0279901C654A6E522CCE0D03243F8F296F8C090576F83F203F802EFCDC784B01BFDCA123BCA32ED0FFAC681E97DFE165FFAA3D331348C4B151578A3D16D40879255EFCA10B2551F6DB9CD8D22A6D0C0F255462A6EFEF26CBC6248AA8ED596A600F901351EDD875E85891EDF35D27B54DC68B98F2DABFDB4246A1C3A10EE49EFD2B5D69A9089E7AD220032FAD0809C8D412BF27A3390B4ADA56EBC79D5AC14F3323C99BA79A52A4C05C141F41FD5784FC2428DE15A68BC3C4689ACBE638BF324262FA92A36400383683A7E3F7A1226FE48AF8596CBBB2FBBF9C4166F90AB768B3EA0190E3E71C4B713211B6E7F8A3FECEB045E6ABB55BC4A3D08DC48406DE89DACA62FB08600276DCAF3ABF5267FC39AF5CD3D1A500AD7D2BCAF33F2C20DF288767B5EC294823052357B9047BA45E8767F5681E6AA4711E574721029E57C2299BEC003394AC13D433818E9B4B5F812F57B1562B25D195126F91C44A0810E0F2CD6C0A793E91148603CB22D752C01D69092DF165B7D7063A3590B0D747221FC04EBB7225238C710D4EB4D0447B53A1AF48C43E26BACDF1A49E9C44358B367891CD17A69E0D310A9AFB25EABA65737D29A392FDA207DAF6E5A8A5D417E044A98EA4ABFD2450DF3E8D56EDEEDA425278C1709CD198A1DD793C33A99CA90C071EAA9889E54D303809E4ABF7A904C952FB406D097B33373A399133B4A420AD511C8665EBC8548820D0D2789A2EEF42780A2D15F1FB9CB629ACBAC4A7815041297956D21706C23BD2F409A7AD29FA071477CF44B8E8281C099A553259787A68F5F2F2E92AFBC641B09A31AE16F0FC2426C5ED0E00EF52866F0B0CB7420ABD193981DA431B6933A89D6F7CBB09D4F9CE9CBA41D9C8C5EB32086AB4C504C4C8A687BEBA08CD65D43144674EA958B387A6CF542FD3510C0A30E55C7E98D9527B7518C16C73836104BA1CC9460227D32EF4EC19E105E071AE8E4EEEC591299177989DAD8FD1B8F40E65A4F82481A218CDB8F3CDA9A049D3478148F4C1E874B824C19BE0926543EB16E2055772E0251A3DD8F34132C5E0472A8550F069BE5F004F92B8E14E22C99DEE4DEE4399D297DFC37103A478BA5E7C4209922AF9148E1991C2252797B9720B23BE391D865001224903A0BE6D1C89D37378D4870E383A391BA666BA89CBB9285EA975E7C1B48D0EE2B215AAC8B4B59A2426AB293602FF00210C5C9FC46628789BE99B8DF0307ABC86B9E4BD50B66703E91CFBFE549A5CEC2C08EB19EDA1AA81EC07452A70ECD5573F885AA679EBC9A4908C4887459D140A8F45600D121BC403409107EAD0E4A4EF6FABFA13ADE1B836A6797B0252A67BBBD3C12C509BA34218E2D413D5C902077F38A47075F569720901BA23C2AE586AB3C29F15051770E9A3015BCAF012B1DCE1D19D51688B373B9766A17185487492008EC418E3CF157370D54F106BDDCC89C38AEB7778DA6A006A0CBC812A5371205B4D9DD5DB926F2AD2201E5726F4E8E60B6E012D02B16AD92E4F2A59F8860B9D46EB4603C77E6DCC2F64B9A2501B0B7A741B21A843136CB638810ABC5FA422FF7F79BADED8852C49A8F2C422F60AB9D03AA64B92F5176BF5C5832AB603E9162DFA189486D399C149360BE16AB1E62BD5E80DF6730E83DD9E372492C60BC4EA443B6717365B07B80EB5A3EBFBF643188E77C152FE0B846016098336A2D58AE05548778AE17E077198CCC4EF69958B10B380703CBD36400BBB31DE74454760EEBBCB8ED4CA781C8ED54AFCB9D8606F68160ED5DF14F041AE7F0CF0B45CEF41A08464EF5BAD82169601F083FDE05FBA2D0D9C0584847DAAEF123136B9B608ED9B5110C954C686D6ADC78745B0A0FEB109523430D9E531909E03B4F956448822208AAC4C6A609F9A20311C362250C574C7F725EC0E2BA10113B756209E28528EE40B742C1711BE405F48AC6FDB6B47B345D59A1DDA3490EB1E6D014F8261E1928142C97112A18ACEEB850E15F3BC26636F22B3C7B1A22C4D202CF8F115BFFBCD5EEB27802F1A3C276062C4C30526060C4014B6B4C704396120C101BDC82E1E00629ED6EE65031353923C18BBAC9F41E88BB498D42BA43DF300640A4CD6E47A01EE49133088248900C07702C486A288AB38686D180A33F763720B59083C058F04312D6BA0E0625247A5D9D7408F807C3101244F2B3166D9EB9C1F000FEE502E7D5D8680C9D47B0041CDF0806A831585E7772C2C673130D161CF40DE68509FB060D8FE4B83081DE5486BAC5A8F0039301A32319C5ACC659731C3382C3FAE99660BC9A23974952D51C321EE888E36F7159E1814FAB71E91A848491A2C463D210598AC7123FB6143C56E5B1A6DC88F1C34975274C64942360CCB841906A3C406190882E57E7AA827180021FD56918621788DC0333DE14E287EEBE20C84F9D11294D250EEB23BF87A33748C2D550738C1A1E47C2D5508B11EA713504844369149F0673471036455B707A326EE8B81EE22161437FF03838E06C24B4188C03CE2682216461037D0063D0100DA4D6757E3C10A2F3E43511C130F023807427126CB80A6843451CD3A2BEFBC18D6A4132915F7811EDA270E35874A766D3476902C1A8E5F3BB4E1683B8CFEFEB0898AF51E85E08AA9B39D94B490EEB5C4F6F4CDF21276FD4009417691A860172E4D634967A4340BC5C110F04EF890B8F07E0858BDEA000AF593AD8A2671A2C6EA54B0C0EF4EC4CCC0DF5E8CCC40051CFCB3A3930AF5D6E939845B0BF100E1F8CAB1070548E9BD42C40B097194539A9681C17C579C5F16561628CFA9E608C0305C9B15298665C4F0BE6C6ABAFF956383DE60C12E81399E93CED15991A86F40A6A03FFB41FE48E26D339EBC797C3395052CC015B81371ED58DDA86410148763C85EA0EA744920194946246C2ACA1AF09CB8D928491C31B782383252D52AA960FAF5A27C33794A8C920B6A0863A8B62E43630867D21B8D0F5A1DC500A9D613630CB7387697240790E30C9AD76EE3B804E07B976B55F6DA8B9EE1BA5070372E0D8CDB0432E1B45834F3D9130FF099A1608127E05C5CC372D1780371A2A63DBB478E85253C92D25A45DE43533AAA8B2DA8DE6408A4B76B5A1E0E34D9657050DA633A43DEBB1C2D398781C417F643C8E688F64F048BD629FC63711EC7EF24A79CD02864ADDDB568D57257F5BAC22001E6F090656C9C35627E227F2F6C41FDC66E750109B42F750EC50568FD49A4750E810AACB8163DD15F147ADC1B511C417DFB9113B5EE51BBCE6E1E2BB33EA4071709CEE08C649E49E076487E3A00718A3EA61A1C428715CF2480FBD60B01EED64544AB73165DEA39DC9F41A2D9C3CE1D14E52648A96F1CAF14E8219F2A222E3C4592E5D7F1E5535F394ADC9D29962D8FEEDC9F6D6EB85E7478FB7AFE378F99D9D9D28251DDD5BB8D3308882ABF8DE3458EC38B360E7E1FDFBEFEE3C78B0B3C868EC4C6BA6F923AAB7654B71103A7344E5264D273D3D74C328DE4FC6EF32556A7BB305534CCE494ED118E92B87FD78C5BBCCA234FE9D0BA0BF9C6543770F9A75D5C01D26BC60144ED942D45460AB25152753C773C2C20111E1FE682FF0560B9FEF0E895F3BBB6946D3A852E5294D5697782D52275426CAD3D9F55C3C61482A79923C8D63378A9FAE16752A65A2C2E804518C55536D6CF234792A6EF23D9F85F33A9932519ECE5152653F75E652FBDE65AA3CA5E7CB046968C6CA44553A0C6F44B22A2D963F329DA5F668879A4DF454DD61E62A059AF4C4978205FE035A2960806D180968E055EC061C76A7D360E55353BA4C54019928BA094206648A54754A13C78B616A598E3C45FC6F9D5296224F618F25B1A74AE3FCE0B84E214D90AF7F125CA6BE644812459AC2143CA37A912628F4022D02AA0F698A3C85F7B0B33C0A978A34792A078BD43B0549244F52E0E5F6143B5BA9319325C9D3F820B83C7763FABB54A90A5F26F76359FB381CDF967C2AA7285CB889158EEDE1DADC21D215D061FF044551EA0FAB860F55B255B456D1D2F98A8A36F333D152CDA62E25D5952C5CAD1B156BCA6A363CB50DD8E08620CB0283049D3B080C8427D896E850B98B55870841DD91E38491F9A43E0F065BA7550F355AAFD538EF50A4966BDCBAE31693CC05E88CFAC055AAC2CA26F3D85A5BDB404E5C1B69B0AB2C225961B5E6BCA4176B698A1A0560C957A62A8C320A3F71A7F4201789CA74D84ED532ECF69955DE238165AEEF172944863C794980315CAD231C46A11F3073D157DA0B31B6A15EF811AF99F3B07371119D176EEA638DA4922729F0143B2130098964855D1E7FC6522A13157021DA0BFC2B37A4F08E4896A795BDAB45F320BCA50588CA92A7F9ECC6CF9F50D124EB3916972D2ED3F92D7039F3ECA101CDA91BFA76F00C571DB7A99CF49BEE489EA46270F92F9F871E6D70E5890A86727E67A06625E769FD9B7F5D80A1852E193A7714BA0AEFB41AE095791569875E9CBA9B0F5FCF9787CCE963916661C7C2CE86C34EEE145A0375B2D85AED508753D7A28E1C158B3A1675D6127504AEDBA57187E3844C1279B8B5C7BBA7D6C52C4D0F792862459A52CFA02D9E56FB3BA650DA143682FB6B2D76D72C1ECAD0B98378483B2C6C0989543C5175546C22D00D301E3BFE7CC5DCC3AB522D7058E090A1733781A3F24DD71E35CAB8C1AD20835F7BDC8B3873974412114B90E192966422599ED6C70B6F3FA06E6E17692AC8E30561FA5128EC2993556644BA466736F8C97495634FE7925EF61669FD23742E36073E284D074AB74E4F9DF89AC2E83465084D68E8F6EAEA1257DA7F4AB155255B1D6675189DAF7E13368B45DF527F1501EBD57517B7E6B8F5160BC46A08FCC2F1569460E449F68A9D458591A0021900ACBD615B5C136B65D7722B77030F66F6E19EFBACDA1F06A6B2B7BEAC5144A6AFE72D403326AC853E193A7710FACED162E9255D6A0D7C058116B0C7AF3A729BC8E81AF5099A208FE956956A27B99DE474BEE224AFE279B69CE4058116939C5F75DC933CF9840E1E8B3AA12A55A14723BCC79F4315885F7D2E0F4D2DEDCC5849C4FE32E76CB9C852D8AEB52F122CCCF707F33860B106C8EFCE5A423C5471DC006F21B02F08B4802543E78E02561E565C03B3B227F2ED608B53D722974C7D8B5C72542C72C9D35A13E4CA8268B7DD32032282CB6C9781D5C60D55F6E84E4CC94E443A5F7122BE703D4FE745515EBFC574E4D6EC6646E2D94F5328D2ECBCB6F35A96D69ACCEBDDCB6015E37D63DD7BEA34A11633BD9944474AD8CC239E8D7D2A63C14386CE5D060F7DD4D0818B7533D679AFFDDA3DF5337B4AAEEB6E7D123BF18ABA8B53A459E4B2C83512E4CAF5F21E7E3AA1FBC82625D2FE950DA7FAB821CCDC331B3BD5C594EC54A7F315A7FA61E0C713F77BEDB72E0A022DA638BFEAB8A7F793B4D3F58B725992C2556196C6892A8D094B63A24AC302830C9D3B080CD82C7503FFC8BF0ADABF53AA68B479AB24AADD0D42988BAD37AE8DD273442D7FD2043594703CD6A421D3E5A9EDCE66218AE8682545A2025737B377BFFD31C5589EA648E5BB0095EF2AF164E8B186B157BE0676EDECA541AB6DFAD73687475A2E41694A7A8A874362DCF629D17FBA4354968A348DC9759FC511193A7710470E349023A9DB022CC05AE33D893376FB0F47F88C9D97C9D05C6461ED6BB2C1E42A5852CBA5777BB140F175408D563D479EE25384668C839932510175C260CAD8AD65A2029D193399F2A4FE2CFA43E775BD7E9AA0B07FC1C46B3D510CD77A86164EF8B24EA34853B0517DCFF5A9C12CD2FA3E97D10FA66BE2A9B6B5DAADB6ED4FDB821EF415346E6A41B6D2BA70CD715BE649B7E98EE449D612B7D820476B7DB041F7D25C45A21D42ACE74539EBEDDA829805B17180D81377FEC4F17DD4FE164C49A1058409EA8EDBCC31F534D03EED93A36291479ED69A20CF478E3F73DADFBDC3B5DB5CBA83EB8D1B6DBA38EEB84473D7F793A1AAD32392E569CD5014BB7EDA913AB55A46FF47CD6636BDEC7693C5C49E3071CFD138DDC1955B20225CAD1B40C46DB1CEBEAA54354A47FB2C1D9CA646E5BD95CBBCC32852FB87FABDC08F93AF4475A84854381172E83001598A3C85F7103EE2AAD328D2140CD305738C9227299C041D506F52D204F9FA1F0497F5FA698202409E52327F3AD4B6C269888093B52251E56C933DCF543BC3C4613018112B13E5E92CA7B4B067290A3D61B1FD99AAE29A9CBB342F799282A4B3340E54699CD25074AA8642E3BCC578CAED56BB7E9D821D3B6DD1B3D435118D2E65A2820C5E5DB953961291ACA693CED0320863585392790A3D5C7ACE949E26799A021246182776A3C89D53967E3D47A15FBB2172A86E6549D69CB6E6349DDFC29C3E0D966130D532AA33122D4D6B5EE5EE0C6CF659B1EA01AC59339DD58B55AA9AC9A21FFC288B53C09E1515A97DEF2D648089A70D3B4A749E2A55F6B20E99AEB2B73C833B58CB90A7B78FBC5BB66F55AACABE93777BE53914AE55A96A7D8299ACE7A800E595EBA107344AE6890A8B9F64C65154F224791A7152E1619D469E244FC30F96DE6DA298237A8FAF96A160F843570D972DAE1A66F8CEC2552D43995EB274E7922CF3AC89624D143ABFB58972AC718FA44645CB5001EB8FD956199B5DB03B658F418A34552A800627D22DFA58F4A1F35BA0CFCD2B2DD8B979D5126FA08A63069A0F292BEC432513EC43CAFEFA50C9F8FAF02DAAF65BFDED5F5BF090A17347C1E33C74F01EA71682E4345AC208B7B63DC1BCEB2798BAA729F604D4F449909E263263EA9BDA98C9AA3C84E828E9F6ACCA5B101D252DFF891B78E94DAB0BF68898CE6B433566B6C6E8BCFECFB1AB1E00C75B4C661BBA538779F2C064B6EEEF85C31C7EC125DAB71006CE4CDC425642C1928BD1821E92226DC893446BA7CAD0B9DB76AA81C3C01A253D9B756D8F06ED811E978A3DD0B3077A623AF6404F4CC7F4815E0EB62F0AB327DAF39C28623C17724BC9B754553E4378D53C851AE216B2968EB574E87CD56869A010B77F5100D36B13524D925037968F7D8C2D1C5B3B7F473B7F091561701613544DCC6521393BA3ED8CBEC3339A59EDEBDEF2A18999D881B0777EEC9D1F0B497710925EDD98C0A257377A2004D51F33FAD88B4016512CA2B0CB16EC1E77EF1A4DDB4783A409B55F9D084874072D66EEE198B82F626E721C457B817FE586D41A894856BA1984ABB01DAB658C4DACF7D1D20963DCEC0CC5C977D0956E9A5E7B216FA6D48DAC9B339CAD4A91A1737755CACC8D92CEB5F7125627D37EA671098C7D82B9D1EE2A0EA06B36F51C3B65ED94A5F3DB4DD93334C79BC29A3336A3D27EC2F2EA8F7B3BDACCBE96DDD46E18653BD3A9FC96333D706626B473414763B673297433DF3374A16954A9FD23879DAB3274EEE05C7D3FF0DC9973DB7A8EE6F55BCC4D6ECD6EE6E4515434486D5494C9F2B4F22A4530823A452653E55262340DDD257B5854CBB0B3DECE7A3A5FD9B7F6ED69E0FAB1867BED8C402B0FDBBCAA1DEDBC260DE35FD4966299AAAAD6595A64BAC271AEA13773FB68193F5DD07891A5C95379123AFEF49AA653A52A2C2F027FEEC62BDA972391AC40CB89215265AAC2CD713764ECA8224D9E8AE72E68C7A8799205650BCA743E0F9477A32898BAE9E5380699CF020F5D643B79090A80D85B2F42C32BCECD32016C9FD1884690BA9804AB700A79089702E5941484CC7834CA7615BB74EE8473042919A92E6554143BF56807FC3CF25FF029BA89F2B85B17F837E27D47A820FD358932F8A7C42765A96A7E5892A081EF0BF44FEF2B63227D7FE32AB4DAC501FF0333A5F811DA8018CAC0D85104353F2C41CDC077A5FBA6F7511312C6BF6901CBD8D784E3FA28A48B94B89FA7947F474502FE80CE1C65DFB7AA37995EA38593B2122D9D29362F9312876E18612DEA5C3A11CA8A6C6F257DFFC49DA1F0F1F6E436D1F98B7BB8C0BDC92B6FCF73530718458113C777AF50149F072F91FF78FBE1FDFBBFB3BDB5EBB94E8483EA7957DB5BAF179E9FFC711DC7CBEFECEC446903D1BD853B0D8328B88AEF4D83C58E330B7692AAEFEE3C78B083668B9D289AD58E81097B9FD863AE7FB547BF8F98EF5F7C823374457CC11DEA8BD0151F015F1D378D2D97389F1EEFA1E4CB248A7976EAC489E5E0E35228EDE4F6D6D395E7E198558FB7AF1C2F6274374DFED409939A5423158D385C359228B7DA320AFE274E38BD76127BE6C4797D8CFC797CFD78FBC1FDFBCA5D4B3FA490EA3BF755FB5A6EE183DCCA8D586E870AFAF550B95FA5556A946A65A26664B14197B99950A3535AA8467B4758AB1DD055E79B345985D31F321DD7160076A7D360E58B67EF3BEA93F7D489A29B209CD174FFCDC279FD6F55BF68416CE2784A3023433BDB9230CBFC9E2251997EA6DE8A8C523C092ED370D046893E3F6BEAA67A3FD35BFB06C4A8F03BD51EFBF34B8442B852179793DB53776AF83B7C105C9EBBB1F9CF9B9FA46BA84F142EDC284A77E9CDCAC9EEFE098AA23478A809CCB16AFE8EAB79BCCBB2214ABE2B139D3F995BCDB92E2C7E03886591E08E2341B60DC13CF1DC683878A70D1C34CF14F509DC4EA6151673B17394D885AFEFD0B76DB14CC27116B3187366E99E5F2376A9D44A79A494A085572B6A7BCE4B73848CF56A82C24FDCA9515AC6FA66B7D6ACA6D54663F6006F7D8118A58E6D0DC3B004BAB7D9822803C31B0082172EFE881A3030899DD0C8E4CA7D31EA92211E6AB667EA2374595C77D45B8E3CBBF189B8C7166C2DD8EA80EDB1EBBFDC14C055C746A9030274A337D1F0103F0FC5DBC62DB63BF3CB035A1DD3B6D88CA19A45238B4637D1A1EBD13EFAEE121C49D115E191DC873C6C3EF61BE26285C5128B2506B1E468818F022D96582CB15862B1440F4BF25BBB9B82265DEC4CC9CC38B9DB3EB1A337659B764786DC6733006D06F6A22CA8DD75504BE6C8E6E1DAB1E3CF57C4FDAF567BCF76E2DB89BFD9131F2BC7F4D4773366FD9A5D78486426C181CB06896C41F8E385B71F30D788DBDD5908BC20842E06E8F6F1F9325D9537ED86B7D8A53FF071D98177C373513C105F6D6E316EC92CB9364DD384B63470B972758989EC3F353E6456D5DD715537B98D4E90BFB26A4EF8151AA0F86D658A2F1C6F25561CEA34EDDD328B07264CDFE29AD3664082FE56D973BFAADFE715B51664B347B75D1842A3BAFBA66FC75AA0BBEB40778E164B0F93DF0C98EB6A4BAEBBC5E81334411ED1653B8DED34569FC6EF070B74B7AF674AD1C5CE813049AD8D9B91DD38CFA1C9342E750075065666FA260FB1A9AD75EA6BAFD65BEC3687DDBB338BDC16E5FA45390B4FA2096FE18980A7ECE5B545288B5016A12C428D0DA1CE831BFF0E81531BE74BF650CCCE33ED79F6C24D886CCC3B198C1A7A9AC74E563B59473B59772F83558CB78437EBDE7657EF5136F9158805030B0639186C0A0A7434D98CBC4AEBEEAC187098DDCE9D5BECC42BADAB2716952C2A99B86497BD9CF0824D8990B0660F4CEC34B6D3587B1A1F067E3C71BFB729FB025DADEA9FA463A431D34E74094C7409D8B97ED7E73AB61CDDC03FF2AF820D99EE06829A0977123BBA062FB7C5898C7B12C410E0785DD82265A866B31D3EBF99BDFBED8F0DCFB394E8770D1335F9B6C0C403D52E76DAECAD39AB898C6BA2C323EB095266CB271FAECDF4E26611E1AE230213F3797D41609DC2013CC7811263E7A5EBCF2FD260C78637BD77974BEFF66281E2EBC04C84D0A708CD4C793F49FE9C3619AD2D783E9D11D3C414D1A6D5400B9287CE6BD3244F14A366CAD03C430B277C69E4833FF33DD73763A3EB9FC2188B76AAFD14D9DAF5568B1BD3E2A985B9219ABC23A59B8C9235E32D006C2800D82B6B8D44AD0B657B4DC502522F80F4C49D3F71FC040E36058FD6EB01DCDABC56933D3EB3687297D1E423C79F3916497A39ACB84473D7F75DBF4948D47B384351ECFA692F8DD336790EACBFC76477762CF29941BE3D67630E68302B98BCCE84C0348EF69BD1A38BBEBFB772C5BE3C5BD8699DBDC908FC18F90CDD76473F4E839FFA361D4CBECDAC9AE5ADECD385E2A187DCEDAF03E6B588EE37FE20B8344DF2E8D43445131B12A7219A3520A1B4D37BD3FCE1A8108A722C4376396527598DE6DBCA1D35A14427E7AE795E0FBA207ADA80A90FBABA6CD9E6505DE2B2659B21E8AEC7A71D7539756F641E299F5D5DB9D32E0863F57D86964118EB1A20CF969E3335FE9D8E228C79BB51E4CE7D0D007EB61B22A783E9649720777D09721A2CC360BA410B112D43C7D44A8654A85AD692E6691676FCAF7714A5BD6392611F96E2A64191D2D4293563377D72A7C3267AB68FBC5B63FD9A25C4AE3C67AEF1E5707F4CF1F67C79E57AE881F12B780935E344E384DA43D344FD60E9DD262A386237385B7DDFA5C445C9361A1DD38D0400A64065B2BAD426640D0C6B6064E274BC31F757B46D8C31A8F5DDA9EA598D3C55634AD0E287C58F08DDBCB2C09111F8F0814EE5873A95DFD2A86C7E4BDB02830586648D1D3A78577183D0C19EA1DA33D4166727F658B6F363D98E4E5BCCAB46EDC545475B3D1959E3FB3219D9B74C93FDC40DBCF436DB859163EF8A5CDC74D0DA62D3C7D0D97CD5C9E683B0168FA12AF253A7F901487BEA69E72F9CE6E332ED36C2C0311E01D88DD1A26170DE19EA0CD25ADDD6EA2EAD6E7B6C688FFCB814EC919F3DF2E390B5477E633CF2CB41FD4561E1447B9E13455A2852D13A4378113FD5A668ED8FBB6E7FC062BA2146887DF46D67DEFACC3C02D4EDFCB3F3CFCEBF9E57DEF63ECDA8D6CCF63E8D8591F582915737163FECB51A8B0F161F58331F3B9DDDBB46D34D898FD8D5FD946E6E44C8C8B61CA1682FF0AFDC506BED919368D12155A9DB474B278C17C9379EA1180FEC66089F01EBD202B305E6748ACCDC2821BC29FEB20CCC0C37DA5DC581F06A83DD61B1334C7E869DA1395EC86FC604EB2EC685E68E8BDD13B533D6D88C0D9CD90629C50C8046E93EDA4EB9BB3EE5DE0F3C77E6DC6EC8543B8A0A7EF2562E5D753D949328BCCF1BD9A8DF47D1347497D09182DDF8B713B7C5C4FDC8B93D0DF024DA8C99BB9754C2BF4C6FE865CAB70BCA1D3D20DA47CBF8E9C234D527A1E34FAFCDD33D0EFCB91BAFCC7BC33B76E24EE8CEDC50D19092A1EAB90BF36E302DC6DF758C3F0B3C946DA9B541795CFB82857AB06CD60A50BA593194CDB45FF913ADCB12911EC3A7E826CAC32DE19F6D069220213B9EB84AABD1A4DB6A3FA865178C0F6915C0AA6544DA8A80EC7826355A0D27D552FBD12C3AA03B98BB51144CDDF4C625B1F57671EA8489E1943A53AE77E3C09F6DE11956F7B43C41DED5BD2CE164E5C5EED273A749638FB7EFDFBBF78061A54E03D76DA4F35B0C91E45B20DC47177BBFF5A338749241603F9CEB4FDDA5E3917DA60A495AA478084B72744E6224E1C7EF7E4CF325D35636D6708B25614ACE9AB87FB4437C58F1F7CEC1EEE456FF6B3FA0F97DF4CCDF471E8AD15676D12C3D289F3A3356BC13F19CF1DACE550ED97A91B4D65202A9524E4BC57EF820128291FBD8F55F5E000AABFA4A4F8988B6E937CA127A9190A2874CFB59622752C20E46375252B221D35652785031C1414E230372D2A037CAA6184279EADA7FF18C8F75F8E4690CBA7E3E792DDC5D49284F5DFB4F9EF131F64FFE11BA24D704C46FFEF7AFD7A97D403A4B592620A2028A86E482EA764F12A2D21C15AF7630782804258D7B9BB4F79A2B255589BAA557A6F6664A0C23539C111A873815618B0797A33CD890086B8AC0A134D054E9EB8332659F47281344DCA7A15451313C635EBEB690C8355BC8AACAE9A0ABD9A4B379C0CFB5D149559769012A93D75F2311DC8C5E21E11856E98E48FFDAA868BA46AE4ADC043D5472A3A084F01F838AC3EEAC0B61E80850B20E33228493364580125ED6497CCEAF5162C5AC9704A57D6684284FDD1439CAD85917517AE17A1EDE8E3B0F6EE82706D5B74B33C9CF9625F4223779076BCD97699DC80C3B14DD084BC1854C53B84F83C9C8EE65B08AB17E1D6E494D77A14696CDDC042C61B85A871576D9E90BB0D7A6BEA8ACB4C0443B928F561FCC9490483536F8FE2EF6E38A9F92F857417E63244FE18A0951A3F6316BE94AC241D4646E9208A81A1212B2851EE4836656A6C981AF979032A26F95C80B43CF82D09795A12A70835A1A44670F8FF0C5006EEFFB8288B41B3CB279E6860145C6954C9B44A5C144E62042F8EAC3057B93B3FA9E388FFC84E9DF4A1291B74253C9D23AF9FE0C3FDD7CF7820999A60EA2E10C4BE21E6C76502D342EBBBA3F50F58296844DBB3D40B024B9EE18DCB82444645DCE698692A75ECF69144569D0739A3ACCF4BCC93120BAF4B9B1A10E2D836E693C71E74F1CDF47E1001251B65DA347A46E823C54EC2888C3A09BE61F39FECC19686D92B65D17AF2C65135621192BEBB2F4C03E1C070005DC6C8D5496B009509072B20E4A017734F37A7EC1F659F55B497CEF3CB6194DA948EEE4DB4B7F0C031F1D8ADDC669AD70AE3CF47777520F7F9DBF781CEAEBF7751550F1EB0F7A1390F9FAE0DBF616EF10472A030AEF0DFB9582CA4DE7C072709C2C147B5400C7D4D290CAD91035C00412E13438124D8065A07765308424F4AF12642561245AA19284DE15C310F230847A9095881168889B57FDA8869B570C159CB4F6CA2061622DB440EEF4B54F1D9037C97CF7327DEDF1BFE0642DD0BF1606AD79D22B7E3D7979E0AC11A8DCCE8041E59B99139175DA3AA80B4AFF8031A0800C001EEBB4AD0049468F46E4F0F2D1AF31A92E21233028EB32220E394D5C93842354D72E4E728AACA118C9C4E31E9358F1C3DC8F4ECCF8F19505C24656128A5CADE026081E77B8C6247E6077C7237D52DBAB1D9BD3D0CE0A5B60238DEAB5D98865846628D37A406119CCC05E9B4D5A8E940C65660F282B031ADB6BB4817B5E84A51E5007BDBAE1D1C4391BA675E800E09C06075537E77064D87EB184D309C8C266CBAC39A2F0F85A035039AF854C1D44648A70AD80A494599B21206060DA51CB4516EAA72FDBB5D628240F45CE5A5BAB755E466FA616DDCD03485E80B15FB5BFA1A46414512C21BA655E979B290A5FCD909880813B394D566131071115DCF18B0CB8A28B49B00AA77C1315FF5BFB8C59422F6FDCF0BF801223933B91A194C51E448760444A6A025E50A3DE85E6DC09E788AF64A42D8F0D139CFE2C1545D1E1C6B8EA49786837EEA811775A7842EEDE4B37185CA096D759648061DCBF4BC724688C133690943500D5D081648697AB310BD47092443CE63E90002BE5E7D79D3B0500FC9690395DF91D19C4CD84ACBF93A6D07B430856033E49B89FD954511AAD0CF5253C076910C5A44E9CD44061B1831DCCD0A11B4638E6AC73091D2EE05A1314137B12DB5B076540C6DAAECF647A8D16CEE3EDD9257E639E0574C43911B0C4AF932DCC688670910191CE570F8DC4B32526433A4B8608E39C66B293DB28468BF4688CA14DE4410D94D93223533A820106A7CC83C727CF966026330B9816B264887896234135F3730952CEB278D471AE5C1379E434B08D3C8FD7489E2DD1481EAB0B6C24CFE3359266CBB5522A78B09D3297D75219CFAAA92DDAF914D31C5D006AB15E46AAD1CAD300D46295CB69AE2820353B4F90BF82E76696C3999938538A97328205C44A99C9E124CF6F6EE81C2D965E1A189B69A6CA821A29729B9BA86230304D5459501345AE5C13D8473FD800CEE091DF9DC911CF7DB783F4F33C5E1369B6C487489D7AB21F214D063F4092D34CB67421CE502E7320E27966337DD68331D3105B046A912EA5D0B4A8CD86C6A451652FF002C882A8670B70252DD1DCDC6162414DDCEF411FACCA829A297225B08BF4C9C3E217990B62585540ADB1DC8BA8A8C5BC4843B379A98696537B9F692C4D85E8E3058E0CCDCC15264437CBE1D096B304C83538D484700655F9CD0D11AEB59876883CA899325B62EE641E9BD84993A583B305673553CE9E903284B364882ECE91235BDC9E06891799BC26B27C9586600B9FCA17372765E917CF65C1A67006AF8D9B5772C4CB2B60600B652EAF99BC80525BC24F4595696857F6C3F19E90B08A9B531054E5605919A341F8BAA0B94FB5E2523D236A48CE27F6AA67F3E7120A3D5D52A91BF836A1A87D9CDFD0F02B89A5695EF4C471BDBD6B34854C14B688E00394A5A49B66EFC3F17AC0961474842E2CDD9FF2D615AF1B650141EB7919E9468B6B21BC368B7C41935911F916CB0B23DC36CB12A256F34232AB13CF9D39B7E0DA24CF81572669A68C0EBF3D0D5C1FDA96ABB2604D9EE5CAEDA071B7E8C84CDE6E9AEC561D735025DA06116F4ED50A495B755C9B94CC17DB76A09D4A6CCAD63753F3E823D9062A518AD858258BD0FBC4F44DBC92B32281D959AED7C8028F34D4AAED2927A52458CB6F08E4B16D01C6EA054CB25517D4B44E91A4CD56B12F79918915CB56BD00BF93A474A75D84E494A941AEA5CB5A59A211D6D235909037A28469E66ABBB865357859D68EBD741F56C81E51C2347BB5FDE3B25A9EAACD5E7D2BF6A2BE2DCBF22A2ACE6703DE344E79A1B31A06032221A8DFF27BB34EF8399FBDC15B3F052FD4814C8E3065EAA8582F9C0237300EFA0EA6BF3DBD775F7CF82A7D70D681B8EAB0F40BA3AF6B2AA01643D58E55361838CC6C43D07063E2CD9E89147C97C9DA6C33F1AB0196C531AE8D8A357D749256AE128DB04BC467E630CB8BE06C9CD5EA08A7647497B9BED092CD7A1C610EA78260C3C699AD1D2995FCE6A9DA2CD7E2DD02DCF2E3E1D63A4A9E4AA57DCC1204AC51074E69A5324D9B2D6E88568045B970AE46BF2BEF342C25C2669A1B8E320CA9681CE058A5E6188029C1240C30CF0FB2090C826444CE1A0BC04159CA442D5D30044C044E391A9A43C19BF1E278939AB3BEC550E9B1C9864D14B3DC1066B1ABEF5E3BF3A489C0E798EAC34286060446811B39B0D669626B2AED67FAB78049EA28B4A893A599600908830733D7142FAF8345267B4E5BF06F0EDBC1306FE211E8DA02EF996D816E6F8A5F6654ABF7C1361B7D0BE0BA21449751A6991B02696D22557F89C9449782D697E210544671BB765D211BAC2C459BD53D328212C0652DBFBB6F4ADE9A482B660946D8239D027338E4FA0D56EF2453A37E15A0AC57249B6491F05B206694E7E04073036810768B330E0986E1979E6D8E500664BBF4C827E418F6DB674C9C8F298D43E518665756AE457EE78C89F610ACCBC8B8D89D9A5131EF63088AD80D1C9EC1D00E06C4BBBA8856D6C14946583A67C31470B8034A9A1666EA525CC96D996E926509150C9434CA00AF3E07B8A9DC0E06435A0ABAD7D9E3180A31C8096A740174430F09733B5366541A7C6E93FB68C2CBA2D9CE1AA7C8DA0C5CED0AA9DAF0093C478B0611B8E30A0F65ADE0B807B4C9DC840BF708DD9031C216E866601431BC63FB7464C3A280E7BDD8AE030F4FE1C4563C1EA0ABDBAE265075E59CA68073B4999772D40A0C87BA83570312D270399DC471B68CA9A102FD92F247A8D98DA9B981A16EC993E35166991A06D60D277F0C1A5C766AC22A785B9FE4BDC831C63AE57752C0B8C843A50926401AD423821A95324F7B306A4EF272A741C04840C5F82C90EE30D27E670902A6D9EBFF653D6337AB6B3CE46E6C9A5885BCDD1898E97DB0CBF760D7705353E0EEAEC6448BFB96BCFAE07D67D1F30A23C3C1970149BF6C0C3BEA77B6FB1B008E8730F1C12AD79798DEE128A73670FF80CC313E047C0190F17A4533A17A75C230D3D839162652FA5F2AF31EED64CF89F284E4CF38089D393A0966C88BD2D4473B67ABA4F602657FED23ACE34B128F129A3E4A7D7355448B32F848B5703B45F5A8285264130E87B0FAC25776AF9C699C644F516255F8899CBC70BC151E9E04046747FEB355BC5CC509CB09287AB7E46060F755A2F61FED307D7EF46C996E09986021E9A69BB0809EF94F56AE372BFB7DE878F447E391C07EB1DE43497AF62DE3E4FF687E5B527A1AF89284F2E12BDD79955E599EF913E713C4EF5BF318D647ECD1BEEBCC436711E534AAFAC99F89F8CD16AF7FF7FF03E572A5439BB30400, '5.0.0.net45')
