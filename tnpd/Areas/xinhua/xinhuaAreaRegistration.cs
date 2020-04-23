using System.Web.Mvc;

namespace tnpd.Areas.xinhua
{
    public class xinhuaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "xinhua";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "xinhua_default",
                "xinhua/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
