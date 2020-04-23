using System.Web.Mvc;

namespace tnpd.Areas.precinct1
{
    public class precinct1AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct1";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct1_default",
                "precinct1/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
