using System.Web.Mvc;

namespace tnpd.Areas.precinct2en
{
    public class precinct2enAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "precinct2en";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "precinct2en_default",
                "precinct2/en/{controller}/{action}/{id}",
                new {Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
