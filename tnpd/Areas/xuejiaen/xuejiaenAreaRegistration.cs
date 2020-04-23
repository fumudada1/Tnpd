using System.Web.Mvc;

namespace tnpd.Areas.xuejiaen
{
    public class xuejiaenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "xuejiaen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "xuejiaen_default",
                "xuejia/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
