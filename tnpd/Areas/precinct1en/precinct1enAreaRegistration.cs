using System.Web.Mvc;

namespace tnpd.Areas.precinct1en
{
    public class precinct1enAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct1en";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct1en_default",
                "precinct1/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
