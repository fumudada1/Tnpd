using System.Web.Mvc;

namespace tnpd.Areas.safe
{
    public class safeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "safe";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "safe_default",
                "safe/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
