using System.Web.Mvc;

namespace tnpd.Areas.pppcen
{
    public class pppcenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "pppcen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "pppcen_default",
                "pppc/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
