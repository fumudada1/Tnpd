using System.Web.Mvc;

namespace tnpd.Areas.precinct3
{
    public class precinct3AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct3";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct3_default",
                "precinct3/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
