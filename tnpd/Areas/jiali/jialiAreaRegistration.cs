using System.Web.Mvc;

namespace tnpd.Areas.jiali
{
    public class jialiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "jiali";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "jiali_default",
                "jiali/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
