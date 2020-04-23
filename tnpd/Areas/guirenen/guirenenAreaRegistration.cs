using System.Web.Mvc;

namespace tnpd.Areas.guirenen
{
    public class guirenenAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "guirenen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "guirenen_default",
                "guiren/en/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
