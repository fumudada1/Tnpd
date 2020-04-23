using System.Web.Mvc;

namespace tnpd.Areas.madouen
{
    public class madouenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "madouen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "madouen_default",
                "madou/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
