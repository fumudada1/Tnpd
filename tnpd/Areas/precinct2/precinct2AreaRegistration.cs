using System.Web.Mvc;

namespace tnpd.Areas.precinct2
{
    public class precinct2AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct2_default",
                "precinct2/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
