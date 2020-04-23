using System.Web.Mvc;

namespace tnpd.Areas.yujingen
{
    public class yujingenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "yujingen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "yujingen_default",
                "yujing/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
