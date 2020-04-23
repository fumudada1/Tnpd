using System.Web.Mvc;

namespace tnpd.Areas.trafficen
{
    public class trafficenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "trafficen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "trafficen_default",
                "traffic/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
