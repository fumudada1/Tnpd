using System.Web.Mvc;

namespace tnpd.Areas.precinct4
{
    public class precinct4AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct4";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct4_default",
                "precinct4/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
