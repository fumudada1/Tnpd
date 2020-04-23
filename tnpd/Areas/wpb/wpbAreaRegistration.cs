using System.Web.Mvc;

namespace tnpd.Areas.wpb
{
    public class wpbAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "wpb";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "wpb_default",
                "wpb/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
