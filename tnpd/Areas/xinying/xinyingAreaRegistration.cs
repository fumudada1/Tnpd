using System.Web.Mvc;

namespace tnpd.Areas.xinying
{
    public class xinyingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "xinying";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "xinying_default",
                "xinying/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
