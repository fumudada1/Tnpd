using System.Web.Mvc;

namespace tnpd.Areas.madou
{
    public class madouAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "madou";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "madou_default",
                "madou/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
