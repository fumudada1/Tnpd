using System.Web.Mvc;

namespace tnpd.Areas.precinct6
{
    public class precinct6AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct6";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct6_default",
                "precinct6/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
