using System.Web.Mvc;

namespace tnpd.Areas.xuejia
{
    public class xuejiaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "xuejia";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "xuejia_default",
                "xuejia/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
