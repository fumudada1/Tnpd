using System.Web.Mvc;

namespace tnpd.Areas.wpben
{
    public class wpbenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "wpben";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "wpben_default",
                "wpb/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
