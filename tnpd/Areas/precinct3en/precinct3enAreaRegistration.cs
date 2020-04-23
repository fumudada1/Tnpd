using System.Web.Mvc;

namespace tnpd.Areas.precinct3en
{
    public class precinct3enAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct3en";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct3en_default",
                "precinct3/en/{controller}/{action}/{id}",
                new { Controller = "Home",action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
