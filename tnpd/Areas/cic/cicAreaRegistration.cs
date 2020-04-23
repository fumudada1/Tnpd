using System.Web.Mvc;

namespace tnpd.Areas.cic
{
    public class cicAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "cic";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "cic_default",
                "cic/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
