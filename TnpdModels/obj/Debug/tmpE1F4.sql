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
INSERT INTO [__MigrationHistory] ([MigrationId], [Model], [ProductVersion]) VALUES ('202004210216192_init', 0x1F8B0800000000000400ED7D6B8F24C971D87703FE0F83F9640BD0CEEEDE8322B12B61761EC7A16676E7A667F7C84F839AEE9C9EF25657F55655DFECF0D3C18224D2842002162943262C4B908F122D523ECA122D5226FFCCED3D3EF92FB8B29E599991599995598FEE49E0B0D7938FC888ACC888C8C8C8C8FFF7ABDF3CFABDD70B6FEB4314466EE03FDE7E70EFFEF616F2A7C1CCF5E78FB757F1D56FFFCEF6EFFDEEBFFD378F0E668BD75B2F8A766FE176494F3F7ABC7D1DC7CBAFEDEC44D36BB470A27B0B771A06517015DF9B068B1D6716EC3CBC7FFFAB3B0F1EECA004C476026B6BEBD1D9CA8FDD054AFF48FEDC0BFC295AC62BC73B0966C88BF2F2A4669242DD7AEA2C50B474A6E8F1F6B9BF9C65ADB6B7763DD749309820EF6A7B6BF9F6D79E47681287813F9F2C9DD875BCF3DB254AEAAF1C2F4239BE5F5BBE2D8BF2FD8718E51DC7F783380117F8AD48DE2E8949C83948C88E6F315A29498FB79FFB6E4CB648DAFC3EBAAD152445A761B044617C7B86AEF27E47B3EDAD9D7ABF1DBA63D98DE883874E7EF9F15B0FB7B79EAE3CCFB9F4503943C9144EE22044EF211F854E8C66A74E1CA3D0C77D518A3A332A35C6A913260D0523C5E10A350199AC2EFF039AC6058CE49326DCB8BD75E8BE46B363E4CFE3EB12E313E77551F2E07EC293C97C26CC5B8EC350281E38E527C561DF118D2A43EDB11BC54F578BA66FD330EF41947C2845D41FEAA2EE26DCFB2C9CF73DEC5132EC7EC29EC5B8F8F779224E54E13C5FCE9C01662D1B768079CB066E35734F9D0FDD792A04C1059F8AB1AD33E4A54DA26B779949E57BB8E2826C7318068BB3C0CB451F5175310956E1146316C0F5E74E3847B13C6627687199E82B10ADACEEE2E49646AA56510E59A054AF2D1026117AB4538978A1E0CF406D92E8DF9D4E8344AD9B149F72A2CF89A29B209C290E9CFCD45C4EC5C013C733ABAB6406C7FFF63ED57BA64795A1F4FCE0B8EF214F824BD7EB9DD0E767EA846A538A1641EF0B2791313342ED62819C15E10265A63C5838AEA7AC4AB5D7C2C9EDA93BED9B45BE115C9EBBF100AC99E8BB661DD2202E51B870A328DD47F5CCE5BBFB27288A9CB9EABC19D011D62EB776F910767961DAB6377E697B1C368D65F1C130605B1CD75C94C67A850D59CE58E2B54A2D431C43DA24337C30E7496BF9FEEE7D7D013F84E3C68852B4FAC1EA8741F483C03B22259169F5008AEB561279729BAC89C57130B762596CFA762761B46563BBA5AEE03D8B9DA364C7F8DAB288BE2B69E2C6682FE9DDFBC0E7D748D99DA4BF254B476DE1C8D21F79CF7939CCA083503B41E187EE74B07107A1D99EE5599B70DC36A1B48A7D8A6EA28DD2AE496B5557B701DDDA4AA71B704FEF86B13B557614EB4BC0172EE61B2DF937899DD0884C38F06726C0EC46913BF7ABDE7AD0B22D926684CC51B417F8576E586A9A74DFF62448F6608EDFEA14E50374B99750340FC25B5D9FC6B31B3F8186AD4AEB1DB19A70B49A50E41DC1CAEFD8F55FC2FE91A2F622D5918483A456C1F8ACEBB590D3BA09A543D74311C78D5ED683585535205A44751BBC8E16CE5C8417AE47205E550D881751DD06AF44A2395E30E72396374847400C72F55A1041AA892A924EAA5AF258201E9217F556751C6B958C538E6DA1E5992B3878A3ECC20E2C34A9001E74A3AB1EF1A7781EAAC63EE81FA7970BA1323DB2A2569687913DB341F3C51A1DD6E818CCE8D0B13720E1CF5A23AD057F6A2358C96F606F2E16FD72EC77D8264071145715ACA8B6A27AA345B5D42E0C12D6C026ADB5B44E774E565A5B696DA5B595D6565A6BFAA620690DB8AE5A4BEBDC8DB451F27A90F336397125774E133BBA12AFF91864D46788461488E9832AAB45AC1619448B64A22515D3902E49AAE90381EAF8A1522C8266CC0183A8ADEA49431531D9741642B4848F43CA06A05A645BA95E15C9A56623A2653B18CDBC5A8864D14615C5E2BCC8C4B192083FEAE4A9957D51E7A24D32318E1D7FBE226FF5E1F92F0A5B2917AB6AADAAB5AA767055DB78766F40D9D262574631B795BF58C7A4F1C81B247CEFDC6D85646D2512FA527969EB8FFCCD85B71FA8E61A30707320F082B04518BD3EC1CF97A973453DB6413F2CE3C0C76D8DA950237A3D5F6A07AA176BF53F442215AE7B1FD4BC5967E4A6EEEA1283D97FDAFF47B0669835C38630C304392408A3064824C1D642E616DD44F7D2F009F257D6C232A07D95D5EEDBBA43BE70BC95AA99A13DA8BDAC6785F3B885B3CA1EB3B8F0B54102D0C411DC73BF8230F25B7FFAE36631FE8318ED6B7CDFD0F096CF6A04AB118631D785477C958A804FF8A07AC86A671B69D9EDE768B1F4F0346C90DE1AEC146940BFD91394701949B415A3568C8E568C4A8BA7AF070B646F3C1A124FC9B7713259A0E50D1F597A885CE8F62E718790F2465C2686ED6DC293A8196869135758E5B506CAAB6DEC5CA1CBA0C039BA8EB1FC99065A563F86B63BB34AD59CCD6FD5CFA8D58FD51A566BACAFD6D89DF1744655036A0CA25A5B5F64D962ADCAB02AC3AA0CAB32ACCA18B7CA48A5354F6BD42A41C5516FA177C210DCF8566D1878E0C5C6CB5881356E81252D135EB80990CDCA8F82C59CAE3561458B152D56B428DB42A985011942B998B9C81A54361059CE983FB54A2DCB67F73258C5D869BB81777F074B2F72E7137A58316BC5ECE8B69CB4A883769EBC368C04E636544D135102826F3197D51765621100DBA292490FC2B6D07A08B504B7516A6228516B280BD580117DEACFD11B78BB2B76E255F590EB286EDF5A7D67F5DD20FA4E946D4A517570351CA35CDA5E3FCAD232784168B5C71AE7B8B002D30ACC710B4C69997418F8F1C4FDF646F97807F3CF3E4967524B2A9CE88398E883B0B2C9CAA6E16513DEE624F6DC917F156C92783A75C2A4A1E68EB7E11CAB3BFFAA0131798EFA7F8C0B4B34C71BC45ADC9DCD421445BD937C7E33FBEA57BED9B7544847FD56DFA30E75AFDE4CE2B2410E69EC351E6B2DAC81B52072FD649A34371340071061425C50AD2B4710B711E310E2B7543DF3E01E869343D007E2749D103FEEC1B8082D02007C18A339A1F4E14CF3D4B7C0FCF088FFFE2FDDEC82343261129876222AD8C65AA74D34E44DB283073B742226F50E3F5965B5B6D5DA8368ED9AC4ED4A440B34235F9EB712D107D166394F07D98C0C77C9CB9F257F392F93E12EB2FCAC3DC70FEC2E97DEEDC502C5D7816A063EFDEDEB53846643E44C4FFE9CAABB44F427FB744648B2DE4655F777E98F7988BF51CF639E386EFF849EA18513BEEC9D839FF99EEBF7EF6F321CF9738216AAB27E14B928ADEFCA5AC16B6E05A7AF58739C14894989AB2FB0694918BC4431E38220EB545D27C23784128045F0D301F54A1B5505E154ABD7F284E4046E94A93D94D19BCCA5757C58916F457ED3C066457E2ACEDB897BDA9D01A982B662D55E7B33E4C4B0AF6A1BD45156D45B51BFBEA2BEE1156CC232265B82A675D500D202402BD5335F71427C620C30213E542F42B49B07AFEB33415F69646B1BA6B2D535C603C1764E760B2540EB40FB89EB27EEFC89E3273A75A354FD5D4B0FB7BEA9DA64E334ADC6B61A7B088DCDD52EA5E084940B53C90871B685DE3542C79F3956848F2A10E812CD5DDF4FD1EE9B88198A62D7CF63F0FA1E7CA8B86BC3E760F63CC9EAAF35D75F4D5155A9D6E085523195EC93664C0B2D1DB6E76C56D414A627933C8444AA0A158508EE78B4DF85343743E97B2B573556497FDF335C8E9AC08F93B9E93F38CB5998DD5D4A519B70C68C10C5988FB3A2569C7CB0301D132477C3F04035298F3E7F7E23B8EC7DCCA3D3DE873473AC701AA25983FA930CDF1C206433B1188CAF4D997197536529F4B62EA926CCBB49D6A9E7D93A1864D45365CDF860B0ABCD0682975B5D6D3630CB03D27C3A14D169C2FF0114DBB3AB2B773AC8C8D8D63C43CB208C41DB9AAC52DDF02F3D67DA3FEF1C455861ED46913BF76BE4709C17529F6737444EEF32C7440E8BBD6B175DE1A0F1CBE0359E174D7047D1EE2A0EF6BC2042A6E6F6D0F592DD5131A93D07BB6583D738BFAF4816EBC5B25EAC41BC58A253182CF2A10318B29CF15BD52A554FF44F8365184CE1437D0C38ABBF489D591442441513174DD7ABC66BE76879C7BC98ED6A80A48900BDA2568061D9443DA83C4237AFF8F8DDBC0211CB8B418C8A3AE5F94A95D5341B8EC758B546147311753083910D54990C77E24F93345ED0848188B7F6D7660CB1695E5BDD031FF39E5F72DFAAE518D10E425D5CA250377E943DA84B7929BB005AD4AA9E3AA6067435377A7A3B8336C855B8037F668A8A7DE4DD0E42C32C19F8CA73E6A66C6E4C88A94979BEBC723DF4A0F77D1E1EAEFF51E364B887BD8FEA074BEF36D98246CAF107FABCB76C758FDC80C72D35C048B1DF32D82D8533595D1A0065776C76C736C08E4D68554B6E90209B1ADA40C9E2E4A41A3D3B1B126346B604F1AB1A88B0245AB5C335B3B5E4B02DDA0AF0CD9A34639CB7D30EE528F78A77687720B5E0466285EF4E8D8707CA0F3B88556AF5A1D58763D587924E39BEF466FC76C615231EA14937D6DB34A0DB8F86AC46122949BA9514EAA654E5CD2BAB236B20DE7FA0D7FDA15EF7B7F4CE1CFB0FC0B29ACD6AB6F169B686E31C48C2D2473DAD456A7650BE51B99181B3FF62A6C92AD5FB4EAD42A8C5C6B85C3C6E329EBD186AC5DE3A8A3DDA490B08C07A930B4226D56FFB8BDA3107B8C2C65A87B914451B243607137043DD83B452D14AC5C18C4142C675251469BB514A82B63624CF4307C73A6F9244B41713EDC544D34A07FFDB3FB5866FD6D8CB909B761972A87B3903F8000D9C2B0E151A958DDB7F985236EE5B038DFBF640E3BE33D0B8EFF63EEE876E9099A017462E1657E062F56BA4FA016686AE475754B4B9EFA67F85A91A7FEAB4C9C86B6CF894FC0BA7CD4D39D3488481A36EC868EFD863B4509EFF77C678DDD17A3CACC763108F8744EC44EE3F10444EB02DC0A331A099E9BB6BF91082084DB60578C90868D6C985B6DA38BC101AB05133DA9DDD72CB47797523C236AF15A15934D1BEC455237BD31C5D46EE72D9DB57F6F69532287BFB0A5A05F6F695BD7D35DEDB57B92A7C51ECCFA23D2FB1B734256E05ED0CE15381A9019876CB63B73C836C7984117FAD361082CD4EC717BEEAA334EED0E4AE7F711B7712E30E8D06C7B9F35B4A932188771711424953495A985E2272A8C61214D13DDA1345087565D26A7DE50824BA289149F6D38A8A8075E426ED1BED0367D634B0A60108A9BD9420459D951556565859616505CF096DB3068CD57B6CB30658116945E4C83C2D6A279F8D9BC62E53093003497A5D24120B88DA77EF7B694A33206CAC428FA9A403E529B2D5B4351036F780D58FFCF9B5FA71ADF4A35C908D40FAD271383ABE10FCACC1DE359ABEDC34893BC815AB816ECEC848003940D15EE05FB961E9C1D17EB13487D7023B554EDE474B278C170987CC508C3FC30631B4916DB2558A56290EA2141B372E9C15CCDDBBC8B46714A85427132A75E646C91C6ED4CBE746E48F9BBE7B247FCBC21E1658A1B6FE422D97064DB20C6AC61361605B1392EB0CCDB1237D8304D76027A106CE46EC61AA958FEB2E1FB97EF79AC4E138DC796D7872916D68442806CE6CD3ECB96CA6742FEDD828132B18AD6054158CB99125128AB9C4B9289AB222B1DE822B10A9665AE2F0EB81E7CE9CDB4D1283475141543E54BE135516263998BD048B7910DE2AB2A97E3CC83E8AA6A1BB6C1106636351AC201DB12095964E1F38B7A781EBC79B249EF69236F857EFC75899BA1864E8A192A3EDA365FC74D1FBB04F42C79F5E0F30F071E0CFDD7835EBFF0B1F2756CF2003CFDCD0F476456658CF5DB8FD8F6AB5B2D5CA0368E5D582D0C9640AD2A3E8D073E65149C6735CE5DD2664925AB48E6DE6D9CE817DFE67BFD8DE7AE178ABE4F77D86B05AD337FFE31FCBA60F58CC331C057867DB244378BFF9D38FBFFCE1F7BEF8F8879FFFE4CF6509F8EC477FFFE647FF2BE9F6E6077FA945C9B1E3CF57CE1C19A2E5D3FFF3D3CF7EF8C79F7FF73BB2847CF1BD4FA80E6DA8A89D0F6A13F1D97FF9076946FAFEC75A8857F9ABB5B1FEF2E31F7EF9835F7CFA9BBFFAFC679F48A3FF9D3F7AF3C94749B72FBEF3C76FFEE6C75FFEC57FC2FF31501E3440E1F77CD8C0C63FF9EB2FBEF3DD2F7EFE93CF3FFA23AAE75BE29E9FFFF3CFBFF8C53FBCF9EF9F7CF65715EBBCDD80E79FFEC1173FFE082F9D4F3EFAEC2FFEF6B3EF7EF4E693BFFFECEFFEF2CB3FFF5109E29D06107FF7F19B7FF9D7373FFBEE9B3FFCDB2F7EFAF1E77FF34BBC0C7FF5DFCAFEEFB6648233B40C425312E5F31FFCF3A7BFFCDEA7BFFCA5F42AFCEBFFF9E667FF487668F8E4C9947DFA9BFF487678D892F0D37A0627FDC5FBA39F245CFDD9FFFEB5F41AF8F51F7EF16BEE2A665AFFE2E764EB06FEFEFC9FBE4FF267034F7FF9D147892C94E6E64F7EF0E657FF2CCDB83FFCD9A7FFF74F3EFBB37F2167E6DD06AEF8F5C724F65F69F985C9679EF495E58FFFF39BEFFF89ECB7C581E5920CFD5F7FF5E6A7DFFFF20FFEE953624EA5787A378A82A99B3A6A8993DD8B53274CF6F0E981557DD8037FB695F960A976958FB60A7CC90EBC4E565EEC2E3D779A58548FB77F8BA1840FB24CC4270679FFDE3D767ECED015C2805CC7DB0BFC280E1DD78F595F89EB4FDDA5E38911A0BA493A59F0949703D035C97E1C5B8F7E2C9E4D9991B35EF0F8E53094FBA769761EED108C21E6973C3EE3E456C82DB55610AF149123F2DC520729C92B0FE8297DF4CCDF471E8AD15676DF315DF55367C66E2E92F53333C06320DA3D7018F80564C62D423D06E12E8CFB45863A9DC2A0E204B211C45BB85E85B36AF000C692E25505229FA29BE8D8F55F5EE01F5C2A6BAD20328B062AA4D68102B4A6288D6E118168F7B088C06F20336ED27EB03584714DCC181435F357D58CC760690B550E23C04AB298399D0E8FDF13AFB0F3B90ECC72B470E6A89959AA663C66495BA8320B0176106661C7EF8959D8F91C3BB37C802E31BA7B4EEC78C1FC82F8CDE71C411F888DEACDA5BEBCDC581CDEE20CC4326D2BFE9220BE0766939816192C12304544C8A0E2AAA0E304C5CE5132DE6BA1D4625AF38497342734C1072DC802D5519A565C1A7A9284DC6F24337EDA69408938716384FF68DA12B34D39F2AF68A5C28900F0B5D921F371EF473672BECAE8F7CAE4CAC9C990928479DB8EE460011D603F016F9B35E938B8F42CCDA8799654B1B8CB28580AFF46E21D41BD69470C950397DC1BE87965EAD7DA44D8716EB62921D70054DA136576F18048F4B474C0599533028A1BBD439901F953F4125629D498630A94EFDB2B59022CF4F532494524F4631270BFCFE80DD2AF070B947A2C9B2C02BA21C481451B15FE63E00E6508F010E981837873AB6002E03F0665A2DD99140B55CD780CB43B53651F02A61AF30C2FBA60F47B6239F65BAC13C39D5FA3640F26C373B5963CB64B1BA9725E1DF23A321F48414FFC077E977561C117AEE7617FFC797043A78CA878846C04315E5EAFC276359000C7A5F88C8ED520AC7BE03268FE6586C5ED07E3ACDDCB6015637340D659C3EB00711CDD5685F5B8E30C65B23521D4038735CDFD3A78714A1A2E0AA668E480A2A590C55AF156091960AA26E6ED80B36874FA64297A9265C61EFCC40DC73AE3E41AFE5590072CE6255CA6E2F680988B68ACC25EFC310036138C618EC31A31EA81D31A675E068781E34B491A84B618DDB023EE52B1CBBAE1A69E6D2CDEBC8EDECE22103F3CC27158E4D797F8D44CA7068E4ADBB7642B76AC11C82D2E52FD721DF73BC8A041741B8C110FD26B24E822F93F97EF8836109BE5D52ADC45420498E920EAD07007C6EE816780399419F5201ACE344FC62636164DE639D89AC32E2DF67F307878F3A71A94D7968DF808F5C350FCF996DCF90D6EB01344349F45428D3B61B0753D8B1491D02F47AEDF59647D39095D5E6CD3AEC4DCA00E2E3E2ABD4BB775736A11D81F88E292A8769DB0D101272009B0B1DAC7233D71E74F1CDF4761E3E2615A4244978D546866210FB572B898F4B070B8F3ABB06E063DDBFAC0F1678ED4769869090626E1464A31490CD4A176BB5C4C7A6023EEDCAECBBE367DA5AB4918918D20E6C9DE0A93E79D1ABCA1A40F84440F1C03CDE53AE8EA34554AFAA666F6349CE8DB12ED78EC923551651A1230C037101F0EBFDDE020DE13AF01DF4266E4E245D0A1798D48BB2FC11850027E837C07A4EDEF2DAF89108D7E79899D6599F107BD57C360DF701D80D3BE43AE1AEE8240032A43F0D6DA5D16A828281F156FFEF2ECD3E210571DAB6D6901F0929AB20B8EA271E89599E8F95D23BD473DF12EF7B165B59F16478D4307C298F4CE5D6BAD092B02A49521DDA5733E1B8356E4613310B7ADA56EBC79D5AC14F3363C9EBA79A5CA4C05C041F41F35784FCC42CDE15A68BC3CAC6DDAC8206443636E2812E6208C0261D0971F0A985099A1070E27CC246234595DE20F7551A578159D21893A41CC546FAF98B448381887CBF2061D652D9221BF07AE939918193470EF418556FE7C99AC91CE36E709B0BCA5AA1C030618C43CE7E3D1934CE3CFF45A98E639FAB29E76B679035FB5F35401C370A418877DC7E185E793D12F73AEAB4FBE8EBE9AE893F6D01B64D49108C3A1DDF64D5F600D05A3B4DF82DFAD37FE1BD687D18CD1805CB896BE8C3A212FDC207F2477CF4B88526046AA670FFC488F085D81CA3AD054F5C8A21C2407E152CE2792DAA18013394AC63D43F891F4696BF625FAF7CAC4E4B832AC4CD239084303080FCCD6C0A793C10804301EDE963A83057B48F16F8B730D7830B53D54873C3AD031ADF013ACDF8E48E1D056D0AF37161CD5EE68D0035D89AFB17E7B24A5E35D61CF9E397244FBA5818F7EA5BECA7AED9A5EDD486BE6BC6903F7BDBA69C97605F81128610A957EB98B9AE6D1ABDD1CED6424278C1709CC198A1DD7939375329D2186E3F453613DA9A107107A2A78F5C0992A5F680D445F4ECECC8D664EEC283129D447C09B79F3162C090E341C278AD0E98F0145B3BF3E7C7786E6385594C4AE84D741C07159DB160CC70ED2FB06A40993FE188D3BE3A3DF72140404CE2C5D2A393F347DFC7A73117FE52DDB70183508DF3D0833B179468311EA91CDE069974120EBD1139B1D247DE2DBA44FA2F5FDF275EC27CEF465320E2E46AF592186BB4C504C2C8A687B2B2B66240AC33AF5CEC543B56CF742FD3500C0B30E75C7E58D9D27B7518C16C7F8F13D1642592941449A57C49D829810A9591AE0E4EFC5B020B29750247AE31C993C0059FE510920E9139C5C3CF2E74C25E0A4AF33F2C0E40F5D4A8029DF4784019579281A40D5333041D0E81C4DCD008B6BD31C68D5ADEA663E3C41FE8AC385B84A069BFCC90D0E32E543280D80CED162E9393108A6A86B04523CDF0001A99EC49000B23BE381D86504240820CDA8CE839167B86F9A91E0C6076723CD5FD9D039CFB70DF52F539D3780A073FC42B0D83CC0B24085D06417C15EE005A01427EB1B811D26FA66E27E1B9CACA2AE792D55691EC0F544E6C89007956654041163D35936403D80E1A4996F9ABBE6E217EA9EA53B6C0621602332AF4F03A032A50B04874895D3C44038A507C839598A9486EED83706F5CE2E124874CE43A439208A137469401C5B82BAA52501EEE6150F0EBE992301A08AD187C19401F00DB0EA31E410343A765F02B9DC4AE6E1567A83E54189BF231510D124F0C1601258237202785447200EF6E5C6A94557A84E93804BD9532679E0AF6E1AA0E2D303B99939715C6FEF1A4D41F544B791054A7B3905B059D7B3DC10B91F4B00B9741CCA01CC76830278C58E5A125CBE2F15012CFD008DE695E7CE9C5BD8B84AAB24A4FFED69906C55610590D5314088AD6C7D179A5FB0CA369E442B62434A36A177D7955B836A593A4D4AF4CB5D2FB345E703299C224D406A7BF5A49904F1B9BF347F3E1920BDDE808F73AD1D4476B95F17105E07D221D978B88B122396EA5A3D1F5FB2194473EE6210505C830010CC99B51624171BF98BCC25C0D25C6FC047B9D60EA29A70270828AF8301482F7C1A46284F8D5F21E9440B31D255431EF1A51BA4817C0252D7F4A74E1321FD440B31D655431EFD85FBA6817C025097E4D79D331775570E3B17A2E67C7A04BDA059625C4A82A91281E6CC1B0F6E4BE661535A7378A821F735C301FCF4D79204494004852AE17535C15FD42BF61CB6A25B093F39D598C344841B51CC4134B80E752BF00C7B13BF80792DB9DF964E70A9CB2B74824BC929D69C9A42BE8967067A089E4B08F514BCEEBC508FBF77A59AEAB1169CF910046430F8C381188DE837C0E9D43803DF3087A548C35BE7F4C2E7BF765E67F3EA08402C48F8EF9B7726609967B58189113FBD5D2382FBF8364100710A21980EEE73DBDD4910EA7568CE4CF0DE8F66B0075E90A666213D46699803E0CDE86E67A0FE5C316712046F1A3314C0AF1A5353511C0835CC06FC8E717713527B3C17980BFEE3BA35D4C1E77509ACABE32801FDE083BA0490FC404C9B66EEB3AE00FD724FC0D6C8687C049620093863134C50E3B3AFDDF109FB32A968B2E0E74B615A98074CA1E9919C17E6C95295A96E312BFC273681D9917C8FB34659F38B9C0485F52348C17C35BFC129095573CA784247FC922497149EF069352F5D0B21E19B87E239697823914712FF954478AECAB367B919E33F8CD81D3391EFF50173C67DCEAF4603F4A01F817275F82D9807E809BF3A0C43E4026FD0C184373D5647A32F78AEAE4E8894A6123F5027EFCBD29B24E16EA8F9B5351E45C2DD508B19EA7137043CECD5C83E0DE68EE001306DC6E9C9B8A15FA8124F09FB88158F82038E43A5C5641C709C2986240BFB641530070DEF5AD550E7BF6C45204FC6F208A681FF9655772CC13EBC043954C4AF33D5BD1FDCF7994822F2A824911785FB2253776A36BD3928608C5A3D1F75B219447D1E542520BE06A17B26A8C2A7B2EBAC1CD2B969FC18DCA10C7ED4049401450DD30065E96B9A4BBD2920AE17892782770F894703700D496F52802B471D1C553003F29DD0BCA60AD4F01DD2AD27A80FDF742D02516215C1495D387430F95CC059396E52B300C05E56149549A4715E14D71527E1888939EA7B8131592E24E74A619971D361989BAFBED65B91869F334960967E06793A4F3F350D699C7003FD7466FE8E16532DB33CCF3AE1669F67ED0B28FFBCBA9502659CEF60028459D281C990CFAA5E23492AAF3A411E13BD2D982EA934EAD4D411341AE1A17336D13787938096624E603BF0B8AA8A4E6F602E0064C762B89E594EB4D2809652C44898C674C8BDDC2C4918CABC89373259D22CA56A3DF3BA75327D43B19A8CD617F4502751ACFD0DCC615F568030C7A9DC540AB3DE3610CBCB7B6B724279996EC9E31AEE9D9A4E27B9764D466DAAB9795AA52703CAD4DACDB443B95945934F5D3732FF099A3699120944C5C4376D3981FB4E2A73DBB401ED5253C96D47A573613613AAA8B2DACDE6408A4B76C7AA90CC519656050DA633A53DEBB122A5A0781EC1C4833C8AE8D483F04CBD6273603401EC7EF14AA5C703A64A3DAD5E8D56A5C47AAC22002E420A265629955E27EC274AEBC69FDCE62C701099C23C70EC5456173E9B675098F9ADCB8963F392F167AD21871944173F8B193B5FE57DD6E6E9E2E72DEB407170B26B09E64994870B248793890B98A3EA92AEC42C71726F494FBD60B2F06B50184A991FAAAC7BB433995EA38593173CDA499A4CD1325E39DE4930435E54549C38CBA5EBCFA3AA675EB235593A532CB67F7BB2BDF57AE1F9D1E3EDEB385E7E6D67274A4147F716EE340CA2E02ABE370D163BCE2CD87978FFFE57771E3CD859643076A635D3FC11856D39521C84CE1C51B5C9D009A6876E18C5FBC9FC5DA64A6D6FB6609AC965C32A06239362B11FAFB8E35CB4C6BF7306F497B36CEAEE41ABAE9AB8C384162C8553B210B514D86E49C7C9D4F19CB0C83446E439DB0BBCD5C2E7E73DE3F7AEDEA42461F05FAAE4439AAC2EF15EA40EA82C9487B3EBB978C19050F2227918C76E143F5D2DEA50CA4285D909A218ABA6DADCE465F250DCE47B3E0BE7753065A13C9CA3A4CB7E9AB5A9F6BDCB527948CF9789A4A1092B0B55E130B411C5AAB058FAC87216DAA31D6A35D14B758759AB94D0A417BE9458E05F4697120CB00D23211A781DBB110EBBD369B0F2A9255D16AA089928BA094246C814A5EA90268E17C3D0B21A7988F8DF3AA4AC441EC21E0B624F15C6F9C1711D425A20DFFF24B84C934691208A3285257846619116286081160185435A220FE13D9C1593924B45993C9483459AE98504921729D0727B8A1317D588C98AE4617C23B83C7763FABB54A50A5F264F585BFB389C24B67C28A7285CB889158EEDE1DADA21CA15A4C3FE098AA234F15D4D3E54C556D15A454BD72B2ADA2C674B4B359BE68E5557B270B76E54AC29ABD9F0D23660831B1259563048C0B983828148F9DC523A5479A1D54584A0EFC8E58491F5A4BE0E06DBA755977D5AEFD5387799A4B66BDCBEE366932CD7EF8CFAC055A9C2CE264BCD5CDBDB40D99A1B61B0BB2CA25861B7E6BCA4376B69891A0460CB57962ACC320A3F74A7F4241785CA7058A46A15D67D6695F748C432378F92944486B2E2490863B85B477218857EC0AC455FC91762CCA15E3C185033E7E1570444705EB869BE42124A5EA44053EC84C022248A15BC3CFE8C8554162ACC4F7A2E897B9DBBB400A5EB947C61F94356943F8CF3BC958013A3BDC0BF72434A1E13C5F2B0B2BBE3681E84B7346A54953CCC67377E7E4D900659AFB17AC3EA0DBABE85DEC8B2D768A88EF43D8C76EA03EE3A6E533EC19B46242F523108FD97CF438F3608F34205433E8F69A859F17959FFE66917C2D08A2E19387754741599A83584579639A79DF4E2F4DD7CF1F57C79C89C8E166556EC58B1B3E162274F00AF2175B247FEDA491D4E5F2B75E4A058A963A5CE5A4A1DC1330DD2728793684F52F2707B8FD7E7D7C52A4D0FA1183F5456A68419E4E269E5DF3125A54DC946D0BFD6C2BB66E5A10C9C3B280FE9A49C2D4522F5B0B1BA546C02D08D603C76FCF98A8913AC4AADE0B0824306CEDD141C55FEC5F652A37CC0BC95C8E0F71EF726CE5C104BC2628964B8A43999289687F5CD85B71F5091E545998AE4F18230FD2894EC298B555644BA47671CFC64B9CAB1AC73496F7B8BB2FE2574CE36073EC84D074A51B1A74E7C4DC9E8B464084D6828BA7675893BED3FA5C8AA8AAD0EB33A8CAE578FD43D41FE4A274E17F76F17A50BF71CB7DE6205B19A047EE1782B8A31F2221B0268A5C248A402F9C85D7BC3B608636B65D7723B77231ECCF8E19EFBACDA1F464C65B16CAC514496AF6794A21913D68A3E19387750F49DA3C5D24B506A2DF80A002DC41EBFEBC86D22A37BD42768823C06ADAAD42E72BBC8E97AC5455EBD59DB729117005A2C727ED7712FF2E4133A782EEA80AA52058C4678CF201755A0FCEA737B686A6B67C64A22FCCB9CB3E5A24AC15D6B6F245831DF9F98C78F726B08F9DD594B110F751CB780B722B02F116805960C9C3B2AB0D28BF35A322BBBC2DF4E6C71FA5AC925D3DF4A2E39285672C9C35A13C9953D14DFD66506BC7A2FE32E03BB8D5B54D9A33B3124BB10E97AC585F8C2F53C9D1B4579FF16CB91DBB39B1589573F0DA128B3EBDAAE6B59586BB2AE772F83558CFDC6BA71EA34A0162BBD1944474AD8CC259E8DBD2A6385870C9CBB2C3CF4A5868EB85837639D77DBAFDD553FB3A7E4BAE9E027B113AFA8589CA2CC4A2E2BB94622B972BDBC87AF4EE85EB24981B4BF65C3E93E6E1166EE9A8D5DEA624876A9D3F58A4BFD30F0E389FBEDF6AE8B02408B25CEEF3AEEE5FD2445BA1E28971529840AB3304E54614C58181355185630C8C0B98382019BA56EE01FF95741FB7B4A158C36779544BDBB9110E6DEFE1B97A3F41C51DB9FB4404D4A381E6BD290E5F2D07667B31045F46B2A45A1025537B3AF7EE59B1461799922946F0150BEA54493A1CB1AC66EF91AF0DAD9A041AB6DFAD73687475A294169487A8A870362DCF629813F8D1055A5C24D634ADD67E5880C9C3B28470E342447D2B785B0007B8DF724CE58F41F7E8134765E26537391BECE5DE70DA656C1925A2EBDDB8B058AAF036AB6EA35F2109F22346312CC94850A52270CA68CDD5A162AC099318B292FEACFA23F745ED7FBA7050AFE0BE63DD913C5E764CFD0C2095FD66114650A36AAEFB93E35994559DFE732FA8FFD9AB8AA6DAD76AB6DFBD3B660067D058D9B5A90ADB42EDC73DC967982368D485E642D712B1BE460AD8F6CD00D9AAB40B49310EB192867B35D5B216685D83884D81377FEC4F17DD43E0AA684D0428409FA8EDBCC317535D05EED938362258F3CAC35913C1F38FECC691F7B877BB709BA83FB8D5BDA7471DC7189E6AEEF275355874714CBC39AA12876FD14913AB45A45FF47CD669C5ED6DD6465624F3271CFD138DDC19D5B4844B85B3702118FC526FBAA4AD5201DEDB37070991A94F7562E730FA328ED5FD4EF057E9C7C250AA1A250E144C8A19F09C84AE421BC87F011571D4651A660982E986394BC48E124E880BA939216C8F7FF467059EF9F162808C8538AE74F87722B9C860838592B0A55CE36D9F34CB5334CFC0C06C36265A13C9CE59466F6AC44011356B63F53555C937397A6252F52E07416C6812A8C535A149DAA49A17146319E72D16A87D72988D8690BCCD2D444B474290B1578F0EACA9DB2908862359D7486964118C39A92AC53C070E939537A99E4650A9230C27262378ADC3965E9D76B14F0DA0D9143A1951529F083B198E6BD6B175DE163FECBE035F4DA0A54AF327BBBAB38D8F38288B6B5C90A850807D74B0CEB491C32BBB87A8D2A4496EFC872BBCDB1DB1CBABEC536E7345886C1546BB3938168B9E5E175EE6EE3C35EF7563D1837BB7D62ED95AA54CD94D47F942A7B3F823DC32B4AFBF6F9648A0C2F1B7696E83A55A86C101559AEE2F39FC108D62AE4E1ED23EF96C5AD2A55F1077AB7579E43C9B5AA540D2798C87A8D8AA0BC723DF480969279A1C2A63459711494BC481E469C747858879117C9C3F083A5779B184C11ED7BAD55286CC8A010D0658B10D04CBEB3E2AA56A10C6FB2BAE4822CEBAC89624D14BABEB58972AC11DF5383A265A880FDC76CAB8CCD2ED89DB2C75345992A14408313E556FA58E943D7B7903E37AFB4C4CECDAB96F206EA386641F33E6585BDAF6482BD4FD95FEF2B195FEFBF45F57EABBF73052B3C64E0DC51E19139EC340F9373202DE508B77737C2C4ACEBD2585A8A645C2649465E6697BA5DEA74BDE252AF6F7C5B2F766AFFACBEDE9B0074B3E44D2D5253E1667699CAC0B983CB34F5AB870E3E0DD652C8398C960A99DBBB3BEBDEC67A09311A4DAC976EDC898D15331D33A3B73734E37C33755492757908C151DA6D675DDE82E028EDBBB32E6F4370DE5687F30E04E71D7538EF4270DE5581F3A11B7869ACFD051B2448D7B5811A338770745DFF918C150640801353D906EED4612EBD3295ADF1BD7098F027B845FB11C2C0998947C85A2858A8315AD05352940D194B66ED6F193877DBFE3610765483A4678BAF6D10920D1DE242B1A1433674480CC7860E89E1980E1DCA85ED8BC2EC89F63C278A18B73CB795FC4855E73384BD015368206E236BE9584B87AE577D2F1764E2F6270330BC368FEA4A021AF749814DC7238664D72F5DAFBB7E091561701513504DAC652138BBA2ED8ABEC32B9AD9EDEBC613D3C04C78206C74B18D2EB622E90E8AA457372664D1AB1B3D2104F51FB3F4B121C756A25889C26E5B706684BD6B346DFF1E380DA8FDEE4400A23BD16226BEC8441C8CB9C57114ED05FE951B527B24A25829E209776111AB558C8DADF7D1D209633CEC0CC5C977D0E56E1A5E7B266F86D40DAF9B339CAD4A9181737755CACC8D12E4DAE789AD8369BFD2B800C6BEC0DC34B710146653AFB14BD62E59BABEDD923D4373EC14D65CB11994F60B96D77FDCEE68337E2DEBD46E9865BBD2A9FA962B3D706626B473014763B5732174B3DE33E942C3A84AED41965DF3B2B0D664CD7F3DF0DC9973DB7AADE7FD5BAC716ECF6ED6F651540C48393CCA6279587997E259AB3A44A65225B8319A86EE923D74AA55D8556F573D5DAFFC4ACBED69E0FAB1C6432D1980566FB5F0BA76E4C14D06C6BF28D76459AA6A1EB0B0C87285636143770AF7D1327EBAA0E54556260FE549E8F8D36B1A4E55AA60B204FEDC8D57745670A25801961343A0CA528508743764ECB1A24C1E8AE72EE814FB799115CA5628D3F53CA1BC1B45C1D44D83EC18C97C1678E822F30826520094BDF526B478C5B5592520DB67B44423405D4C82553885F2844809E514142499F16C94E32AA274EE8473042919299432288A483DDA013F8FFC177C8A6EA2FC05D70BFC1BF1BE23D490FE9A441BFC53E293B250353F2C09D0C0F705F0D3FBCA1848DFDFB87AA4F7E280FF819956FCB77E0FD8D79A80B9A3006A7E58029A81EF4AE3A6F7511310C6BF692196712E0EC7F551483729E57E5E52FE1D1505F8033A73947DDFAADF647A8D164E4A4AB474A6D8BC4C5A1CBA6184B5A873E944286BB2BD95E0FEA13B43E1E3EDC96DA2F317F770837B9357DE9EE7A609428A06278EEF5EA1283E0F5E22FFF1F6C3FBF77F677B6BD7739D083FCFEC5D6D6FBD5E787EF2C7751C2FBFB6B313A50344F716EE340CA2E02ABE370D163BCE2CD849BA7E75E7C1831D345BEC44D1AC769C4CD8FB84AFBAFED51EFD3E62BE7FF109CED015F10577A82F42777C047C753C34B65CE27C79BC87922F9328E6D9A9132796838F5BA114C9EDADA72BCFC3AF9F3EDEBE723CF6550D1A7CF55608314805230E578D204A975D06C1FFD009A7D74E62CF9C38AF8F913F8FAF1F6F3FB87F5F19B5F4430AA1BE735F15D7D22D08522B3763B91D2AC0EBA1325EA5556A146A65A26660B14197A5AB5083535AA846B123ACD50EE0AAD34D9AACC2E50F998E6B2B0076A7D360E58B57EF3BEA8BF7D489A29B209CD170FFDDC279FDEF55BF68016CE2784A62460676E692304BFC9E2250193CD36C4E46219E0497AE671ACDE7674D68AAE39946FF1B60A3222F577BD99F07230AC5953ABB9CDC9EBA53C3DFE11BC1E5B91B9BFFBCF989BC86FA44E1C28DA2D44B6F964F76F74F5014A5CFD09B903956CDDF71358FBD2C1BA2E4BB32D1F98BB9D59AEBC2E23720B1AC24B8E392207343305745375A1CBCD3461C34AF14F505DC8EA7153673B17394D885AFEFD0B76DB14DC22F7667AF159B857B7E8DD8AD522BE5914282365EADA0ED392FCD013286D504851FBA53A3B08CE1665D6B56D36A4B63F6006F7D05314A13E41A16C312D2BD8D0BA27CF3C1802078E1E28FA8210626B1131A595C794E475D3074EE4A3D68D555A6D6272EC40DD4F6B3FC01BA2CE22FF5F647CF6EFC04163610EC3ECB4A7F5DE97FECFA2F374503A80B6BA9130B74A3B7D0F0143F0FC57EEC16FED73C9A410B316D13D29854B3D2C84AA39BE8D0F5E8E48377491C49C115C923B90F79D87C0E3944A48795255696189425470B7C36696589952556965859A2274BF230E24D91265DB8CA64569C9CBF2676F4966C93776448C79F01D166C0176585DA5D176AC91AD93CB976ECF8F3151190D6CA196E17BE5DF89BBDF0B1724C8FA13763D5AF590446C233891CB86CE0C81680BFB9F0F60326AEB95D1045E0052114A9A08BE3F365BA2B6FF286B7F0D21FF8B8EDC0DEF09C150FC4B1D62DE62D5925D7A6619AD09606A23D579718C8FE53E3536655DD1D577593DBE804F92BABE6845FA14114BFAD0CF185E3ADC48A431DA60D76B3F2C084E95BC45D6D8648D077953DF7ABFE7DC6CCB5009BC5717561088D2A184FDF8EB582EEAE0BBA73B4587A18FC6688B9AE5C72DD6D469FA009F20894ED32B6CB587D197F3D58A0BB1D9E290517672BC220B51C37230B81CF459369B9D481A833B033D3377908A7B6D6A9AF0DADB7B2DB9CECDE9D59C96DA55CBF52CE8A27D182B7E289104FD955702BA1AC84B212CA4AA8B149A8F3E0C6BF43C2A94D36287B2866D799F63A7BE1264036E69E0C961A7A9AC72E56BB5847BB58772F83558C5DC29B15B7DDD57D944DBE056285811506B930D81429D0D16233722BADBBB362208377BBFC72B113AFB4424FAC54B252C944905D7673C20B36E5C98635BB606297B15DC6DACBF830F0E389FBED4DF10B74B5AB7F92CE91C64A3BD10530D10560D7FA5D5FEBD8727403FFC8BF0A3664B91B78654DE849EC280C5ECEC5898C6712C422C0F1BAB045CAB7A3CD227C7E33FBEA57BE69789DA540BF6518A8C9BB05262EA876E169B35173561319D74487473613A48CCB279FAECDCCE26625C25D9708CC23D4EB2B04D6E97D82E7F8E5C6D879E9FAF38BF4F565C34EEFDDE5D2BBBD58A0F83A30F364E9538466A6B29F247F4E9B8CD616349FCE8865620A68D36EA005C843E7B56990278ACF78CAC03C430B277C69E4833FF33DD73763A3EB9FC2187B7E55FB2AB2B5EBAD1637A6C5530B73433479474A3799256BC65B01B0A102C086AC3502B529946D988A1548BD08A427EEFC89E327E26053E4D17A5D805B9BDB6AB2C767569ADC6569F281E3CF1C2B497A39ACB84473D7F75DBF8949D4319CA12876FD144BE3B04D9E03EBFB98AC67C74A3E33926FCFD998031A4C0A06AFB320308CA3FD66E9D105EEEFAD5C712ECF16765A677732023F463E03B7DDD18FD390A7BE0D82C9B79955ABBC957DBA503CF4908BFE3A606E8BE87EE36F0497A6411E9D9A8668C221711AA2598324944E7A6F9A3EFC2A84221FCB805D4ED9455683F9B632A22694E8847CD8DD14AD075D003D6D90A90FBA0AB66C73A82E116CD9660ABAC3F8B42394D3F446E625E5B3AB2B77DA0560ACBECFD03208635D03E4D9D273A6C6BFD3518465DE6E14B9735F43003FDB0D91637A39E9877AEF5DBBE80A87285C06AF850F9AC84DD5EE2A0EF6BC20E27D47A9180CD74B2CC2491C36ED735B1CE865B0494633757E61B783763B18A1D3601906D30DDA14EAF9484C6D2B49EB46CB74D53C5AC4AF30E89D0B6ABBAF324584D9B86952A4CCA6149AB1B0AB3C03B409CCF691776B0CAF5902ECCA73E61A5F0EE3638AB6E7CB2BD7430F8CC74326D08C038D13680F4D03F583A5779BD84311EB6D6EF57D971251AB6D762B186E2410600A5026AB4B6D40D6C2B01646C64EC71B134C243432A4667D046A7D77AA7A70260FD59812B4F2C3CA8F08DDBCB2822303F0FE039DCE0F753ABFA5E364317EBE600583150CE92585644636E5DA31EBE83398109751BF72C797090C1B6B67D7A9C63AA5F6929BB1548D2E3293B16676BDDDF5F596FA9E43071F7D6EC862B3815ED20268F303BDD4023C6CEC58E7B1631D858498DF326A3BDD3A3A02C9C01A3FAFC8C0BED50DD8B7BB01FB4E3760DF350DF64337F0D27B07174602142B707153485C8B13214351941592CD214B2DA25C2AF053A7F9AA6E7BE829F2174E736093F61861E034A92A754B3C468B86C97967A86831BBF5B05B8F72EB61838A6C40101F820D08B201411CB03620688C0141B9547F519838D19EE744919614A9609D21ECCA986A43B406C85D37406036DD102BC4E6E7B12B6F7D561E21D4EDFAB3EBCFAEBF9EB7DE36DA76547B661B6D6BC5C87A89915737567E64006CD0AD950F563E90663EBE7CBF778DA69BF2947557513ADDC485C8F0B61CA0682FF0AFDC506BEF91836881902AD7EDA3A513C68BE41BCF508C27763398CF80756905B315CCE91299B951027853529B1A58196E9ADE4518DB603D2C7685C9AFB03334C71BF9CD5860DD3D47A6E971B13E51BB628DADD8C0996D9052CC04907DE9C3AEDACD5CB55F0F3C77E6DC6EC86A3D8A0A7AF2512E5DF545918328DE1A32E2EBDF47D1347497D0A9843D3BB00BB7C5C2FDC0B93D0DF022DA8C95BB9774C2BF4CFB0433FDDD05E48E6E62EDA365FC74611AEA93D0F1A7D7E6E11E07FEDC8D57E6731F1F3B712770676EA8688BC940F5DC85F9A4E756C6DF75197F167828F3CAB591F2B8F7052BEAC1B6D92840EB66C5500ED37E1B428C2E0B447A0E9FA29B287F5C13FF6C33910408D9F9C45D5ACD263D56FB492D51303EA5D573A507ED9EB7A900C8CE67D2A3D5745223B59FCD0201DDC9DC8DA260EAA6419B84F7EE22CBD39E3E9D5147E3C09F6DE115567F576382BCAB7B59C1C9CA8BDDA5E74E93C11E6FDFBF77EF01434A1D06EEDB08E7B71820C9B740184717BF75E04771E82493C07E38D79FBA4BC72371A61A495AA4780A4B70744D6224E12C027E4CD325335695139F1DB1044CF15913F58F76880F2BFEDEB9B03BB9D5FFDA0F687A1F3DF3F7918762B495C5AAA567ED5367C6B277C29E33DED8B9CA21472F8AD69A4B2055CA19A970A90FC221A9E4768A6B76ACF66FF1A51AE442AA1949285941275F5BE12B687E6F56DF73C6A94E3E07FBDEC7AEFFF202305014BF514712A1C090193F2BEC844FA4BF9E012E49C990192B693C289B1CBA1E8A0CF089843C48876200E5A56BFFC5333AD6E193A72F4CF7F3C96B8F599780F2D2B5FFE4191D63FFE41FA04B720F48FCE67FFF7A9FDA07A4AB947902022A8068882F28B47BE21095E1120C8B13A241C543C1282728768E92F15E0B8CC5A245DD5E2C4B7B332586E129CE0C8D839D52E4C6C047F953A2225983EBC9139542D054E5EB23654A9C47C813C4ABAE43A9A2627AC6ECAE68C1916BE6B850E5D341BD1709B2798AE5B5D14915CA340395C5EBAF91086A46AF90F00BB5A947A47F6D540C5D0357156E821E2AA9515042F88F41D96177D6053374245032841916C2459BC240092DEBC43EE7D728B162D68B83529C1926CA4B37858F3272D685955EB89E87DD71E7C10D7D2BA5FA766925F9D9B2825EF82647B0367C59D609CFB053D10DB31454C80C85711A8C47762F83558CF5EB705B6A1A851A58B67213640943D53AECB04BA42F40AC4D7D51596E818176C41FAD3E982926911A6C70FF2E4EFD8B6F1FF957411E21949770D984E851FB98B57225E6207A32914302A88698841CA107FEA089951972E070229247F4AD127966E89911FAB23254196E504B8340F6F008070670B1EF4B44A468F0C0E6951B262832AA64C6243A0DC63207E9339EE8828DDCADBE27AE233F61FAB71247E4A3D050B2B24EBE3F434F37DFBD204266A8836838C392887BCE0EAA85C66557F103151634276C5AF4004192E4BE6370E39260917539A7198A9F7A3DA75164A541CF69EA62A66727C780D2A54FC786BA6819D4A5F1C49D3F717C1F8503704439760D1E51BA09FC5091A3C00E833ACD3F70FC9933D0DE241DBBCE5E59C926EC423252D665EB81D37E0E2014F0B0355059C1268882949275500AE9A7CF1C65531669D58F25F1C1195765871F5DFA2B18F8DAEBE295C4B8662F235CE87FEF8E7613158E0C0645F1DAF30BF4302467B42271FBD0FC925D7CECFC2AF4505FBFAF9851C5AF3F68C828F3F57BBAF63A140FF4770556910B06BE085B617B1CCC0D280EE9CF7F4CF910A89A0D5103CC23459C0147A209300FF4AE0C86E084FE55822C278C442B549CD0BB6218821F86500FB21C31020D71F3AA1FD570F38A81828BD65E1924448C5E0BE4EF284F5697F80BA4DFFBD0F5621446F4593BF1D1EA9D6A1F8FAE5266867C748623CAF24ED88242BB2706296892190EE335A834C8D38EF76929E44332BC5096AFBD955050B2163642ED21CE66D5A0F8F53A743ED510E7E1D3B92B4AE55B9B63AD75724CD519AC7F413320830C2074D6C9690571468F5B94E1F9A3DFAD8A3A878C60BB52E791EAC9F63DE0B176225A9B7EE13D6B5E8BDFE634594336E290325AB682F11D259B9D21FCF8CC548DD9C84E4296AB35DC04C6E34ED798D80F44773CDC27E5BC5734C355B909F2DBB10D36D2A85E1B373FC3344399D60332CB6006F6DA1C0170B86428337B405E19D0D85EA3E3811CEF573703EAA057373C98B866C3B44E42D2E8D5CD39FCA679BFB28483046461B36DD65CA2F0E85A03A1725E7BEC7B1096291E1A0738A5ACDA0C06019F541F355F642FCCF565BBD60685F8A1A8596B6BB54ECBE8CDD402DDFCE9E30BF0D572ED6F28C919C5FBCB10DCB2AE4B678AC25733C426E093D39C21AB079D0761158CF84526B8A28B49B00AA77C1315FF5BFB8C59412FE793F85F408991C59DF0504A620FAC431022C53501EF2DBDDE99E6DC09E788AF64A42D8F0D639CFE2C1545D6E13EADD813F3D0AF49A046B9D322217BF78F05806F9CD4EA3A7BA064985728A49F46697C9E72202E6B105443BF67353C5F8D99A186E32422A7C48184B052CE02D1796E12207D1259D355FAA341B2DDC8A65D6A7AF17508C66A904F1259B036959546CB437D31CF41FA766FD2274E7AA0B0F060073374E886117EEADCB9840E1770AF098A099FC4F6D641F90E70CDEB33995EA385F3787B7689535D64EF08E39A08D8E2D7C116663403B8A88040E7BB8746E0D91693019D154380714D33D8C96D14A3457A34C6C026EAA001CA6A999929F351019353D6C1F393574B10939905CC085931043CAB91809AA5DB052167553CE8B8566E88FC0147708CBC8E37485E2D3148FE642038485EC71B24AD961BA554F0E038652D6FA4F259BDA6B1E81C78CC70740368C47A1BA941AB8427D088552D67B8A281D4EA3C41FE0A5E9B590D6765E24A295ACA87742052CA4A0E25797DF340E768B1F49C181AA6AA8206296A9B87A89E826186A8AAA0218A5AB921F05321E000B882077E7726073C7F4202849FD7F18648AB253E449A5B98FD086931F801929A66B0E54B060CE4B206029E5736C36713A93303B14DA011E9560A438BC66C184C5AAAEC055E005910F56A815C495B340F7798585013F7DBD007ABAAA0618A5A09D945A60663E517590BCAB0AA81DA60793263D18879938661F3560D23A7F63E33585A0AC1C71B1C199859465E086E56C3812D6709907B706808E10AAAEA9B072232FC31E31075D03065B5C4DAC912C7B18B262B07570BAE6A869C5D50660067C5105C5C2307B6889E06811795BC21F2EBA70A03C1163E552F1E4ECAD22F2E638343E10ADE1837AFE48097F77AC111CA5ADE30658386A1E87BC9CC68740368C07A1B39FACA103790BEB296475FDE40692C212B526D1AC695654CDE1519D630E134044D15B0AD8C5124BC3DD18C53ADB91466440F35BE685ACD6C33A92F26BDB6896849D1F8B8BE61E057125BEFBCE989E37A7BD7680A99606C13C107285B490FCDC6FBF130605B0A10A11B4BE3534695F1D0281B0846CFDB480F5A84BDF0C62CEA0543664DE4472C0362B863962D44A3E68D64765F9E3B736EC1BD575E03EFBCD24A191BE5F634707DC8ED5855C1964A562BE721E4BA20C94A9EB750D615C91CC489DC3C62E75BAD91B4D5CAB5B9C97AB1ED0ADAE184D3B9EE2CCE53A4660E62A215E138269BD07E703AD2B0A4AC28603CE7F51E59D2D4865E359F79D24A82B43C02227F421C20ACDEC0245975464DFB1445DA64A5C7A4B568608034B6910164817374A20FC8E5EDC84B9FA6CBE0C1A4550D3411647A90AE90B257566884B4740B2BA48D68619AB89A13BEEC06EFAADB9197BAD185E4112D4C935773FF97DDF2526DF2EA9EF48BBA579DA555D49C4F06ECF34F69A1AB1A26030221E8DFF27BB34FB9703E7BC39B2F9440A2CED3729954968E8AF422B57C03E160067AFADBD3472FC587AFCA07279DC046A05A815626F56B8BA96A476A7E0424E66FB05D37ECCD1E69157497C5DA6417274622CE66DA74C7D6F4C957DAB92A3442EEEEAC8958A245B7A456277025A1BB4CF4494B32EBAFD17328153C596F9CD8DA8960496F5EAA4D72EDD574805AFEABEA3544C943C514C7AC40401A755E98762ACBB4C9E23EF40D9028F728B8D1EFCA3BCC4C81B095E6A6A37CCC5A340FF08BD7E6088021C1200C10CF7FAA199804C9779D6B2400E79C2911B572C11430EF38CBC1D09C0ADE8A17BF5AACB9EA5B4C951E99ECE3BB62921B1EEBEDEABBD78EAC6920F031B4FAB4900FCC02B3C07D7FB68634E1794BF14CFF1610499D64177DB2321324018FA9C2C435BDBADAC126933D662FE83727DBC1C742C533D0B505DE33D902DDDEF40AA651ADDE07D9EC1B8E00D50D0F3D1A259A09F0487B13A5FA5B4CE68D42687F297EC8D0A8DCAE459B6493959568938A0F47451FB656DFDD3725835ED28E598119F2C8B7E6782472DFA3534793E9C19858068923135D7368E3E6C236405A3D8CA3EC57149B2491C8A9212694977C43D3BB3508B9FCB3245E53D3274AFD925D668B14520CE79434C6CEC7943AA56A0C932BCBD7A29C88C6587B08D265785C9CEACF289BF73105C5AB351C9AC1476D0CB077152459F6C145DA24091F680168947FD0A546021C1F99924257354C0415D059CE062F52B3DD573E675F23E17C70A0A5E9F54DC578962497E5264996B04A80964609E0F5E7E832AAB683C990E682EECD98714C8558EE0B7A7421FB879E1226D85866561A52E4937E5361EC73E649E534599B89AB4544AB4D9F20D1BB681281906D782A6B0DC73DA14D1638DCB847D10DD9676C836E26465186776CB28F6C5A14E4792FE6FCC0D353E49C16CF079899BAAB0554DDA0A021E01A6DE2A5F22A03D3A19E8FD9008734DCB520E538DBC6D454816984F933D49C75D8DCC450973EC8F928AB4C4D039B35973F070D197635C52A78F984A4BDA831463A95265640B828A1AC09224018D49D981A94B24E7B32F0987422546026A0667C12C8EC3529DE59818068F6364BD9CFD845811A0D79D6A92652A1E45406567A1FE4F2134E3644E60AB253D68868115FCBEB0FC6B78B6E0B19990E3E0F48A65164C8518FD1EF6F023809FDC407E9DCD47F7A87E19CDE40BC0959637C0AF80C2093A48E26423554C630D138971D0652A64B2BEB1EED64B7E3F282E4CF38089D393A0966C88BD2D2473B67ABA4F702657FED23ACE34B108F12983E4A53E955408B36F808BDC8124761543429AA89FC60587DE110ED2B671A27D5539458157EC2272F1C6F85A7271182B323FFD92A5EAEE284E444287AB7E464E06C73A2F11FED30383F7AB64C5D02264848D0741312D033FFC9CAF56625DE878E477F341E089CC6EE3D949467DF324EFE8FE6B725A4A7812F09289FBE32FB5E9944E9993F713E447CDC9AE7B03E638FF65D671E3A8B288751F54FFE4CD86FB678FDBBFF1FAD2DADBAA8EF0400, '5.0.0.net45')
