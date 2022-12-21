using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.DataAccess;

namespace TnpdModels
{
    public class BackendContext : DbContext
    {
        // 您可以將自訂程式碼新增到這個檔案。變更不會遭到覆寫。
        // 
        // 如果您要 Entity Framework 每次在您變更模型結構描述時
        // 自動卸除再重新產生資料庫，請將下列
        // 程式碼新增到 Global.asax 檔案的 Application_Start 方法中。
        // 注意: 這將隨著每次模型變更而損毀並重新建立您的資料庫。
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<TnpdModels.UnitContext>());

        public BackendContext()
            : base("name=TnpdConnection")
        {
        }
      

        public DbSet<Unit> Units { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<SystemLog> SystemLogs { get; set; }
        public DbSet<MetaIndex> MetaIndices { get; set; }
        public DbSet<News> Newses { get; set; }
        public DbSet<NewsCatalog> NewsCatalogs { get; set; }
        public DbSet<WebNewsCatalog> WebNewsCatalogs { get; set; }

        public DbSet<NewsLink> NewsLinks { get; set; }
        public DbSet<NewsFiles> NewsFileses { get; set; }
        public DbSet<NewsImage> NewsImages { get; set; }
        public DbSet<SysMenu> SysMenus { get; set; }
        public DbSet<WebArticle> WebArticles { get; set; }
        public DbSet<WebSiteName> WebSiteNames { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<HomeLink> HomeLinks { get; set; }
       
      
        public DbSet<HomeAd> HomeAds { get; set; }
        public DbSet<HomeTheme> HomeThemes { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<AboutLinkCatalog> AboutLinkCatalogs { get; set; }
        public DbSet<AboutLink> AboutLinks { get; set; }
       

        //網站顏色定義
        public DbSet<WebSiteColor> WebSiteColors { get; set; }

        //網站字型大小
        public DbSet<FontSize> FontSizes { get; set; }

        public DbSet<StationInfo> StationInfos { get; set; }

        public DbSet<Ese> Eses { get; set; }
        public DbSet<EseCatalog> EseCatalogs { get; set; }
        public DbSet<EseFile> EseFiles { get; set; }

        public DbSet<BigBanner> BigBanners { get; set; }

        public DbSet<StationInfoFIles> StationInfoFIleses { get; set; }

        public DbSet<Wanda> Wandas { get; set; }

        //信箱
        public DbSet<Case> Cases { get; set; }
        //信箱處理
        public DbSet<CasePoproc> CasePoprocs { get; set; }
        
        //信箱白名單條件
        public DbSet<CaseFilters> CaseFilterses { get; set; }
        public DbSet<PoprocsSubType> PoprocsSubTypes { get; set; }
        //交通檢舉信箱
        public DbSet<CaseTraffic> CaseTraffics { get; set; }
        //信箱處理log
        public DbSet<CasePoprocLog> CasePoprocLogs { get; set; }
        //交通檢舉信箱處理
        public DbSet<CaseTrafficPoproc> CaseTrafficPoprocs { get; set; }
        //交通檢舉信箱處理log
        public DbSet<CaseTrafficPoprocLog> CaseTrafficPoprocLogs { get; set; }
        //交通檢舉信箱問卷
        public DbSet<CaseTrafficqw> CaseTrafficqws { get; set; }

        //交通檢舉信箱問卷
        public DbSet<TrafficMailCheck> TrafficMailChecks { get; set; }
        public DbSet<CaseMailCheck> caseMailChecks { get; set; }

        //信箱問卷
        public DbSet<Casewq> Casewqs { get; set; }

        //交通案件承辦人2
        public DbSet<TrafficDepartmentdetail> TrafficDepartmentdetails { get; set; }
        //交通案件承辦人1
        public DbSet<Trafficdisdata> Trafficdisdatas { get; set; }
        //交通區域
        public DbSet<TrafficRegion> TrafficRegions { get; set; }
        //道路資料
        public DbSet<TrafficRoaddata> TrafficRoaddatas { get; set; }
        //回覆類別
        public DbSet<TrafficViolationsClass> TrafficViolationsClasses { get; set; }
        //免回覆類別
        public DbSet<TrafficViolationsRejectclass> TrafficViolationsRejectclasses { get; set; }
        //假日設定
        public DbSet<Holiday> Holidays { get; set; }

        public DbSet<WayPoint> WayPoints { get; set; }

        public DbSet<TrafficSMS> trafficSmses { get; set; }

        public DbSet<TrafficSMSCarInfo> trafficSmsCarInfos { get; set; }

        public DbSet<TrafficSMSCarInfoReject> TrafficSmsCarInfoRejects { get; set; }

        public DbSet<RefugeStation> refugeStations { get; set; }

        public DbSet<TrafficFrontEndViolationItem> TrafficFrontEndViolationItems { get; set; }

        public DbSet<TrafficViolationItem> TrafficViolationItems { get; set; }
    }
}
