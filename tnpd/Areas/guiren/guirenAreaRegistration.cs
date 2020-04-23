using System.Web.Mvc;

namespace tnpd.Areas.guiren
{
    public class guirenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "guiren";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "guiren_default",
                "guiren/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
