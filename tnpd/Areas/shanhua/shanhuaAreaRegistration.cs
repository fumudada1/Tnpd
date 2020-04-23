using System.Web.Mvc;

namespace tnpd.Areas.shanhua
{
    public class shanhuaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "shanhua";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "shanhua_default",
                "shanhua/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
