using System.Web.Mvc;

namespace tnpd.Areas.jpb
{
    public class jpbAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "jpb";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "jpb_default",
                "jpb/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
