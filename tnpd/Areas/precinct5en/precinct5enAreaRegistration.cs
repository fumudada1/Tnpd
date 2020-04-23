using System.Web.Mvc;

namespace tnpd.Areas.precinct5en
{
    public class precinct5enAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct5en";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct5en_default",
                "precinct5/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
