using System.Web.Mvc;

namespace tnpd.Areas.yongkang
{
    public class yongkangAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "yongkang";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "yongkang_default",
                "yongkang/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
