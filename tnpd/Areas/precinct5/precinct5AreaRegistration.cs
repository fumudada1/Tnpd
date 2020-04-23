using System.Web.Mvc;

namespace tnpd.Areas.precinct5
{
    public class precinct5AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct5";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct5_default",
                "precinct5/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
