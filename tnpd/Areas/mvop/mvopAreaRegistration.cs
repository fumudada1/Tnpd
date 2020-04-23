using System.Web.Mvc;

namespace tnpd.Areas.mvop
{
    public class mvopAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "mvop";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "mvop_default",
                "mvop/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
