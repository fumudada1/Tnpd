using System.Web.Mvc;

namespace tnpd.Areas.traffic
{
    public class trafficAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "traffic";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "traffic_default",
                "traffic/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
