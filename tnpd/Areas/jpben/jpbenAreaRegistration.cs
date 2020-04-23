using System.Web.Mvc;

namespace tnpd.Areas.jpben
{
    public class jpbenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "jpben";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "jpben_default",
                "jpb/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
