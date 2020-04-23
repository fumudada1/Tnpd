using System.Web.Mvc;

namespace tnpd.Areas.yongkangen
{
    public class yongkangenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "yongkangen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "yongkangen_default",
                "yongkang/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
