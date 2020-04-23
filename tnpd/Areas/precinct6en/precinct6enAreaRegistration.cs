using System.Web.Mvc;

namespace tnpd.Areas.precinct6en
{
    public class precinct6enAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct6en";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct6en_default",
                "precinct6/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
