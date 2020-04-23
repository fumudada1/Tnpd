using System.Web.Mvc;

namespace tnpd.Areas.pppc
{
    public class pppcAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "pppc";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "pppc_default",
                "pppc/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
